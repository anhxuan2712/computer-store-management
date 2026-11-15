using System.Collections.Generic;
using System.Text.RegularExpressions;
using ComputerStore.DAL.Repositories;
using ComputerStore.DTO.Entities;

namespace ComputerStore.BLL.Services
{
    public class KhachHangBLL
    {
        private readonly KhachHangDAL khachHangDAL;

        public KhachHangBLL()
        {
            khachHangDAL = new KhachHangDAL();
        }

        // 1. LAY DANH SACH KHACH HANG
        public List<KhachHangDTO> GetAll()
        {
            return khachHangDAL.GetAll();
        }

        // 2. TIM KIEM (TU KHOA RONG THI TRA VE TAT CA)
        public List<KhachHangDTO> Search(string keyword)
        {
            keyword = keyword?.Trim() ?? string.Empty;

            return keyword.Length == 0
                ? khachHangDAL.GetAll()
                : khachHangDAL.Search(keyword);
        }

        // 3. THEM KHACH HANG
        public bool Insert(KhachHangDTO khachHang, out string message)
        {
            if (!Validate(khachHang, out message))
                return false;

            khachHang.MaKhachHang = khachHangDAL.Insert(khachHang);
            message = $"Đã thêm khách hàng mã {khachHang.MaKhachHang}.";
            return true;
        }

        // 4. CAP NHAT KHACH HANG
        public bool Update(KhachHangDTO khachHang, out string message)
        {
            if (khachHang.MaKhachHang <= 0)
            {
                message = "Vui lòng chọn khách hàng cần sửa.";
                return false;
            }

            if (!Validate(khachHang, out message))
                return false;

            if (!khachHangDAL.Update(khachHang))
            {
                message = "Không tìm thấy khách hàng để cập nhật.";
                return false;
            }

            message = "Cập nhật khách hàng thành công.";
            return true;
        }

        // 5. XOA KHACH HANG (KHONG CHO XOA NEU DA CO HOA DON)
        public bool Delete(int maKhachHang, out string message)
        {
            if (maKhachHang <= 0)
            {
                message = "Vui lòng chọn khách hàng cần xóa.";
                return false;
            }

            if (khachHangDAL.HasHoaDon(maKhachHang))
            {
                message = "Khách hàng đã có hóa đơn, không thể xóa.";
                return false;
            }

            if (!khachHangDAL.Delete(maKhachHang))
            {
                message = "Không tìm thấy khách hàng để xóa.";
                return false;
            }

            message = "Đã xóa khách hàng.";
            return true;
        }

        // 6. KIEM TRA DU LIEU KHACH HANG
        private bool Validate(KhachHangDTO khachHang, out string message)
        {
            khachHang.HoTen = khachHang.HoTen?.Trim() ?? string.Empty;
            khachHang.SoDienThoai = khachHang.SoDienThoai?.Trim() ?? string.Empty;
            khachHang.DiaChi = string.IsNullOrWhiteSpace(khachHang.DiaChi) ? null! : khachHang.DiaChi.Trim();
            khachHang.Email = string.IsNullOrWhiteSpace(khachHang.Email) ? null! : khachHang.Email.Trim();

            if (khachHang.HoTen.Length == 0)
            {
                message = "Tên khách hàng không được để trống.";
                return false;
            }

            if (khachHang.HoTen.Length > 100)
            {
                message = "Tên khách hàng tối đa 100 ký tự.";
                return false;
            }

            if (!Regex.IsMatch(khachHang.SoDienThoai, @"^0\d{9,10}$"))
            {
                message = "Số điện thoại phải bắt đầu bằng 0 và gồm 10-11 chữ số.";
                return false;
            }

            if (khachHang.Email != null)
            {
                if (khachHang.Email.Length > 100)
                {
                    message = "Email tối đa 100 ký tự.";
                    return false;
                }

                if (!Regex.IsMatch(khachHang.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    message = "Email không hợp lệ.";
                    return false;
                }
            }

            if (khachHang.DiaChi != null && khachHang.DiaChi.Length > 255)
            {
                message = "Địa chỉ tối đa 255 ký tự.";
                return false;
            }

            if (khachHangDAL.ExistsPhone(khachHang.SoDienThoai, khachHang.MaKhachHang))
            {
                message = "Số điện thoại đã được dùng cho khách hàng khác.";
                return false;
            }

            message = string.Empty;
            return true;
        }
    }
}
