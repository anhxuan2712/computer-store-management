using ComputerStore.BLL.Services;
using ComputerStore.DTO.Entities;

namespace ComputerStore.GUI.Forms.Suppliers
{
    public partial class FrmNhaCungCap : Form
    {
        public FrmNhaCungCap()
        {
            InitializeComponent();
        }

        /* =====================================================================
         * HƯỚNG DẪN NỐI LẠI SAU KHI KÉO THẢ UI (Visual Studio Designer):
         * Đặt thuộc tính (Name) cho các control như sau, gán sự kiện tương ứng
         * rồi bỏ comment khối code bên dưới:
         *   - TextBox      : txtSearch, txtName (MaxLength=150), txtPhone (MaxLength=15),
         *                    txtEmail (MaxLength=100), txtAddress (MaxLength=255)
         *   - ComboBox     : cboStatus (DropDownStyle=DropDownList; Items: "Hoạt động", "Ngừng hoạt động")
         *   - Button       : btnTimKiem    -> Click: BtnTimKiem_Click
         *                    btnTaiLai     -> Click: BtnTaiLai_Click
         *                    btnThem       -> Click: Add_Click
         *                    btnSua        -> Click: Update_Click
         *                    btnXoa        -> Click: Delete_Click
         *                    btnLamMoi     -> Click: BtnLamMoi_Click
         *   - DataGridView : grid          -> SelectionChanged: Grid_SelectionChanged
         *   - Form         : FrmNhaCungCap -> Load: FrmNhaCungCap_Load
         * =====================================================================

        private readonly NhaCungCapBLL service = new();
        private int? selectedId;

        private void FrmNhaCungCap_Load(object? sender, EventArgs e)
        {
            cboStatus.Items.Clear();
            cboStatus.Items.AddRange(new object[] { "Hoạt động", "Ngừng hoạt động" });
            cboStatus.SelectedIndex = 0;
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
                SetColumnHeader(nameof(NhaCungCapDTO.MaNhaCungCap), "Mã NCC");
                SetColumnHeader(nameof(NhaCungCapDTO.TenNhaCungCap), "Tên nhà cung cấp");
                SetColumnHeader(nameof(NhaCungCapDTO.SoDienThoai), "Số điện thoại");
                SetColumnHeader(nameof(NhaCungCapDTO.Email), "Email");
                SetColumnHeader(nameof(NhaCungCapDTO.DiaChi), "Địa chỉ");
                SetColumnHeader(nameof(NhaCungCapDTO.TrangThai), "Trạng thái");
                grid.ClearSelection();
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void SetColumnHeader(string property, string title)
        {
            if (grid.Columns[property] is DataGridViewColumn col)
                col.HeaderText = title;
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
        */
    }
}
