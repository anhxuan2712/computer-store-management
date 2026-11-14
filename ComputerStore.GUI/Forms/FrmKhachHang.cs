using ComputerStore.BLL;
using ComputerStore.DTO;

namespace ComputerStore.GUI.Forms
{
    public partial class FrmKhachHang : Form
    {
        private readonly KhachHangBLL bll = new KhachHangBLL();

        public FrmKhachHang()
        {
            InitializeComponent();
        }

        private void FrmKhachHang_Load(object? sender, EventArgs e)
        {
            LoadData();
            ClearInputs();
        }

        private void LoadData(string keyword = "")
        {
            try
            {
                dgvKhachHang.DataSource = bll.Search(keyword);
                SetHeader("MaKhachHang", "Mã KH");
                SetHeader("TenKhachHang", "Tên khách hàng");
                SetHeader("SoDienThoai", "Số điện thoại");
                SetHeader("DiaChi", "Địa chỉ");
                SetHeader("Email", "Email");
                dgvKhachHang.ClearSelection();
            }
            catch (Exception ex)
            {
                ShowError("Không tải được danh sách khách hàng: " + ex.Message);
            }
        }

        private void SetHeader(string column, string header)
        {
            if (dgvKhachHang.Columns[column] is DataGridViewColumn col) col.HeaderText = header;
        }

        private KhachHangDTO ReadInputs() => new KhachHangDTO
        {
            MaKhachHang = txtMa.Text,
            TenKhachHang = txtTen.Text,
            SoDienThoai = txtSdt.Text,
            DiaChi = txtDiaChi.Text,
            Email = txtEmail.Text
        };

        private void ClearInputs()
        {
            txtMa.Clear();
            txtTen.Clear();
            txtSdt.Clear();
            txtDiaChi.Clear();
            txtEmail.Clear();
            dgvKhachHang.ClearSelection();
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            txtTen.Focus();
        }

        private void dgvKhachHang_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvKhachHang.CurrentRow?.DataBoundItem is not KhachHangDTO kh || !dgvKhachHang.CurrentRow.Selected)
                return;

            txtMa.Text = kh.MaKhachHang;
            txtTen.Text = kh.TenKhachHang;
            txtSdt.Text = kh.SoDienThoai;
            txtDiaChi.Text = kh.DiaChi;
            txtEmail.Text = kh.Email;
            btnThem.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void btnThem_Click(object? sender, EventArgs e)
        {
            Execute(() =>
            {
                var kh = ReadInputs();
                kh.MaKhachHang = string.Empty;
                return (bll.Insert(kh, out string msg), msg);
            });
        }

        private void btnSua_Click(object? sender, EventArgs e)
        {
            Execute(() => (bll.Update(ReadInputs(), out string msg), msg));
        }

        private void btnXoa_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMa.Text)) return;
            if (MessageBox.Show($"Xóa khách hàng {txtMa.Text} - {txtTen.Text}?", "Xác nhận xóa",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            Execute(() => (bll.Delete(txtMa.Text, out string msg), msg));
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

        /// <summary>Chạy thao tác thêm/sửa/xóa, hiển thị kết quả và tải lại lưới khi thành công.</summary>
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
    }
}
