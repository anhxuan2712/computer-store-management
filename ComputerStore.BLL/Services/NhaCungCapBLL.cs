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
                throw new ArgumentException("Ma nha cung cap khong hop le.");
            return repository.Update(item);
        }

        public bool Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Ma nha cung cap khong hop le.");
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
                throw new ArgumentException("Ten nha cung cap bat buoc va toi da 150 ky tu.");
            if (item.SoDienThoai?.Length > 15)
                throw new ArgumentException("So dien thoai khong duoc vuot qua 15 ky tu.");
            if (item.Email?.Length > 100)
                throw new ArgumentException("Email khong duoc vuot qua 100 ky tu.");
            if (item.DiaChi?.Length > 255)
                throw new ArgumentException("Dia chi khong duoc vuot qua 255 ky tu.");
            if (item.TrangThai is not (0 or 1))
                throw new ArgumentException("Trang thai chi duoc nhan 0 hoac 1.");
        }

        private static string? Normalize(string? value)
        {
            return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
        }
    }
}
