namespace ComputerStore.DTO
{
    public class KhachHangDTO
    {
        public string MaKhachHang { get; set; } = string.Empty;
        public string TenKhachHang { get; set; } = string.Empty;
        public string SoDienThoai { get; set; } = string.Empty;
        public string? DiaChi { get; set; }
        public string? Email { get; set; }
    }
}
