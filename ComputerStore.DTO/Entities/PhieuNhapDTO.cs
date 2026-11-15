
using System;

namespace ComputerStore.DTO.Entities
{
    public class PhieuNhapDTO
    {
        public int MaPhieuNhap { get; set; }
        public int MaNhaCungCap { get; set; }
        public int MaNhanVien { get; set; }
        public DateTime NgayNhap { get; set; }
        public decimal TongTien { get; set; }
        public string TrangThai { get; set; }
        public string GhiChu { get; set; }
    }
}
