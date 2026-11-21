using System.Drawing;
using ComputerStore.BLL.Services;
using ComputerStore.DTO.Entities;
using ComputerStore.GUI.Helpers;

namespace ComputerStore.GUI.Forms.Products
{
    public class FrmSanPham : Form
    {
        private readonly SanPhamBLL service = new();
        private readonly DanhMucBLL categoryService = new();
        private readonly DataGridView grid = ManagementFormControls.CreateGrid();
        private readonly TextBox txtSearch = new();
        private readonly TextBox txtName = new();
        private readonly TextBox txtUnit = new() { Text = "Cái" };
        private readonly TextBox txtImportPrice = new();
        private readonly TextBox txtSalePrice = new();
        private readonly TextBox txtDescription = new();
        private readonly ComboBox cboCategory = new() { DropDownStyle = ComboBoxStyle.DropDownList };
        private readonly ComboBox cboStatus = ManagementFormControls.CreateStatusCombo();
        private int? selectedId;

        public FrmSanPham()
        {
            Text = "Quản lý sản phẩm";
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(1120, 700);
            MinimumSize = new Size(900, 600);
            BuildLayout();
            grid.SelectionChanged += Grid_SelectionChanged;
            Load += (_, _) => LoadData();
        }

        private void BuildLayout()
        {
            txtName.MaxLength = 150;
            txtUnit.MaxLength = 30;
            txtDescription.MaxLength = 255;
            txtSearch.Width = 220;
            cboCategory.DropDownStyle = ComboBoxStyle.DropDownList;

            var toolbar = ManagementFormControls.CreateToolbar();
            toolbar.Controls.Add(new Label { Text = "Tìm kiếm:", AutoSize = true, Padding = new Padding(0, 10, 0, 0) });
            toolbar.Controls.Add(txtSearch);
            var search = ManagementFormControls.CreateButton("Tìm kiếm");
            search.Click += (_, _) => LoadData(txtSearch.Text);
            toolbar.Controls.Add(search);
            var refresh = ManagementFormControls.CreateButton("Tải lại");
            refresh.Click += (_, _) => { txtSearch.Clear(); LoadData(); ClearFields(); };
            toolbar.Controls.Add(refresh);
            var add = ManagementFormControls.CreateButton("Thêm");
            add.Click += Add_Click;
            toolbar.Controls.Add(add);
            var update = ManagementFormControls.CreateButton("Cập nhật");
            update.Click += Update_Click;
            toolbar.Controls.Add(update);
            var delete = ManagementFormControls.CreateButton("Xóa");
            delete.Click += Delete_Click;
            toolbar.Controls.Add(delete);
            var clear = ManagementFormControls.CreateButton("Làm mới form");
            clear.Click += (_, _) => ClearFields();
            toolbar.Controls.Add(clear);

            var editor = ManagementFormControls.CreateToolbar();
            editor.Controls.Add(ManagementFormControls.CreateField("Tên sản phẩm *", txtName, 210));
            editor.Controls.Add(ManagementFormControls.CreateField("Danh mục *", cboCategory, 190));
            editor.Controls.Add(ManagementFormControls.CreateField("Đơn vị tính *", txtUnit, 130));
            editor.Controls.Add(ManagementFormControls.CreateField("Giá nhập *", txtImportPrice, 150));
            editor.Controls.Add(ManagementFormControls.CreateField("Giá bán *", txtSalePrice, 150));
            editor.Controls.Add(ManagementFormControls.CreateField("Trạng thái", cboStatus, 150));
            editor.Controls.Add(ManagementFormControls.CreateField("Mô tả", txtDescription, 280));

            var root = ManagementFormControls.CreateRoot();
            root.Controls.Add(toolbar, 0, 0);
            root.Controls.Add(editor, 0, 1);
            root.Controls.Add(grid, 0, 2);
            Controls.Add(root);
        }

        private void LoadData(string keyword = "")
        {
            try
            {
                cboCategory.DataSource = categoryService.GetAll();
                cboCategory.DisplayMember = nameof(DanhMucDTO.TenDanhMuc);
                cboCategory.ValueMember = nameof(DanhMucDTO.MaDanhMuc);
                grid.DataSource = service.Search(keyword);
                ManagementFormControls.SetColumnHeader(grid, nameof(SanPhamDTO.MaSanPham), "Mã SP");
                ManagementFormControls.SetColumnHeader(grid, nameof(SanPhamDTO.TenSanPham), "Tên sản phẩm");
                ManagementFormControls.SetColumnHeader(grid, nameof(SanPhamDTO.MaDanhMuc), "Mã DM");
                ManagementFormControls.SetColumnHeader(grid, nameof(SanPhamDTO.TenDanhMuc), "Danh mục");
                ManagementFormControls.SetColumnHeader(grid, nameof(SanPhamDTO.DonViTinh), "Đơn vị");
                ManagementFormControls.SetColumnHeader(grid, nameof(SanPhamDTO.GiaNhap), "Giá nhập");
                ManagementFormControls.SetColumnHeader(grid, nameof(SanPhamDTO.GiaBan), "Giá bán");
                ManagementFormControls.SetColumnHeader(grid, nameof(SanPhamDTO.SoLuongTon), "Tồn kho");
                ManagementFormControls.SetColumnHeader(grid, nameof(SanPhamDTO.MoTa), "Mô tả");
                ManagementFormControls.SetColumnHeader(grid, nameof(SanPhamDTO.TrangThai), "Trạng thái");
                ManagementFormControls.SetColumnHeader(grid, nameof(SanPhamDTO.NgayTao), "Ngày tạo");
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
    }
}
