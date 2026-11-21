namespace ComputerStore.GUI.Forms.Main
{
    partial class FrmMain
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
            pnlHeader = new Panel();
            lblAppTitle = new Label();
            lblUserInfo = new Label();
            btnDangXuat = new Button();
            pnlSidebar = new Panel();
            btnTrangChu = new Button();
            btnBanHang = new Button();
            btnNhapHang = new Button();
            btnKhachHang = new Button();
            btnSanPham = new Button();
            btnDanhMuc = new Button();
            btnNhaCungCap = new Button();
            btnNhanVien = new Button();
            pnlContent = new Panel();
            pnlHome = new Panel();
            lblWelcome = new Label();
            lblThongTinPhien = new Label();
            grpHuongDan = new GroupBox();
            lblHuongDan = new Label();
            pnlHeader.SuspendLayout();
            pnlSidebar.SuspendLayout();
            pnlContent.SuspendLayout();
            pnlHome.SuspendLayout();
            grpHuongDan.SuspendLayout();
            SuspendLayout();
            //
            // pnlHeader
            //
            pnlHeader.BackColor = Color.FromArgb(31, 78, 121);
            pnlHeader.Controls.Add(lblAppTitle);
            pnlHeader.Controls.Add(lblUserInfo);
            pnlHeader.Controls.Add(btnDangXuat);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1160, 54);
            pnlHeader.TabIndex = 0;
            //
            // lblAppTitle
            //
            lblAppTitle.AutoSize = true;
            lblAppTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblAppTitle.ForeColor = Color.White;
            lblAppTitle.Location = new Point(16, 14);
            lblAppTitle.Name = "lblAppTitle";
            lblAppTitle.Size = new Size(277, 25);
            lblAppTitle.TabIndex = 0;
            lblAppTitle.Text = "QUẢN LÝ CỬA HÀNG MÁY TÍNH";
            //
            // lblUserInfo
            //
            lblUserInfo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblUserInfo.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            lblUserInfo.ForeColor = Color.White;
            lblUserInfo.Location = new Point(620, 17);
            lblUserInfo.Name = "lblUserInfo";
            lblUserInfo.Size = new Size(410, 20);
            lblUserInfo.TabIndex = 1;
            lblUserInfo.Text = "Xin chào:";
            lblUserInfo.TextAlign = ContentAlignment.MiddleRight;
            //
            // btnDangXuat
            //
            btnDangXuat.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnDangXuat.BackColor = Color.White;
            btnDangXuat.FlatStyle = FlatStyle.Flat;
            btnDangXuat.ForeColor = Color.FromArgb(31, 78, 121);
            btnDangXuat.Location = new Point(1042, 12);
            btnDangXuat.Name = "btnDangXuat";
            btnDangXuat.Size = new Size(102, 30);
            btnDangXuat.TabIndex = 2;
            btnDangXuat.Text = "Đăng xuất";
            btnDangXuat.UseVisualStyleBackColor = false;
            btnDangXuat.Click += btnDangXuat_Click;
            //
            // pnlSidebar
            //
            pnlSidebar.BackColor = Color.FromArgb(240, 244, 248);
            pnlSidebar.Controls.Add(btnNhanVien);
            pnlSidebar.Controls.Add(btnNhaCungCap);
            pnlSidebar.Controls.Add(btnDanhMuc);
            pnlSidebar.Controls.Add(btnSanPham);
            pnlSidebar.Controls.Add(btnKhachHang);
            pnlSidebar.Controls.Add(btnNhapHang);
            pnlSidebar.Controls.Add(btnBanHang);
            pnlSidebar.Controls.Add(btnTrangChu);
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Location = new Point(0, 54);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new Size(210, 626);
            pnlSidebar.TabIndex = 1;
            //
            // btnTrangChu
            //
            btnTrangChu.BackColor = Color.FromArgb(240, 244, 248);
            btnTrangChu.FlatAppearance.BorderSize = 0;
            btnTrangChu.FlatStyle = FlatStyle.Flat;
            btnTrangChu.Font = new Font("Segoe UI", 10F);
            btnTrangChu.ForeColor = Color.FromArgb(30, 41, 59);
            btnTrangChu.Location = new Point(12, 16);
            btnTrangChu.Name = "btnTrangChu";
            btnTrangChu.Padding = new Padding(12, 0, 0, 0);
            btnTrangChu.Size = new Size(186, 40);
            btnTrangChu.TabIndex = 0;
            btnTrangChu.Text = "Trang chủ";
            btnTrangChu.TextAlign = ContentAlignment.MiddleLeft;
            btnTrangChu.UseVisualStyleBackColor = false;
            btnTrangChu.Click += btnTrangChu_Click;
            //
            // btnBanHang
            //
            btnBanHang.BackColor = Color.FromArgb(240, 244, 248);
            btnBanHang.FlatAppearance.BorderSize = 0;
            btnBanHang.FlatStyle = FlatStyle.Flat;
            btnBanHang.Font = new Font("Segoe UI", 10F);
            btnBanHang.ForeColor = Color.FromArgb(30, 41, 59);
            btnBanHang.Location = new Point(12, 62);
            btnBanHang.Name = "btnBanHang";
            btnBanHang.Padding = new Padding(12, 0, 0, 0);
            btnBanHang.Size = new Size(186, 40);
            btnBanHang.TabIndex = 1;
            btnBanHang.Text = "Bán hàng";
            btnBanHang.TextAlign = ContentAlignment.MiddleLeft;
            btnBanHang.UseVisualStyleBackColor = false;
            btnBanHang.Click += btnBanHang_Click;
            //
            // btnNhapHang
            //
            btnNhapHang.BackColor = Color.FromArgb(240, 244, 248);
            btnNhapHang.FlatAppearance.BorderSize = 0;
            btnNhapHang.FlatStyle = FlatStyle.Flat;
            btnNhapHang.Font = new Font("Segoe UI", 10F);
            btnNhapHang.ForeColor = Color.FromArgb(30, 41, 59);
            btnNhapHang.Location = new Point(12, 108);
            btnNhapHang.Name = "btnNhapHang";
            btnNhapHang.Padding = new Padding(12, 0, 0, 0);
            btnNhapHang.Size = new Size(186, 40);
            btnNhapHang.TabIndex = 2;
            btnNhapHang.Text = "Nhập hàng";
            btnNhapHang.TextAlign = ContentAlignment.MiddleLeft;
            btnNhapHang.UseVisualStyleBackColor = false;
            btnNhapHang.Click += btnNhapHang_Click;
            //
            // btnKhachHang
            //
            btnKhachHang.BackColor = Color.FromArgb(240, 244, 248);
            btnKhachHang.FlatAppearance.BorderSize = 0;
            btnKhachHang.FlatStyle = FlatStyle.Flat;
            btnKhachHang.Font = new Font("Segoe UI", 10F);
            btnKhachHang.ForeColor = Color.FromArgb(30, 41, 59);
            btnKhachHang.Location = new Point(12, 154);
            btnKhachHang.Name = "btnKhachHang";
            btnKhachHang.Padding = new Padding(12, 0, 0, 0);
            btnKhachHang.Size = new Size(186, 40);
            btnKhachHang.TabIndex = 3;
            btnKhachHang.Text = "Khách hàng";
            btnKhachHang.TextAlign = ContentAlignment.MiddleLeft;
            btnKhachHang.UseVisualStyleBackColor = false;
            btnKhachHang.Click += btnKhachHang_Click;
            //
            // btnSanPham
            //
            btnSanPham.BackColor = Color.FromArgb(240, 244, 248);
            btnSanPham.FlatAppearance.BorderSize = 0;
            btnSanPham.FlatStyle = FlatStyle.Flat;
            btnSanPham.Font = new Font("Segoe UI", 10F);
            btnSanPham.ForeColor = Color.FromArgb(30, 41, 59);
            btnSanPham.Location = new Point(12, 200);
            btnSanPham.Name = "btnSanPham";
            btnSanPham.Padding = new Padding(12, 0, 0, 0);
            btnSanPham.Size = new Size(186, 40);
            btnSanPham.TabIndex = 4;
            btnSanPham.Text = "Sản phẩm";
            btnSanPham.TextAlign = ContentAlignment.MiddleLeft;
            btnSanPham.UseVisualStyleBackColor = false;
            btnSanPham.Click += btnSanPham_Click;
            //
            // btnDanhMuc
            //
            btnDanhMuc.BackColor = Color.FromArgb(240, 244, 248);
            btnDanhMuc.FlatAppearance.BorderSize = 0;
            btnDanhMuc.FlatStyle = FlatStyle.Flat;
            btnDanhMuc.Font = new Font("Segoe UI", 10F);
            btnDanhMuc.ForeColor = Color.FromArgb(30, 41, 59);
            btnDanhMuc.Location = new Point(12, 246);
            btnDanhMuc.Name = "btnDanhMuc";
            btnDanhMuc.Padding = new Padding(12, 0, 0, 0);
            btnDanhMuc.Size = new Size(186, 40);
            btnDanhMuc.TabIndex = 5;
            btnDanhMuc.Text = "Danh mục";
            btnDanhMuc.TextAlign = ContentAlignment.MiddleLeft;
            btnDanhMuc.UseVisualStyleBackColor = false;
            btnDanhMuc.Click += btnDanhMuc_Click;
            //
            // btnNhaCungCap
            //
            btnNhaCungCap.BackColor = Color.FromArgb(240, 244, 248);
            btnNhaCungCap.FlatAppearance.BorderSize = 0;
            btnNhaCungCap.FlatStyle = FlatStyle.Flat;
            btnNhaCungCap.Font = new Font("Segoe UI", 10F);
            btnNhaCungCap.ForeColor = Color.FromArgb(30, 41, 59);
            btnNhaCungCap.Location = new Point(12, 292);
            btnNhaCungCap.Name = "btnNhaCungCap";
            btnNhaCungCap.Padding = new Padding(12, 0, 0, 0);
            btnNhaCungCap.Size = new Size(186, 40);
            btnNhaCungCap.TabIndex = 6;
            btnNhaCungCap.Text = "Nhà cung cấp";
            btnNhaCungCap.TextAlign = ContentAlignment.MiddleLeft;
            btnNhaCungCap.UseVisualStyleBackColor = false;
            btnNhaCungCap.Click += btnNhaCungCap_Click;
            //
            // btnNhanVien
            //
            btnNhanVien.BackColor = Color.FromArgb(240, 244, 248);
            btnNhanVien.FlatAppearance.BorderSize = 0;
            btnNhanVien.FlatStyle = FlatStyle.Flat;
            btnNhanVien.Font = new Font("Segoe UI", 10F);
            btnNhanVien.ForeColor = Color.FromArgb(30, 41, 59);
            btnNhanVien.Location = new Point(12, 338);
            btnNhanVien.Name = "btnNhanVien";
            btnNhanVien.Padding = new Padding(12, 0, 0, 0);
            btnNhanVien.Size = new Size(186, 40);
            btnNhanVien.TabIndex = 7;
            btnNhanVien.Text = "Nhân viên";
            btnNhanVien.TextAlign = ContentAlignment.MiddleLeft;
            btnNhanVien.UseVisualStyleBackColor = false;
            btnNhanVien.Click += btnNhanVien_Click;
            //
            // pnlContent
            //
            pnlContent.AutoScroll = true;
            pnlContent.Controls.Add(pnlHome);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(210, 54);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(950, 626);
            pnlContent.TabIndex = 2;
            //
            // pnlHome
            //
            pnlHome.BackColor = Color.White;
            pnlHome.Controls.Add(grpHuongDan);
            pnlHome.Controls.Add(lblThongTinPhien);
            pnlHome.Controls.Add(lblWelcome);
            pnlHome.Dock = DockStyle.Fill;
            pnlHome.Location = new Point(0, 0);
            pnlHome.Name = "pnlHome";
            pnlHome.Size = new Size(950, 626);
            pnlHome.TabIndex = 0;
            //
            // lblWelcome
            //
            lblWelcome.AutoSize = true;
            lblWelcome.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblWelcome.ForeColor = Color.FromArgb(31, 78, 121);
            lblWelcome.Location = new Point(36, 36);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(511, 30);
            lblWelcome.TabIndex = 0;
            lblWelcome.Text = "Chào mừng đến với Hệ thống Quản lý Cửa hàng Máy tính";
            //
            // lblThongTinPhien
            //
            lblThongTinPhien.AutoSize = true;
            lblThongTinPhien.Font = new Font("Segoe UI", 10F);
            lblThongTinPhien.ForeColor = Color.DimGray;
            lblThongTinPhien.Location = new Point(38, 76);
            lblThongTinPhien.Name = "lblThongTinPhien";
            lblThongTinPhien.Size = new Size(125, 19);
            lblThongTinPhien.TabIndex = 1;
            lblThongTinPhien.Text = "Thông tin phiên làm việc";
            //
            // grpHuongDan
            //
            grpHuongDan.Controls.Add(lblHuongDan);
            grpHuongDan.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpHuongDan.Location = new Point(38, 120);
            grpHuongDan.Name = "grpHuongDan";
            grpHuongDan.Size = new Size(640, 230);
            grpHuongDan.TabIndex = 2;
            grpHuongDan.TabStop = false;
            grpHuongDan.Text = "Hướng dẫn nhanh";
            //
            // lblHuongDan
            //
            lblHuongDan.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            lblHuongDan.Location = new Point(18, 32);
            lblHuongDan.Name = "lblHuongDan";
            lblHuongDan.Size = new Size(600, 180);
            lblHuongDan.TabIndex = 0;
            lblHuongDan.Text =
                "• Bán hàng: Lập hóa đơn bán hàng, kiểm tra và tự động trừ tồn kho.\r\n" +
                "• Nhập hàng: Lập phiếu nhập từ nhà cung cấp và cộng tồn kho.\r\n" +
                "• Khách hàng: Thêm, sửa, xóa và tìm kiếm thông tin khách hàng.\r\n" +
                "• Sản phẩm / Danh mục / Nhà cung cấp: Quản lý hàng hóa và nguồn nhập.\r\n" +
                "• Nhân viên (dành cho Admin): Quản lý hồ sơ nhân viên và cấp/khóa tài khoản.\r\n\r\n" +
                "Chọn một chức năng ở menu bên trái để bắt đầu làm việc.";
            //
            // FrmMain
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1160, 680);
            Controls.Add(pnlContent);
            Controls.Add(pnlSidebar);
            Controls.Add(pnlHeader);
            MinimumSize = new Size(1160, 680);
            Name = "FrmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý cửa hàng máy tính";
            Load += FrmMain_Load;
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlSidebar.ResumeLayout(false);
            pnlContent.ResumeLayout(false);
            pnlHome.ResumeLayout(false);
            pnlHome.PerformLayout();
            grpHuongDan.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlHeader;
        private Label lblAppTitle;
        private Label lblUserInfo;
        private Button btnDangXuat;
        private Panel pnlSidebar;
        private Button btnTrangChu;
        private Button btnBanHang;
        private Button btnNhapHang;
        private Button btnKhachHang;
        private Button btnSanPham;
        private Button btnDanhMuc;
        private Button btnNhaCungCap;
        private Button btnNhanVien;
        private Panel pnlContent;
        private Panel pnlHome;
        private Label lblWelcome;
        private Label lblThongTinPhien;
        private GroupBox grpHuongDan;
        private Label lblHuongDan;
    }
}
