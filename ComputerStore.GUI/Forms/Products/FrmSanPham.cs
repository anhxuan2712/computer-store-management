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
        private readonly TextBox txtUnit = new() { Text = "Cai" };
        private readonly TextBox txtImportPrice = new();
        private readonly TextBox txtSalePrice = new();
        private readonly TextBox txtDescription = new();
        private readonly ComboBox cboCategory = new() { DropDownStyle = ComboBoxStyle.DropDownList };
        private readonly ComboBox cboStatus = ManagementFormControls.CreateStatusCombo();
        private int? selectedId;

        public FrmSanPham()
        {
            Text = "Quan ly san pham";
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(1120, 700);
            MinimumSize = new Size(900, 600);
            BuildLayout();
            grid.SelectionChanged += Grid_SelectionChanged;
            LoadData();
        }

        private void BuildLayout()
        {
            txtName.MaxLength = 150;
            txtUnit.MaxLength = 30;
            txtDescription.MaxLength = 255;
            txtSearch.Width = 220;
            cboCategory.DropDownStyle = ComboBoxStyle.DropDownList;

            var toolbar = ManagementFormControls.CreateToolbar();
            toolbar.Controls.Add(new Label { Text = "Tim kiem:", AutoSize = true, Padding = new Padding(0, 10, 0, 0) });
            toolbar.Controls.Add(txtSearch);
            var search = ManagementFormControls.CreateButton("Tim kiem");
            search.Click += (_, _) => LoadData(txtSearch.Text);
            toolbar.Controls.Add(search);
            var refresh = ManagementFormControls.CreateButton("Tai lai");
            refresh.Click += (_, _) => LoadData();
            toolbar.Controls.Add(refresh);
            var add = ManagementFormControls.CreateButton("Them");
            add.Click += Add_Click;
            toolbar.Controls.Add(add);
            var update = ManagementFormControls.CreateButton("Cap nhat");
            update.Click += Update_Click;
            toolbar.Controls.Add(update);
            var delete = ManagementFormControls.CreateButton("Xoa");
            delete.Click += Delete_Click;
            toolbar.Controls.Add(delete);
            var clear = ManagementFormControls.CreateButton("Lam moi form");
            clear.Click += (_, _) => ClearFields();
            toolbar.Controls.Add(clear);

            var editor = ManagementFormControls.CreateToolbar();
            editor.Controls.Add(ManagementFormControls.CreateField("Ten san pham *", txtName, 210));
            editor.Controls.Add(ManagementFormControls.CreateField("Danh muc *", cboCategory, 190));
            editor.Controls.Add(ManagementFormControls.CreateField("Don vi tinh *", txtUnit, 130));
            editor.Controls.Add(ManagementFormControls.CreateField("Gia nhap *", txtImportPrice, 150));
            editor.Controls.Add(ManagementFormControls.CreateField("Gia ban *", txtSalePrice, 150));
            editor.Controls.Add(ManagementFormControls.CreateField("Trang thai", cboStatus, 150));
            editor.Controls.Add(ManagementFormControls.CreateField("Mo ta", txtDescription, 280));

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
                ManagementFormControls.SetColumnHeader(grid, nameof(SanPhamDTO.MaSanPham), "Ma san pham");
                ManagementFormControls.SetColumnHeader(grid, nameof(SanPhamDTO.TenSanPham), "Ten san pham");
                ManagementFormControls.SetColumnHeader(grid, nameof(SanPhamDTO.MaDanhMuc), "Ma danh muc");
                ManagementFormControls.SetColumnHeader(grid, nameof(SanPhamDTO.TenDanhMuc), "Danh muc");
                ManagementFormControls.SetColumnHeader(grid, nameof(SanPhamDTO.DonViTinh), "Don vi");
                ManagementFormControls.SetColumnHeader(grid, nameof(SanPhamDTO.GiaNhap), "Gia nhap");
                ManagementFormControls.SetColumnHeader(grid, nameof(SanPhamDTO.GiaBan), "Gia ban");
                ManagementFormControls.SetColumnHeader(grid, nameof(SanPhamDTO.SoLuongTon), "Ton kho");
                ManagementFormControls.SetColumnHeader(grid, nameof(SanPhamDTO.MoTa), "Mo ta");
                ManagementFormControls.SetColumnHeader(grid, nameof(SanPhamDTO.TrangThai), "Trang thai (1=hoat dong)");
                ManagementFormControls.SetColumnHeader(grid, nameof(SanPhamDTO.NgayTao), "Ngay tao");
                if (grid.Columns.Contains(nameof(SanPhamDTO.GiaNhap)))
                    grid.Columns[nameof(SanPhamDTO.GiaNhap)].DefaultCellStyle.Format = "N0";
                if (grid.Columns.Contains(nameof(SanPhamDTO.GiaBan)))
                    grid.Columns[nameof(SanPhamDTO.GiaBan)].DefaultCellStyle.Format = "N0";
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
                    throw new InvalidOperationException("Hay tao danh muc truoc khi them san pham.");
                bool saved = service.Insert(BuildProduct());
                if (!saved) throw new InvalidOperationException("Khong them duoc san pham.");
                LoadData(txtSearch.Text);
                ClearFields();
                MessageBox.Show(this, "Them san pham thanh cong.", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void Update_Click(object? sender, EventArgs e)
        {
            try
            {
                if (selectedId is null) throw new InvalidOperationException("Hay chon san pham can cap nhat.");
                SanPhamDTO item = BuildProduct();
                item.MaSanPham = selectedId.Value;
                if (!service.Update(item)) throw new InvalidOperationException("Khong tim thay san pham de cap nhat.");
                LoadData(txtSearch.Text);
                ClearFields();
                MessageBox.Show(this, "Cap nhat san pham thanh cong.", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void Delete_Click(object? sender, EventArgs e)
        {
            try
            {
                if (selectedId is null) throw new InvalidOperationException("Hay chon san pham can xoa.");
                if (MessageBox.Show(this, "Ban chac chan muon xoa san pham da chon?", "Xac nhan", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
                if (!service.Delete(selectedId.Value)) throw new InvalidOperationException("Khong tim thay san pham de xoa.");
                LoadData(txtSearch.Text);
                ClearFields();
                MessageBox.Show(this, "Xoa san pham thanh cong.", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private SanPhamDTO BuildProduct()
        {
            if (!decimal.TryParse(txtImportPrice.Text.Trim(), out decimal importPrice))
                throw new ArgumentException("Gia nhap phai la so hop le.");
            if (!decimal.TryParse(txtSalePrice.Text.Trim(), out decimal salePrice))
                throw new ArgumentException("Gia ban phai la so hop le.");
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
            if (grid.CurrentRow?.DataBoundItem is not SanPhamDTO item) return;
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
            txtUnit.Text = "Cai";
            txtImportPrice.Clear();
            txtSalePrice.Clear();
            txtDescription.Clear();
            cboStatus.SelectedIndex = 0;
            grid.ClearSelection();
        }

        private void ShowError(Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Loi quan ly san pham", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
