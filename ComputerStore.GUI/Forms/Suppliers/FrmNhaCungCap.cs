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
            Text = "Quản lý nhà cung cấp";
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(1100, 680);
            MinimumSize = new Size(850, 560);
            BuildLayout();
            grid.SelectionChanged += Grid_SelectionChanged;
            Load += (_, _) => LoadData();
        }

        private void BuildLayout()
        {
            txtName.MaxLength = 150;
            txtPhone.MaxLength = 15;
            txtEmail.MaxLength = 100;
            txtAddress.MaxLength = 255;
            txtSearch.Width = 220;

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
            editor.Controls.Add(ManagementFormControls.CreateField("Tên nhà cung cấp *", txtName, 230));
            editor.Controls.Add(ManagementFormControls.CreateField("Số điện thoại", txtPhone, 160));
            editor.Controls.Add(ManagementFormControls.CreateField("Email", txtEmail, 220));
            editor.Controls.Add(ManagementFormControls.CreateField("Trạng thái", cboStatus, 150));
            editor.Controls.Add(ManagementFormControls.CreateField("Địa chỉ", txtAddress, 320));
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
                ManagementFormControls.SetColumnHeader(grid, nameof(NhaCungCapDTO.MaNhaCungCap), "Mã NCC");
                ManagementFormControls.SetColumnHeader(grid, nameof(NhaCungCapDTO.TenNhaCungCap), "Tên nhà cung cấp");
                ManagementFormControls.SetColumnHeader(grid, nameof(NhaCungCapDTO.SoDienThoai), "Số điện thoại");
                ManagementFormControls.SetColumnHeader(grid, nameof(NhaCungCapDTO.Email), "Email");
                ManagementFormControls.SetColumnHeader(grid, nameof(NhaCungCapDTO.DiaChi), "Địa chỉ");
                ManagementFormControls.SetColumnHeader(grid, nameof(NhaCungCapDTO.TrangThai), "Trạng thái");
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
                if (!service.Insert(BuildItem())) throw new InvalidOperationException("Không thêm được nhà cung cấp.");
                LoadData(txtSearch.Text);
                ClearFields();
                MessageBox.Show(this, "Thêm nhà cung cấp thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void Update_Click(object? sender, EventArgs e)
        {
            try
            {
                if (selectedId is null) throw new InvalidOperationException("Hãy chọn nhà cung cấp cần cập nhật.");
                NhaCungCapDTO item = BuildItem();
                item.MaNhaCungCap = selectedId.Value;
                if (!service.Update(item)) throw new InvalidOperationException("Không tìm thấy nhà cung cấp để cập nhật.");
                LoadData(txtSearch.Text);
                ClearFields();
                MessageBox.Show(this, "Cập nhật nhà cung cấp thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void Delete_Click(object? sender, EventArgs e)
        {
            try
            {
                if (selectedId is null) throw new InvalidOperationException("Hãy chọn nhà cung cấp cần xóa.");
                if (MessageBox.Show(this, "Bạn chắc chắn muốn xóa nhà cung cấp đã chọn?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
                if (!service.Delete(selectedId.Value)) throw new InvalidOperationException("Không tìm thấy nhà cung cấp để xóa.");
                LoadData(txtSearch.Text);
                ClearFields();
                MessageBox.Show(this, "Xóa nhà cung cấp thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void Grid_SelectionChanged(object? sender, EventArgs e)
        {
            if (grid.CurrentRow?.DataBoundItem is not NhaCungCapDTO item || !grid.CurrentRow.Selected) return;
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
            MessageBox.Show(this, ex.Message, "Lỗi quản lý nhà cung cấp", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
