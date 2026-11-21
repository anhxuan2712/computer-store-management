using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ComputerStore.DAL.Repositories;
using ComputerStore.DTO.Entities;

namespace ComputerStore.BLL.Services
{
    public class NhanVienBLL
    {
        private readonly NhanVienDAL nhanVienDAL;

        public NhanVienBLL()
        {
            nhanVienDAL = new NhanVienDAL();
        }

        // 1. LAY DANH SACH NHAN VIEN
        public List<NhanVienDTO> GetAll()
        {
            return nhanVienDAL.GetAll();
        }

        // 2. TIM KIEM NHAN VIEN
        public List<NhanVienDTO> Search(string keyword)
        {
            keyword = keyword?.Trim() ?? string.Empty;
            return keyword.Length == 0
                ? nhanVienDAL.GetAll()
                : nhanVienDAL.Search(keyword);
        }

        // 3. TIM NHAN VIEN THEO MA
        public NhanVienDTO? GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Mã nhân viên không hợp lệ.");

            return nhanVienDAL.GetById(id);
        }

        // 4. THEM NHAN VIEN (TRA VE THONG BAO)
        public bool Insert(NhanVienDTO nhanVien, out string message)
        {
            if (!Validate(nhanVien, out message))
                return false;

            if (!nhanVienDAL.Insert(nhanVien))
            {
                message = "Không thể thêm nhân viên.";
                return false;
            }

            message = $"Đã thêm nhân viên mã {nhanVien.MaNhanVien}.";
            return true;
        }

        // Overload cu giu tuong thich nguoc
        public bool Insert(NhanVienDTO nhanVien)
        {
            if (!Insert(nhanVien, out string message))
                throw new ArgumentException(message);

            return true;
        }

        // 5. CAP NHAT NHAN VIEN (TRA VE THONG BAO)
        public bool Update(NhanVienDTO nhanVien, out string message)
        {
            if (nhanVien == null || nhanVien.MaNhanVien <= 0)
            {
                message = "Vui lòng chọn nhân viên cần sửa.";
                return false;
            }

            if (!Validate(nhanVien, out message))
                return false;

            if (!nhanVienDAL.Update(nhanVien))
            {
                message = "Không tìm thấy nhân viên để cập nhật.";
                return false;
            }

            message = "Cập nhật nhân viên thành công.";
            return true;
        }

        // Overload cu giu tuong thich nguoc
        public bool Update(NhanVienDTO nhanVien)
        {
            if (!Update(nhanVien, out string message))
                throw new ArgumentException(message);

            return true;
        }

        // 6. XOA NHAN VIEN (TRA VE THONG BAO)
        public bool Delete(int id, out string message)
        {
            if (id <= 0)
            {
                message = "Vui lòng chọn nhân viên cần xóa.";
                return false;
            }

            if (nhanVienDAL.HasRelatedRecords(id))
            {
                message = "Nhân viên đã có tài khoản hoặc chứng từ liên quan, không thể xóa. Hãy chuyển trạng thái sang 'Đã nghỉ việc'.";
                return false;
            }

            if (!nhanVienDAL.Delete(id))
            {
                message = "Không tìm thấy nhân viên để xóa.";
                return false;
            }

            message = "Đã xóa nhân viên.";
            return true;
        }

        // Overload cu giu tuong thich nguoc
        public bool Delete(int id)
        {
            if (!Delete(id, out string message))
                throw new ArgumentException(message);

            return true;
        }

        // 7. KIEM TRA DU LIEU NHAN VIEN
        private bool Validate(NhanVienDTO nhanVien, out string message)
        {
            if (nhanVien == null)
            {
                message = "Thông tin nhân viên không hợp lệ.";
                return false;
            }

            nhanVien.HoTen = nhanVien.HoTen?.Trim() ?? string.Empty;
            nhanVien.GioiTinh = string.IsNullOrWhiteSpace(nhanVien.GioiTinh) ? "Khac" : nhanVien.GioiTinh.Trim();
            nhanVien.SoDienThoai = string.IsNullOrWhiteSpace(nhanVien.SoDienThoai) ? null : nhanVien.SoDienThoai.Trim();
            nhanVien.Email = string.IsNullOrWhiteSpace(nhanVien.Email) ? null : nhanVien.Email.Trim();
            nhanVien.DiaChi = string.IsNullOrWhiteSpace(nhanVien.DiaChi) ? null : nhanVien.DiaChi.Trim();

            if (nhanVien.HoTen.Length == 0)
            {
                message = "Họ tên nhân viên không được để trống.";
                return false;
            }

            if (nhanVien.HoTen.Length > 100)
            {
                message = "Họ tên nhân viên không được vượt quá 100 ký tự.";
                return false;
            }

            if (nhanVien.GioiTinh != "Nam" && nhanVien.GioiTinh != "Nu" && nhanVien.GioiTinh != "Khac")
            {
                message = "Giới tính chỉ được chọn Nam, Nu hoặc Khac.";
                return false;
            }

            if (nhanVien.NgaySinh.HasValue && nhanVien.NgaySinh.Value.Date > DateTime.Today)
            {
                message = "Ngày sinh không được lớn hơn ngày hiện tại.";
                return false;
            }

            if (nhanVien.SoDienThoai != null)
            {
                if (!Regex.IsMatch(nhanVien.SoDienThoai, @"^0\d{9,10}$"))
                {
                    message = "Số điện thoại phải bắt đầu bằng 0 và gồm 10-11 chữ số.";
                    return false;
                }

                if (nhanVienDAL.ExistsPhone(nhanVien.SoDienThoai, nhanVien.MaNhanVien))
                {
                    message = "Số điện thoại này đã thuộc về nhân viên khác.";
                    return false;
                }
            }

            if (nhanVien.Email != null)
            {
                if (nhanVien.Email.Length > 100)
                {
                    message = "Email không được vượt quá 100 ký tự.";
                    return false;
                }

                if (!Regex.IsMatch(nhanVien.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    message = "Email không đúng định dạng.";
                    return false;
                }

                if (nhanVienDAL.ExistsEmail(nhanVien.Email, nhanVien.MaNhanVien))
                {
                    message = "Email này đã thuộc về nhân viên khác.";
                    return false;
                }
            }

            if (nhanVien.DiaChi != null && nhanVien.DiaChi.Length > 255)
            {
                message = "Địa chỉ không được vượt quá 255 ký tự.";
                return false;
            }

            if (nhanVien.TrangThai != 0 && nhanVien.TrangThai != 1)
            {
                message = "Trạng thái chỉ được nhận 0 (Đã nghỉ) hoặc 1 (Đang làm).";
                return false;
            }

            message = string.Empty;
            return true;
        }
    }
}
