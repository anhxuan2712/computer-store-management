using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ComputerStore.DAL.Repositories;
using ComputerStore.DTO.Entities;
using ComputerStore.DTO.Models;

namespace ComputerStore.BLL.Services
{
    public class NhapHangBLL
    {
        private readonly NhapHangDAL nhapHangDAL;

        public NhapHangBLL()
        {
            nhapHangDAL = new NhapHangDAL();
        }

        // 1. DU LIEU CHO CAC COMBOBOX VA DANH SACH
        public DataTable GetNhaCungCap() => nhapHangDAL.GetNhaCungCap();
        public DataTable GetSanPham() => nhapHangDAL.GetSanPham();
        public DataTable GetDanhSachPhieuNhap() => nhapHangDAL.GetDanhSachPhieuNhap();
        public DataTable GetChiTietPhieuNhap(int maPhieuNhap) => nhapHangDAL.GetChiTietPhieuNhap(maPhieuNhap);

        // 2. THEM MOT SAN PHAM VAO DANH SACH NHAP (CHUA LUU DB)
        // Neu san pham da co thi cong don so luong va lay don gia moi.
        public bool ThemChiTiet(IList<DongChiTietModel> chiTiet, int maSanPham, string tenSanPham,
            int soLuong, decimal donGiaNhap, out string message)
        {
            if (maSanPham <= 0)
            {
                message = "Vui lòng chọn sản phẩm.";
                return false;
            }

            if (soLuong <= 0)
            {
                message = "Số lượng nhập phải lớn hơn 0.";
                return false;
            }

            if (donGiaNhap <= 0)
            {
                message = "Đơn giá nhập phải lớn hơn 0.";
                return false;
            }

            DongChiTietModel? dongDaCo = chiTiet.FirstOrDefault(x => x.MaSanPham == maSanPham);

            if (dongDaCo != null)
            {
                dongDaCo.SoLuong += soLuong;
                dongDaCo.DonGia = donGiaNhap;
            }
            else
            {
                chiTiet.Add(new DongChiTietModel
                {
                    MaSanPham = maSanPham,
                    TenSanPham = tenSanPham,
                    SoLuong = soLuong,
                    DonGia = donGiaNhap
                });
            }

            message = string.Empty;
            return true;
        }

        // 3. TINH TONG TIEN PHIEU
        public decimal TinhTongTien(IEnumerable<DongChiTietModel> chiTiet)
        {
            return chiTiet.Sum(x => x.ThanhTien);
        }

        // 4. XAC NHAN PHIEU NHAP: LUU PHIEU, CHI TIET VA TANG TON KHO
        public bool XacNhanNhap(int maNhaCungCap, int maNhanVien, string? ghiChu,
            IList<DongChiTietModel> chiTiet, out string message)
        {
            if (maNhaCungCap <= 0)
            {
                message = "Vui lòng chọn nhà cung cấp.";
                return false;
            }

            if (maNhanVien <= 0)
            {
                message = "Không xác định được nhân viên lập phiếu.";
                return false;
            }

            if (chiTiet.Count == 0)
            {
                message = "Phiếu nhập chưa có sản phẩm nào.";
                return false;
            }

            if (chiTiet.Any(x => x.SoLuong <= 0 || x.DonGia <= 0))
            {
                message = "Số lượng và đơn giá nhập phải lớn hơn 0.";
                return false;
            }

            PhieuNhapDTO phieuNhap = new PhieuNhapDTO
            {
                MaNhaCungCap = maNhaCungCap,
                MaNhanVien = maNhanVien,
                NgayNhap = DateTime.Now,
                TongTien = TinhTongTien(chiTiet),
                TrangThai = "DaXacNhan",
                GhiChu = string.IsNullOrWhiteSpace(ghiChu) ? null! : ghiChu.Trim()
            };

            List<ChiTietPhieuNhapDTO> chiTietDTO = chiTiet
                .Select(x => new ChiTietPhieuNhapDTO
                {
                    MaSanPham = x.MaSanPham,
                    SoLuong = x.SoLuong,
                    DonGiaNhap = x.DonGia,
                    ThanhTien = x.ThanhTien
                })
                .ToList();

            try
            {
                int maPhieuNhap = nhapHangDAL.Insert(phieuNhap, chiTietDTO);
                message = $"Đã lưu phiếu nhập mã {maPhieuNhap}. Tồn kho đã được cập nhật.";
                return true;
            }
            catch (Exception ex)
            {
                message = "Lưu phiếu nhập thất bại, dữ liệu đã được hoàn tác: " + ex.Message;
                return false;
            }
        }
    }
}
