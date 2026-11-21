using ComputerStore.BLL.Services;
using ComputerStore.DTO.Entities;

namespace ComputerStore.GUI.Forms.Authentication
{
    public partial class FrmLogin : Form
    {
        private readonly TaiKhoanBLL taiKhoanBLL = new TaiKhoanBLL();

        public TaiKhoanDTO? LoggedInAccount { get; private set; }
        public NhanVienDTO? LoggedInEmployee { get; private set; }

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void FrmLogin_Load(object? sender, EventArgs e)
        {
            txtTenDangNhap.Focus();
        }

        private void chkHienMatKhau_CheckedChanged(object? sender, EventArgs e)
        {
            txtMatKhau.UseSystemPasswordChar = !chkHienMatKhau.Checked;
        }

        private void btnDangNhap_Click(object? sender, EventArgs e)
        {
            try
            {
                bool ok = taiKhoanBLL.Login(
                    txtTenDangNhap.Text,
                    txtMatKhau.Text,
                    out TaiKhoanDTO? taiKhoan,
                    out NhanVienDTO? nhanVien,
                    out string message);

                if (!ok || taiKhoan == null)
                {
                    MessageBox.Show(message, "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMatKhau.Clear();
                    txtMatKhau.Focus();
                    return;
                }

                LoggedInAccount = taiKhoan;
                LoggedInEmployee = nhanVien;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối khi đăng nhập: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
