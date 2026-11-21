using ComputerStore.DAL.Repositories;
using ComputerStore.DTO.Entities;

namespace ComputerStore.BLL.Services
{
    public class NhaCungCapBLL
    {
        private readonly NhaCungCapDAL repository = new();

        public List<NhaCungCapDTO> GetAll() => repository.GetAll();

        public List<NhaCungCapDTO> Search(string keyword)
        {
            return repository.Search(keyword?.Trim() ?? string.Empty);
        }

        public bool Insert(NhaCungCapDTO item)
        {
            Validate(item);
            return repository.Insert(item);
        }

        public bool Update(NhaCungCapDTO item)
        {
            Validate(item);
            if (item.MaNhaCungCap <= 0)
                throw new ArgumentException("Mã nhà cung cấp không hợp lệ.");
            return repository.Update(item);
        }

        public bool Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Mã nhà cung cấp không hợp lệ.");
            if (repository.HasPhieuNhap(id))
                throw new InvalidOperationException("Nhà cung cấp đã có phiếu nhập, không thể xóa. Hãy chuyển sang trạng thái 'Ngừng hoạt động'.");
            return repository.Delete(id);
        }

        private static void Validate(NhaCungCapDTO item)
        {
            ArgumentNullException.ThrowIfNull(item);
            item.TenNhaCungCap = item.TenNhaCungCap.Trim();
            item.SoDienThoai = Normalize(item.SoDienThoai);
            item.Email = Normalize(item.Email);
            item.DiaChi = Normalize(item.DiaChi);
            if (item.TenNhaCungCap.Length == 0 || item.TenNhaCungCap.Length > 150)
                throw new ArgumentException("Tên nhà cung cấp bắt buộc và tối đa 150 ký tự.");
            if (item.SoDienThoai?.Length > 15)
                throw new ArgumentException("Số điện thoại không được vượt quá 15 ký tự.");
            if (item.Email?.Length > 100)
                throw new ArgumentException("Email không được vượt quá 100 ký tự.");
            if (item.DiaChi?.Length > 255)
                throw new ArgumentException("Địa chỉ không được vượt quá 255 ký tự.");
            if (item.TrangThai is not (0 or 1))
                throw new ArgumentException("Trạng thái chỉ được nhận 0 hoặc 1.");
        }

        private static string? Normalize(string? value)
        {
            return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
        }
    }
}
