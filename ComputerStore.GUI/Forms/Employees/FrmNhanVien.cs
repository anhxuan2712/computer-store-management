using ComputerStore.BLL.Services;
using ComputerStore.DTO.Entities;

namespace ComputerStore.GUI.Forms.Employees
{
    public partial class FrmNhanVien : Form
    {
        public FrmNhanVien()
        {
            InitializeComponent();
        }

        /* =====================================================================
         * HƯỚNG DẪN NỐI LẠI SAU KHI KÉO THẢ UI (Visual Studio Designer):
         * Đặt thuộc tính (Name) cho các control như sau, gán sự kiện tương ứng
         * rồi bỏ comment khối code bên dưới:
         *   - Thông tin NV:
         *       TextBox        : txtMa (ReadOnly=true), txtHoTen, txtSdt, txtEmail, txtDiaChi, txtTimKiem
         *       ComboBox       : cboGioiTinh, cboTrangThai
         *       DateTimePicker : dtpNgaySinh (ShowCheckBox=true), dtpNgayVaoLam (ShowCheckBox=true)
         *       Button         : btnThem, btnSua, btnXoa, btnLamMoi, btnTimKiem
         *       DataGridView   : dgvNhanVien (SelectionChanged, CellFormatting)
         *   - Tài khoản liên kết:
         *       GroupBox       : grpTaiKhoan
         *       TextBox        : txtTenDangNhap, txtMatKhau
         *       ComboBox       : cboVaiTro, cboTrangThaiTK
         *       Button         : btnLuuTaiKhoan
         *   - Form: FrmNhanVien -> Load: FrmNhanVien_Load
         * =====================================================================

        private readonly NhanVienBLL nhanVienBLL = new NhanVienBLL();
        private readonly TaiKhoanBLL taiKhoanBLL = new TaiKhoanBLL();

        private void FrmNhanVien_Load(object? sender, EventArgs e)
        {
            cboGioiTinh.Items.Clear();
            cboGioiTinh.Items.AddRange(new object[] { "Nam", "Nu", "Khac" });

            cboTrangThai.Items.Clear();
            cboTrangThai.Items.AddRange(new object[] { "Đang làm việc", "Đã nghỉ việc" });

            cboVaiTro.Items.Clear();
            cboVaiTro.Items.AddRange(new object[] { "NhanVien", "Admin" });

            cboTrangThaiTK.Items.Clear();
            cboTrangThaiTK.Items.AddRange(new object[] { "Hoạt động", "Bị khóa" });

            LoadData();
            ClearInputs();
        }

        private void LoadData(string keyword = "")
        {
            try
            {
                dgvNhanVien.DataSource = nhanVienBLL.Search(keyword);
                SetHeader("MaNhanVien", "Mã NV");
                SetHeader("HoTen", "Họ tên");
                SetHeader("GioiTinh", "Giới tính");
                SetHeader("NgaySinh", "Ngày sinh");
                SetHeader("SoDienThoai", "Số điện thoại");
                SetHeader("Email", "Email");
                SetHeader("DiaChi", "Địa chỉ");
                SetHeader("NgayVaoLam", "Ngày vào làm");
                SetHeader("TrangThai", "Trạng thái");
                SetHeader("NgayTao", "Ngày tạo");

                if (dgvNhanVien.Columns["NgaySinh"] is DataGridViewColumn colNgaySinh)
                    colNgaySinh.DefaultCellStyle.Format = "dd/MM/yyyy";
                if (dgvNhanVien.Columns["NgayVaoLam"] is DataGridViewColumn colNgayVaoLam)
                    colNgayVaoLam.DefaultCellStyle.Format = "dd/MM/yyyy";
                if (dgvNhanVien.Columns["NgayTao"] is DataGridViewColumn colNgayTao)
                    colNgayTao.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

                dgvNhanVien.ClearSelection();
            }
            catch (Exception ex)
            {
                ShowError("Không tải được danh sách nhân viên: " + ex.Message);
            }
        }

        private void SetHeader(string column, string header)
        {
            if (dgvNhanVien.Columns[column] is DataGridViewColumn col)
                col.HeaderText = header;
        }

        private NhanVienDTO ReadInputs() => new NhanVienDTO
        {
            MaNhanVien = int.TryParse(txtMa.Text, out int ma) ? ma : 0,
            HoTen = txtHoTen.Text,
            GioiTinh = cboGioiTinh.SelectedItem?.ToString() ?? "Khac",
            NgaySinh = dtpNgaySinh.Checked ? dtpNgaySinh.Value.Date : null,
            SoDienThoai = txtSdt.Text,
            Email = txtEmail.Text,
            DiaChi = txtDiaChi.Text,
            NgayVaoLam = dtpNgayVaoLam.Checked ? dtpNgayVaoLam.Value.Date : null,
            TrangThai = cboTrangThai.SelectedIndex == 1 ? 0 : 1
        };

        private void ClearInputs()
        {
            txtMa.Clear();
            txtHoTen.Clear();
            cboGioiTinh.SelectedIndex = 0;
            dtpNgaySinh.Value = new DateTime(2000, 1, 1);
            dtpNgaySinh.Checked = false;
            txtSdt.Clear();
            txtEmail.Clear();
            txtDiaChi.Clear();
            dtpNgayVaoLam.Value = DateTime.Today;
            dtpNgayVaoLam.Checked = true;
            cboTrangThai.SelectedIndex = 0;

            ClearAccountInputs();
            grpTaiKhoan.Enabled = false;

            dgvNhanVien.ClearSelection();
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            txtHoTen.Focus();
        }

        private void ClearAccountInputs()
        {
            txtTenDangNhap.Clear();
            txtMatKhau.Clear();
            cboVaiTro.SelectedIndex = 0;
            cboTrangThaiTK.SelectedIndex = 0;
        }

        private void LoadAccountForEmployee(int maNhanVien)
        {
            ClearAccountInputs();
            grpTaiKhoan.Enabled = true;

            try
            {
                TaiKhoanDTO? tk = taiKhoanBLL.GetByMaNhanVien(maNhanVien);
                if (tk != null)
                {
                    txtTenDangNhap.Text = tk.TenDangNhap;
                    cboVaiTro.SelectedItem = tk.VaiTro;
                    cboTrangThaiTK.SelectedIndex = tk.TrangThai == 1 ? 0 : 1;
                }
            }
            catch (Exception ex)
            {
                ShowError("Không tải được tài khoản nhân viên: " + ex.Message);
            }
        }

        private void dgvNhanVien_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvNhanVien.CurrentRow?.DataBoundItem is not NhanVienDTO nv || !dgvNhanVien.CurrentRow.Selected)
                return;

            txtMa.Text = nv.MaNhanVien.ToString();
            txtHoTen.Text = nv.HoTen;
            cboGioiTinh.SelectedItem = string.IsNullOrEmpty(nv.GioiTinh) ? "Khac" : nv.GioiTinh;

            if (nv.NgaySinh.HasValue)
            {
                dtpNgaySinh.Value = nv.NgaySinh.Value;
                dtpNgaySinh.Checked = true;
            }
            else
            {
                dtpNgaySinh.Checked = false;
            }

            txtSdt.Text = nv.SoDienThoai ?? string.Empty;
            txtEmail.Text = nv.Email ?? string.Empty;
            txtDiaChi.Text = nv.DiaChi ?? string.Empty;

            if (nv.NgayVaoLam.HasValue)
            {
                dtpNgayVaoLam.Value = nv.NgayVaoLam.Value;
                dtpNgayVaoLam.Checked = true;
            }
            else
            {
                dtpNgayVaoLam.Checked = false;
            }

            cboTrangThai.SelectedIndex = nv.TrangThai == 1 ? 0 : 1;

            LoadAccountForEmployee(nv.MaNhanVien);

            btnThem.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void dgvNhanVien_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvNhanVien.Columns[e.ColumnIndex].Name == "TrangThai" && e.Value is int tt)
            {
                e.Value = tt == 1 ? "Đang làm" : "Đã nghỉ";
                e.FormattingApplied = true;
            }
        }

        private void btnThem_Click(object? sender, EventArgs e)
        {
            Execute(() =>
            {
                NhanVienDTO nv = ReadInputs();
                nv.MaNhanVien = 0;
                return (nhanVienBLL.Insert(nv, out string msg), msg);
            });
        }

        private void btnSua_Click(object? sender, EventArgs e)
        {
            Execute(() => (nhanVienBLL.Update(ReadInputs(), out string msg), msg));
        }

        private void btnXoa_Click(object? sender, EventArgs e)
        {
            if (!int.TryParse(txtMa.Text, out int maNhanVien))
                return;

            if (MessageBox.Show($"Xóa nhân viên {maNhanVien} - {txtHoTen.Text}?", "Xác nhận xóa",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            Execute(() => (nhanVienBLL.Delete(maNhanVien, out string msg), msg));
        }

        private void btnLuuTaiKhoan_Click(object? sender, EventArgs e)
        {
            if (!int.TryParse(txtMa.Text, out int maNhanVien) || maNhanVien <= 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần cấp/cập nhật tài khoản.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string vaiTro = cboVaiTro.SelectedItem?.ToString() ?? "NhanVien";
                int trangThai = cboTrangThaiTK.SelectedIndex == 0 ? 1 : 0;

                bool ok = taiKhoanBLL.SaveAccount(
                    maNhanVien,
                    txtTenDangNhap.Text,
                    txtMatKhau.Text,
                    vaiTro,
                    trangThai,
                    out string message);

                if (!ok)
                {
                    MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show(message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMatKhau.Clear();
            }
            catch (Exception ex)
            {
                ShowError("Lỗi lưu tài khoản: " + ex.Message);
            }
        }

        private void btnLamMoi_Click(object? sender, EventArgs e)
        {
            txtTimKiem.Clear();
            LoadData();
            ClearInputs();
        }

        private void btnTimKiem_Click(object? sender, EventArgs e)
        {
            LoadData(txtTimKiem.Text);
            ClearInputs();
        }

        private void txtTimKiem_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnTimKiem_Click(sender, e);
            }
        }

        private void Execute(Func<(bool ok, string message)> action)
        {
            try
            {
                var (ok, message) = action();
                if (!ok)
                {
                    MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show(message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData(txtTimKiem.Text);
                ClearInputs();
            }
            catch (Exception ex)
            {
                ShowError("Lỗi thao tác dữ liệu: " + ex.Message);
            }
        }

        private static void ShowError(string message) =>
            MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        */
    }
}
