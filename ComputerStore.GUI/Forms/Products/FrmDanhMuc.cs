using System.Drawing;
using ComputerStore.BLL.Services;
using ComputerStore.DTO.Entities;
using ComputerStore.GUI.Helpers;

namespace ComputerStore.GUI.Forms.Products
{
    public class FrmDanhMuc : Form
    {
        private readonly DanhMucBLL service = new();
        private readonly DataGridView grid = ManagementFormControls.CreateGrid();
        private readonly TextBox txtSearch = new();
        private readonly TextBox txtId = new();
        private readonly TextBox txtName = new();
        private readonly TextBox txtDescription = new();
        private string? selectedId;

        public FrmDanhMuc()
        {
            Text = "Quản lý danh mục";
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(1000, 650);
            MinimumSize = new Size(800, 520);
            BuildLayout();
            grid.SelectionChanged += Grid_SelectionChanged;
            Load += (_, _) => LoadData();
        }

        private void BuildLayout()
        {
            txtId.MaxLength = 10;
            txtName.MaxLength = 100;
            txtDescription.MaxLength = 255;
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
            editor.Controls.Add(ManagementFormControls.CreateField("Mã danh mục *", txtId, 180));
            editor.Controls.Add(ManagementFormControls.CreateField("Tên danh mục *", txtName, 260));
            editor.Controls.Add(ManagementFormControls.CreateField("Mô tả", txtDescription, 400));
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
                ManagementFormControls.SetColumnHeader(grid, nameof(DanhMucDTO.MaDanhMuc), "Mã danh mục");
                ManagementFormControls.SetColumnHeader(grid, nameof(DanhMucDTO.TenDanhMuc), "Tên danh mục");
                ManagementFormControls.SetColumnHeader(grid, nameof(DanhMucDTO.MoTa), "Mô tả");
                grid.ClearSelection();
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private DanhMucDTO BuildItem() => new()
        {
            MaDanhMuc = txtId.Text,
            TenDanhMuc = txtName.Text,
            MoTa = txtDescription.Text
        };

        private void Add_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!service.Insert(BuildItem())) throw new InvalidOperationException("Không thêm được danh mục.");
                LoadData(txtSearch.Text);
                ClearFields();
                MessageBox.Show(this, "Thêm danh mục thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void Update_Click(object? sender, EventArgs e)
        {
            try
            {
                if (selectedId is null) throw new InvalidOperationException("Hãy chọn danh mục cần cập nhật.");
                DanhMucDTO item = BuildItem();
                item.MaDanhMuc = selectedId;
                if (!service.Update(item)) throw new InvalidOperationException("Không tìm thấy danh mục để cập nhật.");
                LoadData(txtSearch.Text);
                ClearFields();
                MessageBox.Show(this, "Cập nhật danh mục thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void Delete_Click(object? sender, EventArgs e)
        {
            try
            {
                if (selectedId is null) throw new InvalidOperationException("Hãy chọn danh mục cần xóa.");
                if (MessageBox.Show(this, "Bạn chắc chắn muốn xóa danh mục đã chọn?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
                if (!service.Delete(selectedId)) throw new InvalidOperationException("Không tìm thấy danh mục để xóa.");
                LoadData(txtSearch.Text);
                ClearFields();
                MessageBox.Show(this, "Xóa danh mục thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void Grid_SelectionChanged(object? sender, EventArgs e)
        {
            if (grid.CurrentRow?.DataBoundItem is not DanhMucDTO item || !grid.CurrentRow.Selected) return;
            selectedId = item.MaDanhMuc;
            txtId.Text = item.MaDanhMuc;
            txtId.ReadOnly = true;
            txtName.Text = item.TenDanhMuc;
            txtDescription.Text = item.MoTa ?? string.Empty;
        }

        private void ClearFields()
        {
            selectedId = null;
            txtId.ReadOnly = false;
            txtId.Clear();
            txtName.Clear();
            txtDescription.Clear();
            grid.ClearSelection();
        }

        private void ShowError(Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Lỗi quản lý danh mục", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
