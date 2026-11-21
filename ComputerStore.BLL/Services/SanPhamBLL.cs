using ComputerStore.DAL.Repositories;
using ComputerStore.DTO.Entities;

namespace ComputerStore.BLL.Services
{
    public class SanPhamBLL
    {
        private readonly SanPhamDAL repository = new();
        private readonly DanhMucDAL danhMucRepository = new();

        public List<SanPhamDTO> GetAll() => repository.GetAll();

        public List<SanPhamDTO> Search(string keyword)
        {
            return repository.Search(keyword?.Trim() ?? string.Empty);
        }

        public bool Insert(SanPhamDTO item)
        {
            Validate(item);
            return repository.Insert(item);
        }

        public bool Update(SanPhamDTO item)
        {
            Validate(item);
            if (item.MaSanPham <= 0)
                throw new ArgumentException("Ma san pham khong hop le.");
            return repository.Update(item);
        }

        public bool Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Ma san pham khong hop le.");
            return repository.Delete(id);
        }

        private void Validate(SanPhamDTO item)
        {
            ArgumentNullException.ThrowIfNull(item);
            item.TenSanPham = item.TenSanPham.Trim();
            item.MaDanhMuc = item.MaDanhMuc.Trim();
            item.DonViTinh = item.DonViTinh.Trim();
            item.MoTa = string.IsNullOrWhiteSpace(item.MoTa) ? null : item.MoTa.Trim();

            if (item.TenSanPham.Length == 0 || item.TenSanPham.Length > 150)
                throw new ArgumentException("Ten san pham bat buoc va toi da 150 ky tu.");
            if (item.MaDanhMuc.Length == 0 || !danhMucRepository.GetAll().Any(x => x.MaDanhMuc == item.MaDanhMuc))
                throw new ArgumentException("Danh muc khong ton tai. Vui long tai lai danh muc.");
            if (item.DonViTinh.Length == 0 || item.DonViTinh.Length > 30)
                throw new ArgumentException("Don vi tinh bat buoc va toi da 30 ky tu.");
            if (item.GiaNhap < 0 || item.GiaBan < 0)
                throw new ArgumentException("Gia nhap va gia ban khong duoc am.");
            if (item.TrangThai is not (0 or 1))
                throw new ArgumentException("Trang thai chi duoc nhan 0 hoac 1.");
        }
    }
}
