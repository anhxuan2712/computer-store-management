using System.Data;
using ComputerStore.DAL;
using ComputerStore.DTO;

namespace ComputerStore.BLL
{
    public class NhapHangBLL
    {
        private readonly NhapHangDAL dal = new NhapHangDAL();

        public DataTable GetNhaCungCap() => dal.GetNhaCungCap();
        public DataTable GetSanPham() => dal.GetSanPham();
        public DataTable GetDanhSachPhieuNhap() => dal.GetDanhSachPhieuNhap();
        public DataTable GetChiTietPhieuNhap(string maPhieuNhap) => dal.GetChiTietPhieuNhap(maPhieuNhap);

        /// <summary>
        /// Thêm một sản phẩm vào danh sách chi tiết (chưa lưu DB).
        /// Nếu sản phẩm đã có thì cộng dồn số lượng và lấy đơn giá mới.
        /// </summary>
        public bool ThemChiTiet(IList<ChiTietPhieuNhapDTO> chiTiet, string maSanPham, string tenSanPham,
            int soLuong, decimal donGiaNhap, out string message)
        {
            if (string.IsNullOrWhiteSpace(maSanPham))
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

            var existing = chiTiet.FirstOrDefault(x => x.MaSanPham == maSanPham);
            if (existing != null)
            {
                existing.SoLuong += soLuong;
                existing.DonGiaNhap = donGiaNhap;
            }
            else
            {
                chiTiet.Add(new ChiTietPhieuNhapDTO
                {
                    MaSanPham = maSanPham,
                    TenSanPham = tenSanPham,
                    SoLuong = soLuong,
                    DonGiaNhap = donGiaNhap
                });
            }
            message = string.Empty;
            return true;
        }

        public decimal TinhTongTien(IEnumerable<ChiTietPhieuNhapDTO> chiTiet) => chiTiet.Sum(x => x.ThanhTien);

        /// <summary>Xác nhận phiếu nhập: lưu phiếu, chi tiết và tăng tồn kho.</summary>
        public bool XacNhanNhap(PhieuNhapDTO phieu, out string message)
        {
            if (string.IsNullOrWhiteSpace(phieu.MaNhaCungCap))
            {
                message = "Vui lòng chọn nhà cung cấp.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(phieu.MaNhanVien))
            {
                message = "Không xác định được nhân viên lập phiếu.";
                return false;
            }
            if (phieu.ChiTiet.Count == 0)
            {
                message = "Phiếu nhập chưa có sản phẩm nào.";
                return false;
            }
            if (phieu.ChiTiet.Any(x => x.SoLuong <= 0 || x.DonGiaNhap <= 0))
            {
                message = "Số lượng và đơn giá nhập phải lớn hơn 0.";
                return false;
            }

            phieu.NgayNhap = DateTime.Now;
            phieu.TongTien = TinhTongTien(phieu.ChiTiet);
            phieu.GhiChu = string.IsNullOrWhiteSpace(phieu.GhiChu) ? null : phieu.GhiChu.Trim();

            try
            {
                string ma = dal.Insert(phieu);
                message = $"Đã lưu phiếu nhập {ma}. Tồn kho đã được cập nhật.";
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
