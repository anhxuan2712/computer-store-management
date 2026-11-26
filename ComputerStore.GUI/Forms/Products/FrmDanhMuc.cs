using ComputerStore.BLL.Services;
using ComputerStore.DTO.Entities;

namespace ComputerStore.GUI.Forms.Products
{
    public partial class FrmDanhMuc : Form
    {
        public FrmDanhMuc()
        {
            InitializeComponent();
        }

        /* =====================================================================
         * HƯỚNG DẪN NỐI LẠI SAU KHI KÉO THẢ UI (Visual Studio Designer):
         * Đặt thuộc tính (Name) cho các control như sau, gán sự kiện tương ứng
         * rồi bỏ comment khối code bên dưới:
         *   - TextBox      : txtSearch, txtId (MaxLength=10), txtName (MaxLength=100), txtDescription (MaxLength=255)
         *   - Button       : btnTimKiem  -> Click: BtnTimKiem_Click
         *                    btnTaiLai   -> Click: BtnTaiLai_Click
         *                    btnThem     -> Click: Add_Click
         *                    btnSua      -> Click: Update_Click
         *                    btnXoa      -> Click: Delete_Click
         *                    btnLamMoi   -> Click: BtnLamMoi_Click
         *   - DataGridView : grid        -> SelectionChanged: Grid_SelectionChanged
         *   - Form         : FrmDanhMuc  -> Load: FrmDanhMuc_Load
         * =====================================================================

        private readonly DanhMucBLL service = new();
        private string? selectedId;

        private void FrmDanhMuc_Load(object? sender, EventArgs e)
        {
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
                grid.DataSource = service.Search(keyword);
                SetColumnHeader(nameof(DanhMucDTO.MaDanhMuc), "Mã danh mục");
                SetColumnHeader(nameof(DanhMucDTO.TenDanhMuc), "Tên danh mục");
                SetColumnHeader(nameof(DanhMucDTO.MoTa), "Mô tả");
                grid.ClearSelection();
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void SetColumnHeader(string property, string title)
        {
            if (grid.Columns[property] is DataGridViewColumn col)
                col.HeaderText = title;
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
        */
    }
}
