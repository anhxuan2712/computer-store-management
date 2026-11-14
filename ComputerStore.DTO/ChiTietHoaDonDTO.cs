namespace ComputerStore.DTO
{
    public class ChiTietHoaDonDTO
    {
        public string MaHoaDon { get; set; } = string.Empty;
        public string MaSanPham { get; set; } = string.Empty;
        /// <summary>Chỉ dùng để hiển thị, không lưu vào bảng ChiTietHoaDon.</summary>
        public string TenSanPham { get; set; } = string.Empty;
        public int SoLuong { get; set; }
        public decimal DonGiaBan { get; set; }
        public decimal ThanhTien => SoLuong * DonGiaBan;
    }
}
