namespace ComputerStore.DTO.Models
{
    /// <summary>
    /// Một dòng sản phẩm trên phiếu đang lập (giỏ hàng bán / danh sách nhập),
    /// chưa lưu vào DB. Có thêm TenSanPham để hiển thị trên lưới.
    /// Khi lưu, BLL chuyển sang ChiTietHoaDonDTO / ChiTietPhieuNhapDTO.
    /// </summary>
    public class DongChiTietModel
    {
        public int MaSanPham { get; set; }
        public string TenSanPham { get; set; } = string.Empty;
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien => SoLuong * DonGia;
    }
}
