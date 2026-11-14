using System.Text.RegularExpressions;
using ComputerStore.DAL;
using ComputerStore.DTO;

namespace ComputerStore.BLL
{
    public class KhachHangBLL
    {
        private readonly KhachHangDAL dal = new KhachHangDAL();

        public List<KhachHangDTO> GetAll() => dal.GetAll();

        public List<KhachHangDTO> Search(string keyword)
        {
            keyword = keyword?.Trim() ?? string.Empty;
            return keyword.Length == 0 ? dal.GetAll() : dal.Search(keyword);
        }

        public bool Insert(KhachHangDTO kh, out string message)
        {
            if (!Validate(kh, isUpdate: false, out message)) return false;

            if (!dal.Insert(kh))
            {
                message = "Thêm khách hàng thất bại.";
                return false;
            }
            message = $"Đã thêm khách hàng {kh.MaKhachHang}.";
            return true;
        }

        public bool Update(KhachHangDTO kh, out string message)
        {
            if (string.IsNullOrWhiteSpace(kh.MaKhachHang))
            {
                message = "Vui lòng chọn khách hàng cần sửa.";
                return false;
            }
            if (!Validate(kh, isUpdate: true, out message)) return false;

            if (!dal.Update(kh))
            {
                message = "Không tìm thấy khách hàng để cập nhật.";
                return false;
            }
            message = "Cập nhật khách hàng thành công.";
            return true;
        }

        public bool Delete(string maKhachHang, out string message)
        {
            if (string.IsNullOrWhiteSpace(maKhachHang))
            {
                message = "Vui lòng chọn khách hàng cần xóa.";
                return false;
            }
            if (dal.HasHoaDon(maKhachHang))
            {
                message = "Khách hàng đã có hóa đơn, không thể xóa.";
                return false;
            }
            if (!dal.Delete(maKhachHang))
            {
                message = "Không tìm thấy khách hàng để xóa.";
                return false;
            }
            message = "Đã xóa khách hàng.";
            return true;
        }

        private bool Validate(KhachHangDTO kh, bool isUpdate, out string message)
        {
            kh.TenKhachHang = kh.TenKhachHang?.Trim() ?? string.Empty;
            kh.SoDienThoai = kh.SoDienThoai?.Trim() ?? string.Empty;
            kh.DiaChi = string.IsNullOrWhiteSpace(kh.DiaChi) ? null : kh.DiaChi.Trim();
            kh.Email = string.IsNullOrWhiteSpace(kh.Email) ? null : kh.Email.Trim();

            if (kh.TenKhachHang.Length == 0)
            {
                message = "Tên khách hàng không được để trống.";
                return false;
            }
            if (kh.TenKhachHang.Length > 100)
            {
                message = "Tên khách hàng tối đa 100 ký tự.";
                return false;
            }
            if (!Regex.IsMatch(kh.SoDienThoai, @"^0\d{9,10}$"))
            {
                message = "Số điện thoại phải bắt đầu bằng 0 và gồm 10-11 chữ số.";
                return false;
            }
            if (kh.Email != null && !Regex.IsMatch(kh.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                message = "Email không hợp lệ.";
                return false;
            }
            if (dal.ExistsPhone(kh.SoDienThoai, isUpdate ? kh.MaKhachHang : null))
            {
                message = "Số điện thoại đã được dùng cho khách hàng khác.";
                return false;
            }
            message = string.Empty;
            return true;
        }
    }
}
