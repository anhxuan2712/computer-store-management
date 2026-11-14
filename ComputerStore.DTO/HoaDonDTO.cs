namespace ComputerStore.DTO
{
    public class HoaDonDTO
    {
        public string MaHoaDon { get; set; } = string.Empty;
        public DateTime NgayLap { get; set; } = DateTime.Now;
        /// <summary>NULL nếu là khách lẻ.</summary>
        public string? MaKhachHang { get; set; }
        public string MaNhanVien { get; set; } = string.Empty;
        public decimal TongTien { get; set; }
        public decimal TienKhachDua { get; set; }
        public string? GhiChu { get; set; }
        public List<ChiTietHoaDonDTO> ChiTiet { get; set; } = new();
    }
}
