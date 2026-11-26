using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ComputerStore.DAL.Repositories;
using ComputerStore.DTO.Entities;
using ComputerStore.DTO.Models;

namespace ComputerStore.BLL.Services
{
    public class BanHangBLL
    {
        private readonly BanHangDAL banHangDAL;

        public BanHangBLL()
        {
            banHangDAL = new BanHangDAL();
        }

        // 1. DU LIEU CHO CAC COMBOBOX VA DANH SACH
        public DataTable GetKhachHang() => banHangDAL.GetKhachHang();
        public DataTable GetSanPham() => banHangDAL.GetSanPham();
        public DataTable GetDanhSachHoaDon() => banHangDAL.GetDanhSachHoaDon();

        // 2. THEM SAN PHAM VAO GIO HANG (CHUA LUU DB)
        // Kiem tra tong so luong trong gio khong vuot qua ton kho hien tai.
        public bool ThemVaoGio(IList<DongChiTietModel> gioHang, int maSanPham, string tenSanPham,
            int soLuong, decimal donGiaBan, out string message)
        {
            if (maSanPham <= 0)
            {
                message = "Vui lòng chọn sản phẩm.";
                return false;
            }

            if (soLuong <= 0)
            {
                message = "Số lượng phải lớn hơn 0.";
                return false;
            }

            DongChiTietModel? dongDaCo = gioHang.FirstOrDefault(x => x.MaSanPham == maSanPham);
            int soLuongTrongGio = dongDaCo?.SoLuong ?? 0;
            int tonKho = banHangDAL.GetSoLuongTon(maSanPham);

            if (soLuong + soLuongTrongGio > tonKho)
            {
                message = $"Không đủ hàng. Tồn kho hiện tại: {tonKho}, trong giỏ đã có: {soLuongTrongGio}.";
                return false;
            }

            if (dongDaCo != null)
            {
                dongDaCo.SoLuong += soLuong;
            }
            else
            {
                gioHang.Add(new DongChiTietModel
                {
                    MaSanPham = maSanPham,
                    TenSanPham = tenSanPham,
                    SoLuong = soLuong,
                    DonGia = donGiaBan
                });
            }

            message = string.Empty;
            return true;
        }

        // 3. TINH TONG TIEN GIO HANG
        public decimal TinhTongTien(IEnumerable<DongChiTietModel> gioHang)
        {
            return gioHang.Sum(x => x.ThanhTien);
        }

        // 4. THANH TOAN: LUU HOA DON, CHI TIET VA TRU TON KHO TRONG MOT TRANSACTION
        // maKhachHang = null nghia la khach le.
        // Tien khach dua chi dung de kiem tra va tinh tien thoi, khong luu vao DB.
        public bool ThanhToan(int? maKhachHang, int maNhanVien, decimal tienKhachDua, string? ghiChu,
            IList<DongChiTietModel> gioHang, out string message, DateTime? ngayLap = null)
        {
            if (maNhanVien <= 0)
            {
                message = "Không xác định được nhân viên bán hàng.";
                return false;
            }

            if (gioHang.Count == 0)
            {
                message = "Giỏ hàng đang trống.";
                return false;
            }

            if (gioHang.Any(x => x.SoLuong <= 0))
            {
                message = "Số lượng sản phẩm phải lớn hơn 0.";
                return false;
            }

            decimal tongTien = TinhTongTien(gioHang);

            if (tienKhachDua < tongTien)
            {
                message = $"Tiền khách đưa chưa đủ. Cần thanh toán {tongTien:N0} đ.";
                return false;
            }

            HoaDonDTO hoaDon = new HoaDonDTO
            {
                MaKhachHang = maKhachHang > 0 ? maKhachHang : null,
                MaNhanVien = maNhanVien,
                NgayLap = ngayLap ?? DateTime.Now,
                TongTien = tongTien,
                TrangThai = "DaThanhToan",
                GhiChu = string.IsNullOrWhiteSpace(ghiChu) ? null! : ghiChu.Trim()
            };

            List<ChiTietHoaDonDTO> chiTietDTO = gioHang
                .Select(x => new ChiTietHoaDonDTO
                {
                    MaSanPham = x.MaSanPham,
                    SoLuong = x.SoLuong,
                    DonGiaBan = x.DonGia,
                    ThanhTien = x.ThanhTien
                })
                .ToList();

            try
            {
                int maHoaDon = banHangDAL.Insert(hoaDon, chiTietDTO);
                decimal tienThoi = tienKhachDua - tongTien;
                message = $"Thanh toán thành công hóa đơn mã {maHoaDon}.\n" +
                          $"Tổng tiền: {tongTien:N0} đ\nTiền thối: {tienThoi:N0} đ";
                return true;
            }
            catch (Exception ex)
            {
                message = "Thanh toán thất bại, dữ liệu đã được hoàn tác: " + ex.Message;
                return false;
            }
        }
    }
}
