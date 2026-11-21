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
            if (repository.ExistsId(item.MaDanhMuc))
                throw new ArgumentException("Mã danh mục đã tồn tại.");
            if (repository.ExistsName(item.TenDanhMuc))
                throw new ArgumentException("Tên danh mục đã tồn tại.");
            return repository.Insert(item);
        }

        public bool Update(DanhMucDTO item)
        {
            Validate(item);
            if (string.IsNullOrWhiteSpace(item.MaDanhMuc))
                throw new ArgumentException("Vui lòng chọn danh mục cần cập nhật.");
            if (repository.ExistsName(item.TenDanhMuc, item.MaDanhMuc))
                throw new ArgumentException("Tên danh mục đã được dùng cho danh mục khác.");
            return repository.Update(item);
        }

        public bool Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Mã danh mục không hợp lệ.");
            string ma = id.Trim();
            if (repository.HasSanPham(ma))
                throw new InvalidOperationException("Danh mục đang có sản phẩm thuộc về, không thể xóa.");
            return repository.Delete(ma);
        }

        private static void Validate(DanhMucDTO item)
        {
            ArgumentNullException.ThrowIfNull(item);
            item.MaDanhMuc = item.MaDanhMuc.Trim();
            item.TenDanhMuc = item.TenDanhMuc.Trim();
            item.MoTa = string.IsNullOrWhiteSpace(item.MoTa) ? null : item.MoTa.Trim();
            if (item.MaDanhMuc.Length == 0 || item.MaDanhMuc.Length > 10)
                throw new ArgumentException("Mã danh mục bắt buộc và tối đa 10 ký tự.");
            if (item.TenDanhMuc.Length == 0 || item.TenDanhMuc.Length > 100)
                throw new ArgumentException("Tên danh mục bắt buộc và tối đa 100 ký tự.");
            if (item.MoTa?.Length > 255)
                throw new ArgumentException("Mô tả không được vượt quá 255 ký tự.");
        }
    }
}
