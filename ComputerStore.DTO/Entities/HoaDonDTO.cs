
using System;

namespace ComputerStore.DTO.Entities
{
    public class HoaDonDTO
    {
        public int MaHoaDon { get; set; }
        public int? MaKhachHang { get; set; }
        public int MaNhanVien { get; set; }
        public DateTime NgayLap { get; set; }
        public decimal TongTien { get; set; }
        public string TrangThai { get; set; } = "ChoThanhToan";
        public string? GhiChu { get; set; }
    }
}
