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
            Text = "Quan ly danh muc";
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(1000, 650);
            MinimumSize = new Size(800, 520);
            BuildLayout();
            grid.SelectionChanged += Grid_SelectionChanged;
            LoadData();
        }

        private void BuildLayout()
        {
            txtId.MaxLength = 10;
            txtName.MaxLength = 100;
            txtDescription.MaxLength = 255;
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
            editor.Controls.Add(ManagementFormControls.CreateField("Ma danh muc *", txtId, 180));
            editor.Controls.Add(ManagementFormControls.CreateField("Ten danh muc *", txtName, 260));
            editor.Controls.Add(ManagementFormControls.CreateField("Mo ta", txtDescription, 400));
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
                ManagementFormControls.SetColumnHeader(grid, nameof(DanhMucDTO.MaDanhMuc), "Ma danh muc");
                ManagementFormControls.SetColumnHeader(grid, nameof(DanhMucDTO.TenDanhMuc), "Ten danh muc");
                ManagementFormControls.SetColumnHeader(grid, nameof(DanhMucDTO.MoTa), "Mo ta");
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
                if (!service.Insert(BuildItem())) throw new InvalidOperationException("Khong them duoc danh muc.");
                LoadData(txtSearch.Text);
                ClearFields();
                MessageBox.Show(this, "Them danh muc thanh cong.", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void Update_Click(object? sender, EventArgs e)
        {
            try
            {
                if (selectedId is null) throw new InvalidOperationException("Hay chon danh muc can cap nhat.");
                DanhMucDTO item = BuildItem();
                item.MaDanhMuc = selectedId;
                if (!service.Update(item)) throw new InvalidOperationException("Khong tim thay danh muc de cap nhat.");
                LoadData(txtSearch.Text);
                ClearFields();
                MessageBox.Show(this, "Cap nhat danh muc thanh cong.", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void Delete_Click(object? sender, EventArgs e)
        {
            try
            {
                if (selectedId is null) throw new InvalidOperationException("Hay chon danh muc can xoa.");
                if (MessageBox.Show(this, "Ban chac chan muon xoa danh muc da chon?", "Xac nhan", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
                if (!service.Delete(selectedId)) throw new InvalidOperationException("Khong tim thay danh muc de xoa.");
                LoadData(txtSearch.Text);
                ClearFields();
                MessageBox.Show(this, "Xoa danh muc thanh cong.", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void Grid_SelectionChanged(object? sender, EventArgs e)
        {
            if (grid.CurrentRow?.DataBoundItem is not DanhMucDTO item) return;
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
            MessageBox.Show(this, ex.Message, "Loi quan ly danh muc", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
