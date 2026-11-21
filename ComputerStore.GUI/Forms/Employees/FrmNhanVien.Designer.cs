namespace ComputerStore.GUI.Forms.Employees
{
    partial class FrmNhanVien
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            grpThongTin = new GroupBox();
            lblMa = new Label();
            txtMa = new TextBox();
            lblHoTen = new Label();
            txtHoTen = new TextBox();
            lblGioiTinh = new Label();
            cboGioiTinh = new ComboBox();
            lblSdt = new Label();
            txtSdt = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblNgaySinh = new Label();
            dtpNgaySinh = new DateTimePicker();
            lblDiaChi = new Label();
            txtDiaChi = new TextBox();
            lblNgayVaoLam = new Label();
            dtpNgayVaoLam = new DateTimePicker();
            lblTrangThai = new Label();
            cboTrangThai = new ComboBox();
            grpTaiKhoan = new GroupBox();
            lblTenDangNhap = new Label();
            txtTenDangNhap = new TextBox();
            lblMatKhau = new Label();
            txtMatKhau = new TextBox();
            lblVaiTro = new Label();
            cboVaiTro = new ComboBox();
            lblTrangThaiTK = new Label();
            cboTrangThaiTK = new ComboBox();
            btnLuuTaiKhoan = new Button();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            btnLamMoi = new Button();
            lblTimKiem = new Label();
            txtTimKiem = new TextBox();
            btnTimKiem = new Button();
            dgvNhanVien = new DataGridView();
            grpThongTin.SuspendLayout();
            grpTaiKhoan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvNhanVien).BeginInit();
            SuspendLayout();
            //
            // grpThongTin
            //
            grpThongTin.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpThongTin.Controls.Add(lblMa);
            grpThongTin.Controls.Add(txtMa);
            grpThongTin.Controls.Add(lblHoTen);
            grpThongTin.Controls.Add(txtHoTen);
            grpThongTin.Controls.Add(lblGioiTinh);
            grpThongTin.Controls.Add(cboGioiTinh);
            grpThongTin.Controls.Add(lblSdt);
            grpThongTin.Controls.Add(txtSdt);
            grpThongTin.Controls.Add(lblEmail);
            grpThongTin.Controls.Add(txtEmail);
            grpThongTin.Controls.Add(lblNgaySinh);
            grpThongTin.Controls.Add(dtpNgaySinh);
            grpThongTin.Controls.Add(lblDiaChi);
            grpThongTin.Controls.Add(txtDiaChi);
            grpThongTin.Controls.Add(lblNgayVaoLam);
            grpThongTin.Controls.Add(dtpNgayVaoLam);
            grpThongTin.Controls.Add(lblTrangThai);
            grpThongTin.Controls.Add(cboTrangThai);
            grpThongTin.Location = new Point(12, 10);
            grpThongTin.Name = "grpThongTin";
            grpThongTin.Size = new Size(860, 128);
            grpThongTin.TabIndex = 0;
            grpThongTin.TabStop = false;
            grpThongTin.Text = "Thông tin nhân viên";
            //
            // lblMa
            //
            lblMa.AutoSize = true;
            lblMa.Location = new Point(15, 27);
            lblMa.Name = "lblMa";
            lblMa.Text = "Mã NV:";
            //
            // txtMa
            //
            txtMa.Location = new Point(95, 24);
            txtMa.Name = "txtMa";
            txtMa.PlaceholderText = "Tự động";
            txtMa.ReadOnly = true;
            txtMa.Size = new Size(110, 23);
            txtMa.TabStop = false;
            //
            // lblHoTen
            //
            lblHoTen.AutoSize = true;
            lblHoTen.Location = new Point(225, 27);
            lblHoTen.Name = "lblHoTen";
            lblHoTen.Text = "Họ tên:";
            //
            // txtHoTen
            //
            txtHoTen.Location = new Point(305, 24);
            txtHoTen.MaxLength = 100;
            txtHoTen.Name = "txtHoTen";
            txtHoTen.Size = new Size(250, 23);
            txtHoTen.TabIndex = 1;
            //
            // lblGioiTinh
            //
            lblGioiTinh.AutoSize = true;
            lblGioiTinh.Location = new Point(575, 27);
            lblGioiTinh.Name = "lblGioiTinh";
            lblGioiTinh.Text = "Giới tính:";
            //
            // cboGioiTinh
            //
            cboGioiTinh.DropDownStyle = ComboBoxStyle.DropDownList;
            cboGioiTinh.FormattingEnabled = true;
            cboGioiTinh.Location = new Point(645, 24);
            cboGioiTinh.Name = "cboGioiTinh";
            cboGioiTinh.Size = new Size(200, 23);
            cboGioiTinh.TabIndex = 2;
            //
            // lblSdt
            //
            lblSdt.AutoSize = true;
            lblSdt.Location = new Point(15, 60);
            lblSdt.Name = "lblSdt";
            lblSdt.Text = "Điện thoại:";
            //
            // txtSdt
            //
            txtSdt.Location = new Point(95, 57);
            txtSdt.MaxLength = 15;
            txtSdt.Name = "txtSdt";
            txtSdt.Size = new Size(140, 23);
            txtSdt.TabIndex = 3;
            //
            // lblEmail
            //
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(255, 60);
            lblEmail.Name = "lblEmail";
            lblEmail.Text = "Email:";
            //
            // txtEmail
            //
            txtEmail.Location = new Point(305, 57);
            txtEmail.MaxLength = 100;
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(250, 23);
            txtEmail.TabIndex = 4;
            //
            // lblNgaySinh
            //
            lblNgaySinh.AutoSize = true;
            lblNgaySinh.Location = new Point(575, 60);
            lblNgaySinh.Name = "lblNgaySinh";
            lblNgaySinh.Text = "Ngày sinh:";
            //
            // dtpNgaySinh
            //
            dtpNgaySinh.CustomFormat = "dd/MM/yyyy";
            dtpNgaySinh.Format = DateTimePickerFormat.Custom;
            dtpNgaySinh.Location = new Point(645, 57);
            dtpNgaySinh.Name = "dtpNgaySinh";
            dtpNgaySinh.ShowCheckBox = true;
            dtpNgaySinh.Size = new Size(200, 23);
            dtpNgaySinh.TabIndex = 5;
            //
            // lblDiaChi
            //
            lblDiaChi.AutoSize = true;
            lblDiaChi.Location = new Point(15, 93);
            lblDiaChi.Name = "lblDiaChi";
            lblDiaChi.Text = "Địa chỉ:";
            //
            // txtDiaChi
            //
            txtDiaChi.Location = new Point(95, 90);
            txtDiaChi.MaxLength = 255;
            txtDiaChi.Name = "txtDiaChi";
            txtDiaChi.Size = new Size(275, 23);
            txtDiaChi.TabIndex = 6;
            //
            // lblNgayVaoLam
            //
            lblNgayVaoLam.AutoSize = true;
            lblNgayVaoLam.Location = new Point(385, 93);
            lblNgayVaoLam.Name = "lblNgayVaoLam";
            lblNgayVaoLam.Text = "Vào làm:";
            //
            // dtpNgayVaoLam
            //
            dtpNgayVaoLam.CustomFormat = "dd/MM/yyyy";
            dtpNgayVaoLam.Format = DateTimePickerFormat.Custom;
            dtpNgayVaoLam.Location = new Point(445, 90);
            dtpNgayVaoLam.Name = "dtpNgayVaoLam";
            dtpNgayVaoLam.ShowCheckBox = true;
            dtpNgayVaoLam.Size = new Size(135, 23);
            dtpNgayVaoLam.TabIndex = 7;
            //
            // lblTrangThai
            //
            lblTrangThai.AutoSize = true;
            lblTrangThai.Location = new Point(595, 93);
            lblTrangThai.Name = "lblTrangThai";
            lblTrangThai.Text = "Trạng thái:";
            //
            // cboTrangThai
            //
            cboTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTrangThai.FormattingEnabled = true;
            cboTrangThai.Location = new Point(665, 90);
            cboTrangThai.Name = "cboTrangThai";
            cboTrangThai.Size = new Size(180, 23);
            cboTrangThai.TabIndex = 8;
            //
            // grpTaiKhoan
            //
            grpTaiKhoan.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpTaiKhoan.Controls.Add(lblTenDangNhap);
            grpTaiKhoan.Controls.Add(txtTenDangNhap);
            grpTaiKhoan.Controls.Add(lblMatKhau);
            grpTaiKhoan.Controls.Add(txtMatKhau);
            grpTaiKhoan.Controls.Add(lblVaiTro);
            grpTaiKhoan.Controls.Add(cboVaiTro);
            grpTaiKhoan.Controls.Add(lblTrangThaiTK);
            grpTaiKhoan.Controls.Add(cboTrangThaiTK);
            grpTaiKhoan.Controls.Add(btnLuuTaiKhoan);
            grpTaiKhoan.Location = new Point(12, 144);
            grpTaiKhoan.Name = "grpTaiKhoan";
            grpTaiKhoan.Size = new Size(860, 62);
            grpTaiKhoan.TabIndex = 1;
            grpTaiKhoan.TabStop = false;
            grpTaiKhoan.Text = "Tài khoản đăng nhập của nhân viên đang chọn";
            //
            // lblTenDangNhap
            //
            lblTenDangNhap.AutoSize = true;
            lblTenDangNhap.Location = new Point(15, 27);
            lblTenDangNhap.Name = "lblTenDangNhap";
            lblTenDangNhap.Text = "Tài khoản:";
            //
            // txtTenDangNhap
            //
            txtTenDangNhap.Location = new Point(82, 24);
            txtTenDangNhap.MaxLength = 50;
            txtTenDangNhap.Name = "txtTenDangNhap";
            txtTenDangNhap.Size = new Size(140, 23);
            txtTenDangNhap.TabIndex = 0;
            //
            // lblMatKhau
            //
            lblMatKhau.AutoSize = true;
            lblMatKhau.Location = new Point(232, 27);
            lblMatKhau.Name = "lblMatKhau";
            lblMatKhau.Text = "Mật khẩu:";
            //
            // txtMatKhau
            //
            txtMatKhau.Location = new Point(296, 24);
            txtMatKhau.MaxLength = 100;
            txtMatKhau.Name = "txtMatKhau";
            txtMatKhau.PlaceholderText = "Trống = giữ nguyên";
            txtMatKhau.Size = new Size(145, 23);
            txtMatKhau.TabIndex = 1;
            txtMatKhau.UseSystemPasswordChar = true;
            //
            // lblVaiTro
            //
            lblVaiTro.AutoSize = true;
            lblVaiTro.Location = new Point(450, 27);
            lblVaiTro.Name = "lblVaiTro";
            lblVaiTro.Text = "Quyền:";
            //
            // cboVaiTro
            //
            cboVaiTro.DropDownStyle = ComboBoxStyle.DropDownList;
            cboVaiTro.FormattingEnabled = true;
            cboVaiTro.Location = new Point(498, 24);
            cboVaiTro.Name = "cboVaiTro";
            cboVaiTro.Size = new Size(95, 23);
            cboVaiTro.TabIndex = 2;
            //
            // lblTrangThaiTK
            //
            lblTrangThaiTK.AutoSize = true;
            lblTrangThaiTK.Location = new Point(602, 27);
            lblTrangThaiTK.Name = "lblTrangThaiTK";
            lblTrangThaiTK.Text = "TT:";
            //
            // cboTrangThaiTK
            //
            cboTrangThaiTK.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTrangThaiTK.FormattingEnabled = true;
            cboTrangThaiTK.Location = new Point(626, 24);
            cboTrangThaiTK.Name = "cboTrangThaiTK";
            cboTrangThaiTK.Size = new Size(100, 23);
            cboTrangThaiTK.TabIndex = 3;
            //
            // btnLuuTaiKhoan
            //
            btnLuuTaiKhoan.Location = new Point(735, 21);
            btnLuuTaiKhoan.Name = "btnLuuTaiKhoan";
            btnLuuTaiKhoan.Size = new Size(110, 29);
            btnLuuTaiKhoan.TabIndex = 4;
            btnLuuTaiKhoan.Text = "Lưu tài khoản";
            btnLuuTaiKhoan.UseVisualStyleBackColor = true;
            btnLuuTaiKhoan.Click += btnLuuTaiKhoan_Click;
            //
            // btnThem
            //
            btnThem.Location = new Point(12, 214);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(86, 30);
            btnThem.TabIndex = 2;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = true;
            btnThem.Click += btnThem_Click;
            //
            // btnSua
            //
            btnSua.Location = new Point(104, 214);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(86, 30);
            btnSua.TabIndex = 3;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = true;
            btnSua.Click += btnSua_Click;
            //
            // btnXoa
            //
            btnXoa.Location = new Point(196, 214);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(86, 30);
            btnXoa.TabIndex = 4;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            btnXoa.Click += btnXoa_Click;
            //
            // btnLamMoi
            //
            btnLamMoi.Location = new Point(288, 214);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(86, 30);
            btnLamMoi.TabIndex = 5;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = true;
            btnLamMoi.Click += btnLamMoi_Click;
            //
            // lblTimKiem
            //
            lblTimKiem.AutoSize = true;
            lblTimKiem.Location = new Point(455, 221);
            lblTimKiem.Name = "lblTimKiem";
            lblTimKiem.Text = "Tìm kiếm:";
            //
            // txtTimKiem
            //
            txtTimKiem.Location = new Point(522, 218);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.PlaceholderText = "Mã, họ tên, SĐT hoặc email";
            txtTimKiem.Size = new Size(258, 23);
            txtTimKiem.TabIndex = 6;
            txtTimKiem.KeyDown += txtTimKiem_KeyDown;
            //
            // btnTimKiem
            //
            btnTimKiem.Location = new Point(786, 214);
            btnTimKiem.Name = "btnTimKiem";
            btnTimKiem.Size = new Size(86, 30);
            btnTimKiem.TabIndex = 7;
            btnTimKiem.Text = "Tìm";
            btnTimKiem.UseVisualStyleBackColor = true;
            btnTimKiem.Click += btnTimKiem_Click;
            //
            // dgvNhanVien
            //
            dgvNhanVien.AllowUserToAddRows = false;
            dgvNhanVien.AllowUserToDeleteRows = false;
            dgvNhanVien.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvNhanVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvNhanVien.Location = new Point(12, 252);
            dgvNhanVien.MultiSelect = false;
            dgvNhanVien.Name = "dgvNhanVien";
            dgvNhanVien.ReadOnly = true;
            dgvNhanVien.RowHeadersVisible = false;
            dgvNhanVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvNhanVien.Size = new Size(860, 297);
            dgvNhanVien.TabIndex = 8;
            dgvNhanVien.CellFormatting += dgvNhanVien_CellFormatting;
            dgvNhanVien.SelectionChanged += dgvNhanVien_SelectionChanged;
            //
            // FrmNhanVien
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(884, 561);
            Controls.Add(grpThongTin);
            Controls.Add(grpTaiKhoan);
            Controls.Add(btnThem);
            Controls.Add(btnSua);
            Controls.Add(btnXoa);
            Controls.Add(btnLamMoi);
            Controls.Add(lblTimKiem);
            Controls.Add(txtTimKiem);
            Controls.Add(btnTimKiem);
            Controls.Add(dgvNhanVien);
            MinimumSize = new Size(900, 600);
            Name = "FrmNhanVien";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý nhân viên";
            Load += FrmNhanVien_Load;
            grpThongTin.ResumeLayout(false);
            grpThongTin.PerformLayout();
            grpTaiKhoan.ResumeLayout(false);
            grpTaiKhoan.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvNhanVien).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox grpThongTin;
        private Label lblMa;
        private TextBox txtMa;
        private Label lblHoTen;
        private TextBox txtHoTen;
        private Label lblGioiTinh;
        private ComboBox cboGioiTinh;
        private Label lblSdt;
        private TextBox txtSdt;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblNgaySinh;
        private DateTimePicker dtpNgaySinh;
        private Label lblDiaChi;
        private TextBox txtDiaChi;
        private Label lblNgayVaoLam;
        private DateTimePicker dtpNgayVaoLam;
        private Label lblTrangThai;
        private ComboBox cboTrangThai;
        private GroupBox grpTaiKhoan;
        private Label lblTenDangNhap;
        private TextBox txtTenDangNhap;
        private Label lblMatKhau;
        private TextBox txtMatKhau;
        private Label lblVaiTro;
        private ComboBox cboVaiTro;
        private Label lblTrangThaiTK;
        private ComboBox cboTrangThaiTK;
        private Button btnLuuTaiKhoan;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLamMoi;
        private Label lblTimKiem;
        private TextBox txtTimKiem;
        private Button btnTimKiem;
        private DataGridView dgvNhanVien;
    }
}
