
using System;

namespace ComputerStore.DTO.Entities
{
    public class TaiKhoanDTO
    {
        public int MaTaiKhoan { get; set; }
        public int MaNhanVien { get; set; }
        public string TenDangNhap { get; set; } = string.Empty;
        public string MatKhau { get; set; } = string.Empty;
        public string VaiTro { get; set; } = "NhanVien";
        public int TrangThai { get; set; } = 1;
        public DateTime NgayTao { get; set; }
    }
}
