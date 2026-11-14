namespace ComputerStore.DTO
{
    public class PhieuNhapDTO
    {
        public string MaPhieuNhap { get; set; } = string.Empty;
        public DateTime NgayNhap { get; set; } = DateTime.Now;
        public string MaNhaCungCap { get; set; } = string.Empty;
        public string MaNhanVien { get; set; } = string.Empty;
        public decimal TongTien { get; set; }
        public string? GhiChu { get; set; }
        public List<ChiTietPhieuNhapDTO> ChiTiet { get; set; } = new();
    }
}
