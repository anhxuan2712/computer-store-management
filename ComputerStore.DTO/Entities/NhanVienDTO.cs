
using System;

namespace ComputerStore.DTO.Entities
{
    public class NhanVienDTO
    {
        public int MaNhanVien { get; set; }
        public string HoTen { get; set; } = string.Empty;
        public string? GioiTinh { get; set; } = "Khac";
        public DateTime? NgaySinh { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }
        public string? DiaChi { get; set; }
        public DateTime? NgayVaoLam { get; set; }
        public int TrangThai { get; set; } = 1;
        public DateTime NgayTao { get; set; }
    }
}
