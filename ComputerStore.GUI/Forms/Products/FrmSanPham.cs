using ComputerStore.BLL.Services;
using ComputerStore.DTO.Entities;

namespace ComputerStore.GUI.Forms.Products
{
    public partial class FrmSanPham : Form
    {
        public FrmSanPham()
        {
            InitializeComponent();
        }

        /* =====================================================================
         * HƯỚNG DẪN NỐI LẠI SAU KHI KÉO THẢ UI (Visual Studio Designer):
         * Đặt thuộc tính (Name) cho các control như sau, gán sự kiện tương ứng
         * rồi bỏ comment khối code bên dưới:
         *   - TextBox      : txtSearch, txtName (MaxLength=150), txtUnit (MaxLength=30),
         *                    txtImportPrice, txtSalePrice, txtDescription (MaxLength=255)
         *   - ComboBox     : cboCategory (DropDownStyle=DropDownList),
         *                    cboStatus   (DropDownStyle=DropDownList; Items: "Hoạt động", "Ngừng hoạt động")
         *   - Button       : btnTimKiem -> Click: BtnTimKiem_Click
         *                    btnTaiLai  -> Click: BtnTaiLai_Click
         *                    btnThem    -> Click: Add_Click
         *                    btnSua     -> Click: Update_Click
         *                    btnXoa     -> Click: Delete_Click
         *                    btnLamMoi  -> Click: BtnLamMoi_Click
         *   - DataGridView : grid       -> SelectionChanged: Grid_SelectionChanged
         *   - Form         : FrmSanPham -> Load: FrmSanPham_Load
         * =====================================================================

        private readonly SanPhamBLL service = new();
        private readonly DanhMucBLL categoryService = new();
        private int? selectedId;

        private void FrmSanPham_Load(object? sender, EventArgs e)
        {
            cboStatus.Items.Clear();
            cboStatus.Items.AddRange(new object[] { "Hoạt động", "Ngừng hoạt động" });
            cboStatus.SelectedIndex = 0;
            txtUnit.Text = "Cái";
            LoadData();
        }

        private void BtnTimKiem_Click(object? sender, EventArgs e) => LoadData(txtSearch.Text);

        private void BtnTaiLai_Click(object? sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadData();
            ClearFields();
        }

        private void BtnLamMoi_Click(object? sender, EventArgs e) => ClearFields();

        private void LoadData(string keyword = "")
        {
            try
            {
                cboCategory.DataSource = categoryService.GetAll();
                cboCategory.DisplayMember = nameof(DanhMucDTO.TenDanhMuc);
                cboCategory.ValueMember = nameof(DanhMucDTO.MaDanhMuc);
                grid.DataSource = service.Search(keyword);
                SetColumnHeader(nameof(SanPhamDTO.MaSanPham), "Mã SP");
                SetColumnHeader(nameof(SanPhamDTO.TenSanPham), "Tên sản phẩm");
                SetColumnHeader(nameof(SanPhamDTO.MaDanhMuc), "Mã DM");
                SetColumnHeader(nameof(SanPhamDTO.TenDanhMuc), "Danh mục");
                SetColumnHeader(nameof(SanPhamDTO.DonViTinh), "Đơn vị");
                SetColumnHeader(nameof(SanPhamDTO.GiaNhap), "Giá nhập");
                SetColumnHeader(nameof(SanPhamDTO.GiaBan), "Giá bán");
                SetColumnHeader(nameof(SanPhamDTO.SoLuongTon), "Tồn kho");
                SetColumnHeader(nameof(SanPhamDTO.MoTa), "Mô tả");
                SetColumnHeader(nameof(SanPhamDTO.TrangThai), "Trạng thái");
                SetColumnHeader(nameof(SanPhamDTO.NgayTao), "Ngày tạo");
                if (grid.Columns[nameof(SanPhamDTO.GiaNhap)] is DataGridViewColumn colGiaNhap)
                    colGiaNhap.DefaultCellStyle.Format = "N0";
                if (grid.Columns[nameof(SanPhamDTO.GiaBan)] is DataGridViewColumn colGiaBan)
                    colGiaBan.DefaultCellStyle.Format = "N0";
                grid.ClearSelection();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void SetColumnHeader(string property, string title)
        {
            if (grid.Columns[property] is DataGridViewColumn col)
                col.HeaderText = title;
        }

        private void Add_Click(object? sender, EventArgs e)
        {
            try
            {
                if (cboCategory.SelectedValue == null)
                    throw new InvalidOperationException("Hãy tạo danh mục trước khi thêm sản phẩm.");
                bool saved = service.Insert(BuildProduct());
                if (!saved) throw new InvalidOperationException("Không thêm được sản phẩm.");
                LoadData(txtSearch.Text);
                ClearFields();
                MessageBox.Show(this, "Thêm sản phẩm thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void Update_Click(object? sender, EventArgs e)
        {
            try
            {
                if (selectedId is null) throw new InvalidOperationException("Hãy chọn sản phẩm cần cập nhật.");
                SanPhamDTO item = BuildProduct();
                item.MaSanPham = selectedId.Value;
                if (!service.Update(item)) throw new InvalidOperationException("Không tìm thấy sản phẩm để cập nhật.");
                LoadData(txtSearch.Text);
                ClearFields();
                MessageBox.Show(this, "Cập nhật sản phẩm thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void Delete_Click(object? sender, EventArgs e)
        {
            try
            {
                if (selectedId is null) throw new InvalidOperationException("Hãy chọn sản phẩm cần xóa.");
                if (MessageBox.Show(this, "Bạn chắc chắn muốn xóa sản phẩm đã chọn?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
                if (!service.Delete(selectedId.Value)) throw new InvalidOperationException("Không tìm thấy sản phẩm để xóa.");
                LoadData(txtSearch.Text);
                ClearFields();
                MessageBox.Show(this, "Xóa sản phẩm thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private SanPhamDTO BuildProduct()
        {
            if (!decimal.TryParse(txtImportPrice.Text.Trim(), out decimal importPrice))
                throw new ArgumentException("Giá nhập phải là số hợp lệ.");
            if (!decimal.TryParse(txtSalePrice.Text.Trim(), out decimal salePrice))
                throw new ArgumentException("Giá bán phải là số hợp lệ.");
            return new SanPhamDTO
            {
                TenSanPham = txtName.Text,
                MaDanhMuc = Convert.ToString(cboCategory.SelectedValue) ?? string.Empty,
                DonViTinh = txtUnit.Text,
                GiaNhap = importPrice,
                GiaBan = salePrice,
                MoTa = txtDescription.Text,
                TrangThai = cboStatus.SelectedIndex == 0 ? 1 : 0
            };
        }

        private void Grid_SelectionChanged(object? sender, EventArgs e)
        {
            if (grid.CurrentRow?.DataBoundItem is not SanPhamDTO item || !grid.CurrentRow.Selected) return;
            selectedId = item.MaSanPham;
            txtName.Text = item.TenSanPham;
            txtUnit.Text = item.DonViTinh;
            txtImportPrice.Text = item.GiaNhap.ToString("0.##");
            txtSalePrice.Text = item.GiaBan.ToString("0.##");
            txtDescription.Text = item.MoTa ?? string.Empty;
            cboCategory.SelectedValue = item.MaDanhMuc;
            cboStatus.SelectedIndex = item.TrangThai == 1 ? 0 : 1;
        }

        private void ClearFields()
        {
            selectedId = null;
            txtName.Clear();
            txtUnit.Text = "Cái";
            txtImportPrice.Clear();
            txtSalePrice.Clear();
            txtDescription.Clear();
            cboStatus.SelectedIndex = 0;
            grid.ClearSelection();
        }

        private void ShowError(Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Lỗi quản lý sản phẩm", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        */
    }
}
