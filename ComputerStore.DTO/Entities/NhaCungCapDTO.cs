
namespace ComputerStore.DTO.Entities
{
    public class NhaCungCapDTO
    {
        public int MaNhaCungCap { get; set; }
        public string TenNhaCungCap { get; set; } = string.Empty;
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }
        public string? DiaChi { get; set; }
        public int TrangThai { get; set; } = 1;
    }
}
