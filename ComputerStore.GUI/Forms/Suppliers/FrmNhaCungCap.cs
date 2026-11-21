using System.Drawing;
using ComputerStore.BLL.Services;
using ComputerStore.DTO.Entities;
using ComputerStore.GUI.Helpers;

namespace ComputerStore.GUI.Forms.Suppliers
{
    public class FrmNhaCungCap : Form
    {
        private readonly NhaCungCapBLL service = new();
        private readonly DataGridView grid = ManagementFormControls.CreateGrid();
        private readonly TextBox txtSearch = new();
        private readonly TextBox txtName = new();
        private readonly TextBox txtPhone = new();
        private readonly TextBox txtEmail = new();
        private readonly TextBox txtAddress = new();
        private readonly ComboBox cboStatus = ManagementFormControls.CreateStatusCombo();
        private int? selectedId;

        public FrmNhaCungCap()
        {
            Text = "Quan ly nha cung cap";
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(1100, 680);
            MinimumSize = new Size(850, 560);
            BuildLayout();
            grid.SelectionChanged += Grid_SelectionChanged;
            LoadData();
        }

        private void BuildLayout()
        {
            txtName.MaxLength = 150;
            txtPhone.MaxLength = 15;
            txtEmail.MaxLength = 100;
            txtAddress.MaxLength = 255;
            txtSearch.Width = 220;

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
            editor.Controls.Add(ManagementFormControls.CreateField("Ten nha cung cap *", txtName, 230));
            editor.Controls.Add(ManagementFormControls.CreateField("So dien thoai", txtPhone, 160));
            editor.Controls.Add(ManagementFormControls.CreateField("Email", txtEmail, 220));
            editor.Controls.Add(ManagementFormControls.CreateField("Trang thai", cboStatus, 150));
            editor.Controls.Add(ManagementFormControls.CreateField("Dia chi", txtAddress, 320));
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
                grid.DataSource = service.Search(keyword);
                ManagementFormControls.SetColumnHeader(grid, nameof(NhaCungCapDTO.MaNhaCungCap), "Ma nha cung cap");
                ManagementFormControls.SetColumnHeader(grid, nameof(NhaCungCapDTO.TenNhaCungCap), "Ten nha cung cap");
                ManagementFormControls.SetColumnHeader(grid, nameof(NhaCungCapDTO.SoDienThoai), "So dien thoai");
                ManagementFormControls.SetColumnHeader(grid, nameof(NhaCungCapDTO.Email), "Email");
                ManagementFormControls.SetColumnHeader(grid, nameof(NhaCungCapDTO.DiaChi), "Dia chi");
                ManagementFormControls.SetColumnHeader(grid, nameof(NhaCungCapDTO.TrangThai), "Trang thai (1=hoat dong)");
                grid.ClearSelection();
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private NhaCungCapDTO BuildItem() => new()
        {
            TenNhaCungCap = txtName.Text,
            SoDienThoai = txtPhone.Text,
            Email = txtEmail.Text,
            DiaChi = txtAddress.Text,
            TrangThai = cboStatus.SelectedIndex == 0 ? 1 : 0
        };

        private void Add_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!service.Insert(BuildItem())) throw new InvalidOperationException("Khong them duoc nha cung cap.");
                LoadData(txtSearch.Text);
                ClearFields();
                MessageBox.Show(this, "Them nha cung cap thanh cong.", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void Update_Click(object? sender, EventArgs e)
        {
            try
            {
                if (selectedId is null) throw new InvalidOperationException("Hay chon nha cung cap can cap nhat.");
                NhaCungCapDTO item = BuildItem();
                item.MaNhaCungCap = selectedId.Value;
                if (!service.Update(item)) throw new InvalidOperationException("Khong tim thay nha cung cap de cap nhat.");
                LoadData(txtSearch.Text);
                ClearFields();
                MessageBox.Show(this, "Cap nhat nha cung cap thanh cong.", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void Delete_Click(object? sender, EventArgs e)
        {
            try
            {
                if (selectedId is null) throw new InvalidOperationException("Hay chon nha cung cap can xoa.");
                if (MessageBox.Show(this, "Ban chac chan muon xoa nha cung cap da chon?", "Xac nhan", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
                if (!service.Delete(selectedId.Value)) throw new InvalidOperationException("Khong tim thay nha cung cap de xoa.");
                LoadData(txtSearch.Text);
                ClearFields();
                MessageBox.Show(this, "Xoa nha cung cap thanh cong.", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void Grid_SelectionChanged(object? sender, EventArgs e)
        {
            if (grid.CurrentRow?.DataBoundItem is not NhaCungCapDTO item) return;
            selectedId = item.MaNhaCungCap;
            txtName.Text = item.TenNhaCungCap;
            txtPhone.Text = item.SoDienThoai ?? string.Empty;
            txtEmail.Text = item.Email ?? string.Empty;
            txtAddress.Text = item.DiaChi ?? string.Empty;
            cboStatus.SelectedIndex = item.TrangThai == 1 ? 0 : 1;
        }

        private void ClearFields()
        {
            selectedId = null;
            txtName.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            txtAddress.Clear();
            cboStatus.SelectedIndex = 0;
            grid.ClearSelection();
        }

        private void ShowError(Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Loi quan ly nha cung cap", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
