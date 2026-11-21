using ComputerStore.DTO.Entities;
using ComputerStore.GUI.Forms.Customers;
using ComputerStore.GUI.Forms.Employees;
using ComputerStore.GUI.Forms.Products;
using ComputerStore.GUI.Forms.Suppliers;
using ComputerStore.GUI.Forms.Transactions;

namespace ComputerStore.GUI.Forms.Main
{
    public partial class FrmMain : Form
    {
        private readonly TaiKhoanDTO currentAccount;
        private readonly NhanVienDTO? currentEmployee;
        private Form? activeForm;
        private Button? activeButton;

        public bool IsLogout { get; private set; }

        public FrmMain(TaiKhoanDTO account, NhanVienDTO? employee = null)
        {
            currentAccount = account;
            currentEmployee = employee;
            InitializeComponent();
        }

        private void FrmMain_Load(object? sender, EventArgs e)
        {
            string tenHienThi = currentEmployee?.HoTen ?? currentAccount.TenDangNhap;
            string vaiTroText = currentAccount.VaiTro == "Admin" ? "Quản trị viên" : "Nhân viên";

            lblUserInfo.Text = $"Xin chào: {tenHienThi} ({vaiTroText})";
            lblThongTinPhien.Text =
                $"Tài khoản: {currentAccount.TenDangNhap}   |   " +
                $"Nhân viên: {tenHienThi}   |   " +
                $"Vai trò: {vaiTroText}";

            // Phan quyen: chi Admin moi duoc quan ly nhan vien va tai khoan
            bool isAdmin = string.Equals(currentAccount.VaiTro, "Admin", StringComparison.OrdinalIgnoreCase);
            btnNhanVien.Visible = isAdmin;

            ShowHome();
        }

        private void ShowHome()
        {
            CloseActiveForm();
            pnlHome.Visible = true;
            pnlHome.BringToFront();
            HighlightButton(btnTrangChu);
            Text = "Quản lý cửa hàng máy tính - Trang chủ";
        }

        private void OpenChildForm(Form childForm, Button senderButton, string title)
        {
            CloseActiveForm();
            pnlHome.Visible = false;

            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.MinimumSize = Size.Empty;
            childForm.Dock = DockStyle.Fill;

            pnlContent.Controls.Add(childForm);
            childForm.BringToFront();
            childForm.Show();

            HighlightButton(senderButton);
            Text = $"Quản lý cửa hàng máy tính - {title}";
        }

        private void CloseActiveForm()
        {
            if (activeForm != null)
            {
                activeForm.Close();
                activeForm.Dispose();
                activeForm = null;
            }
        }

        private void HighlightButton(Button btn)
        {
            if (activeButton != null)
            {
                activeButton.BackColor = Color.FromArgb(240, 244, 248);
                activeButton.ForeColor = Color.FromArgb(30, 41, 59);
                activeButton.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            }

            activeButton = btn;
            activeButton.BackColor = Color.FromArgb(31, 78, 121);
            activeButton.ForeColor = Color.White;
            activeButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        }

        private void btnTrangChu_Click(object? sender, EventArgs e)
        {
            ShowHome();
        }

        private void btnBanHang_Click(object? sender, EventArgs e)
        {
            var frm = new FrmBanHang { MaNhanVien = currentAccount.MaNhanVien };
            OpenChildForm(frm, btnBanHang, "Bán hàng");
        }

        private void btnNhapHang_Click(object? sender, EventArgs e)
        {
            var frm = new FrmNhapHang { MaNhanVien = currentAccount.MaNhanVien };
            OpenChildForm(frm, btnNhapHang, "Nhập hàng");
        }

        private void btnKhachHang_Click(object? sender, EventArgs e)
        {
            OpenChildForm(new FrmKhachHang(), btnKhachHang, "Quản lý khách hàng");
        }

        private void btnSanPham_Click(object? sender, EventArgs e)
        {
            OpenChildForm(new FrmSanPham(), btnSanPham, "Quản lý sản phẩm");
        }

        private void btnDanhMuc_Click(object? sender, EventArgs e)
        {
            OpenChildForm(new FrmDanhMuc(), btnDanhMuc, "Quản lý danh mục");
        }

        private void btnNhaCungCap_Click(object? sender, EventArgs e)
        {
            OpenChildForm(new FrmNhaCungCap(), btnNhaCungCap, "Quản lý nhà cung cấp");
        }

        private void btnNhanVien_Click(object? sender, EventArgs e)
        {
            OpenChildForm(new FrmNhanVien(), btnNhanVien, "Quản lý nhân viên");
        }

        private void btnDangXuat_Click(object? sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn đăng xuất khỏi hệ thống?", "Xác nhận đăng xuất",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            IsLogout = true;
            Close();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            CloseActiveForm();
            base.OnFormClosed(e);
        }
    }
}
