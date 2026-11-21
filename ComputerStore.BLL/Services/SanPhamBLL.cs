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
                throw new ArgumentException("Mã sản phẩm không hợp lệ.");
            return repository.Update(item);
        }

        public bool Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Mã sản phẩm không hợp lệ.");
            if (repository.HasTransactions(id))
                throw new InvalidOperationException("Sản phẩm đã phát sinh phiếu nhập hoặc hóa đơn, không thể xóa. Hãy chuyển sang trạng thái 'Ngừng hoạt động'.");
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
                throw new ArgumentException("Tên sản phẩm bắt buộc và tối đa 150 ký tự.");
            if (item.MaDanhMuc.Length == 0 || !danhMucRepository.GetAll().Any(x => x.MaDanhMuc == item.MaDanhMuc))
                throw new ArgumentException("Danh mục không tồn tại. Vui lòng tải lại danh mục.");
            if (item.DonViTinh.Length == 0 || item.DonViTinh.Length > 30)
                throw new ArgumentException("Đơn vị tính bắt buộc và tối đa 30 ký tự.");
            if (item.GiaNhap < 0 || item.GiaBan < 0)
                throw new ArgumentException("Giá nhập và giá bán không được âm.");
            if (item.TrangThai is not (0 or 1))
                throw new ArgumentException("Trạng thái chỉ được nhận 0 hoặc 1.");
        }
    }
}
