
using System;

namespace ComputerStore.DTO.Entities
{
    public class KhachHangDTO
    {
        public int MaKhachHang { get; set; }
        public string HoTen { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public DateTime NgayTao { get; set; }
    }
}
