using ComputerStore.DAL.Repositories;
using ComputerStore.DTO.Entities;

namespace ComputerStore.BLL.Services
{
    public class DanhMucBLL
    {
        private readonly DanhMucDAL repository = new();

        public List<DanhMucDTO> GetAll() => repository.GetAll();

        public List<DanhMucDTO> Search(string keyword)
        {
            return repository.Search(keyword?.Trim() ?? string.Empty);
        }

        public bool Insert(DanhMucDTO item)
        {
            Validate(item);
            return repository.Insert(item);
        }

        public bool Update(DanhMucDTO item)
        {
            Validate(item);
            if (string.IsNullOrWhiteSpace(item.MaDanhMuc))
                throw new ArgumentException("Vui long chon danh muc can cap nhat.");
            return repository.Update(item);
        }

        public bool Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Ma danh muc khong hop le.");
            return repository.Delete(id.Trim());
        }

        private static void Validate(DanhMucDTO item)
        {
            ArgumentNullException.ThrowIfNull(item);
            item.MaDanhMuc = item.MaDanhMuc.Trim();
            item.TenDanhMuc = item.TenDanhMuc.Trim();
            item.MoTa = string.IsNullOrWhiteSpace(item.MoTa) ? null : item.MoTa.Trim();
            if (item.MaDanhMuc.Length == 0 || item.MaDanhMuc.Length > 10)
                throw new ArgumentException("Ma danh muc bat buoc va toi da 10 ky tu.");
            if (item.TenDanhMuc.Length == 0 || item.TenDanhMuc.Length > 100)
                throw new ArgumentException("Ten danh muc bat buoc va toi da 100 ky tu.");
            if (item.MoTa?.Length > 255)
                throw new ArgumentException("Mo ta khong duoc vuot qua 255 ky tu.");
        }
    }
}
