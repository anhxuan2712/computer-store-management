using System.Data;
using ComputerStore.DAL;
using ComputerStore.DTO;

namespace ComputerStore.BLL
{
    public class BanHangBLL
    {
        private readonly BanHangDAL dal = new BanHangDAL();

        public DataTable GetKhachHang() => dal.GetKhachHang();
        public DataTable GetSanPham() => dal.GetSanPham();
        public DataTable GetDanhSachHoaDon() => dal.GetDanhSachHoaDon();

        /// <summary>
        /// Thêm sản phẩm vào giỏ hàng (chưa lưu DB). Kiểm tra tổng số lượng trong giỏ
        /// không vượt quá tồn kho hiện tại.
        /// </summary>
        public bool ThemVaoGio(IList<ChiTietHoaDonDTO> gioHang, string maSanPham, string tenSanPham,
            int soLuong, decimal donGiaBan, out string message)
        {
            if (string.IsNullOrWhiteSpace(maSanPham))
            {
                message = "Vui lòng chọn sản phẩm.";
                return false;
            }
            if (soLuong <= 0)
            {
                message = "Số lượng phải lớn hơn 0.";
                return false;
            }

            var existing = gioHang.FirstOrDefault(x => x.MaSanPham == maSanPham);
            int tongSoLuong = soLuong + (existing?.SoLuong ?? 0);
            int ton = dal.GetSoLuongTon(maSanPham);
            if (tongSoLuong > ton)
            {
                message = $"Không đủ hàng. Tồn kho hiện tại: {ton}, trong giỏ đã có: {existing?.SoLuong ?? 0}.";
                return false;
            }

            if (existing != null)
            {
                existing.SoLuong = tongSoLuong;
            }
            else
            {
                gioHang.Add(new ChiTietHoaDonDTO
                {
                    MaSanPham = maSanPham,
                    TenSanPham = tenSanPham,
                    SoLuong = soLuong,
                    DonGiaBan = donGiaBan
                });
            }
            message = string.Empty;
            return true;
        }

        public decimal TinhTongTien(IEnumerable<ChiTietHoaDonDTO> gioHang) => gioHang.Sum(x => x.ThanhTien);

        /// <summary>Thanh toán: lưu hóa đơn, chi tiết và trừ tồn kho trong một transaction.</summary>
        public bool ThanhToan(HoaDonDTO hoaDon, out string message)
        {
            if (string.IsNullOrWhiteSpace(hoaDon.MaNhanVien))
            {
                message = "Không xác định được nhân viên bán hàng.";
                return false;
            }
            if (hoaDon.ChiTiet.Count == 0)
            {
                message = "Giỏ hàng đang trống.";
                return false;
            }
            if (hoaDon.ChiTiet.Any(x => x.SoLuong <= 0))
            {
                message = "Số lượng sản phẩm phải lớn hơn 0.";
                return false;
            }

            hoaDon.NgayLap = DateTime.Now;
            hoaDon.TongTien = TinhTongTien(hoaDon.ChiTiet);
            hoaDon.MaKhachHang = string.IsNullOrWhiteSpace(hoaDon.MaKhachHang) ? null : hoaDon.MaKhachHang;
            hoaDon.GhiChu = string.IsNullOrWhiteSpace(hoaDon.GhiChu) ? null : hoaDon.GhiChu.Trim();

            if (hoaDon.TienKhachDua < hoaDon.TongTien)
            {
                message = $"Tiền khách đưa chưa đủ. Cần thanh toán {hoaDon.TongTien:N0} đ.";
                return false;
            }

            try
            {
                string ma = dal.Insert(hoaDon);
                decimal tienThoi = hoaDon.TienKhachDua - hoaDon.TongTien;
                message = $"Thanh toán thành công hóa đơn {ma}.\nTổng tiền: {hoaDon.TongTien:N0} đ\nTiền thối: {tienThoi:N0} đ";
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
