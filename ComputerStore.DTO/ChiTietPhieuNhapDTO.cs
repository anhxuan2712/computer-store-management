namespace ComputerStore.DTO
{
    public class ChiTietPhieuNhapDTO
    {
        public string MaPhieuNhap { get; set; } = string.Empty;
        public string MaSanPham { get; set; } = string.Empty;
        /// <summary>Chỉ dùng để hiển thị, không lưu vào bảng ChiTietPhieuNhap.</summary>
        public string TenSanPham { get; set; } = string.Empty;
        public int SoLuong { get; set; }
        public decimal DonGiaNhap { get; set; }
        public decimal ThanhTien => SoLuong * DonGiaNhap;
    }
}
