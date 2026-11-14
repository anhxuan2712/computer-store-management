namespace ComputerStore.GUI.Forms
{
    partial class FrmKhachHang
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
            lblTen = new Label();
            txtTen = new TextBox();
            lblSdt = new Label();
            txtSdt = new TextBox();
            lblDiaChi = new Label();
            txtDiaChi = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            btnLamMoi = new Button();
            lblTimKiem = new Label();
            txtTimKiem = new TextBox();
            btnTimKiem = new Button();
            dgvKhachHang = new DataGridView();
            grpThongTin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvKhachHang).BeginInit();
            SuspendLayout();
            //
            // grpThongTin
            //
            grpThongTin.Controls.Add(lblMa);
            grpThongTin.Controls.Add(txtMa);
            grpThongTin.Controls.Add(lblTen);
            grpThongTin.Controls.Add(txtTen);
            grpThongTin.Controls.Add(lblSdt);
            grpThongTin.Controls.Add(txtSdt);
            grpThongTin.Controls.Add(lblDiaChi);
            grpThongTin.Controls.Add(txtDiaChi);
            grpThongTin.Controls.Add(lblEmail);
            grpThongTin.Controls.Add(txtEmail);
            grpThongTin.Location = new Point(12, 12);
            grpThongTin.Name = "grpThongTin";
            grpThongTin.Size = new Size(860, 130);
            grpThongTin.TabIndex = 0;
            grpThongTin.TabStop = false;
            grpThongTin.Text = "Thông tin khách hàng";
            //
            // lblMa
            //
            lblMa.AutoSize = true;
            lblMa.Location = new Point(15, 30);
            lblMa.Name = "lblMa";
            lblMa.Text = "Mã KH:";
            //
            // txtMa
            //
            txtMa.Location = new Point(110, 27);
            txtMa.Name = "txtMa";
            txtMa.ReadOnly = true;
            txtMa.Size = new Size(150, 23);
            txtMa.TabStop = false;
            txtMa.PlaceholderText = "Tự động";
            //
            // lblTen
            //
            lblTen.AutoSize = true;
            lblTen.Location = new Point(290, 30);
            lblTen.Name = "lblTen";
            lblTen.Text = "Tên khách hàng:";
            //
            // txtTen
            //
            txtTen.Location = new Point(400, 27);
            txtTen.MaxLength = 100;
            txtTen.Name = "txtTen";
            txtTen.Size = new Size(440, 23);
            txtTen.TabIndex = 1;
            //
            // lblSdt
            //
            lblSdt.AutoSize = true;
            lblSdt.Location = new Point(15, 63);
            lblSdt.Name = "lblSdt";
            lblSdt.Text = "Số điện thoại:";
            //
            // txtSdt
            //
            txtSdt.Location = new Point(110, 60);
            txtSdt.MaxLength = 11;
            txtSdt.Name = "txtSdt";
            txtSdt.Size = new Size(150, 23);
            txtSdt.TabIndex = 2;
            //
            // lblDiaChi
            //
            lblDiaChi.AutoSize = true;
            lblDiaChi.Location = new Point(290, 63);
            lblDiaChi.Name = "lblDiaChi";
            lblDiaChi.Text = "Địa chỉ:";
            //
            // txtDiaChi
            //
            txtDiaChi.Location = new Point(400, 60);
            txtDiaChi.MaxLength = 255;
            txtDiaChi.Name = "txtDiaChi";
            txtDiaChi.Size = new Size(440, 23);
            txtDiaChi.TabIndex = 3;
            //
            // lblEmail
            //
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(15, 96);
            lblEmail.Name = "lblEmail";
            lblEmail.Text = "Email:";
            //
            // txtEmail
            //
            txtEmail.Location = new Point(110, 93);
            txtEmail.MaxLength = 100;
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(330, 23);
            txtEmail.TabIndex = 4;
            //
            // btnThem
            //
            btnThem.Location = new Point(12, 152);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(90, 30);
            btnThem.TabIndex = 5;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = true;
            btnThem.Click += btnThem_Click;
            //
            // btnSua
            //
            btnSua.Location = new Point(108, 152);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(90, 30);
            btnSua.TabIndex = 6;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = true;
            btnSua.Click += btnSua_Click;
            //
            // btnXoa
            //
            btnXoa.Location = new Point(204, 152);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(90, 30);
            btnXoa.TabIndex = 7;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            btnXoa.Click += btnXoa_Click;
            //
            // btnLamMoi
            //
            btnLamMoi.Location = new Point(300, 152);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(90, 30);
            btnLamMoi.TabIndex = 8;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = true;
            btnLamMoi.Click += btnLamMoi_Click;
            //
            // lblTimKiem
            //
            lblTimKiem.AutoSize = true;
            lblTimKiem.Location = new Point(470, 159);
            lblTimKiem.Name = "lblTimKiem";
            lblTimKiem.Text = "Tìm kiếm:";
            //
            // txtTimKiem
            //
            txtTimKiem.Location = new Point(540, 156);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.PlaceholderText = "Mã, tên hoặc số điện thoại";
            txtTimKiem.Size = new Size(240, 23);
            txtTimKiem.TabIndex = 9;
            txtTimKiem.KeyDown += txtTimKiem_KeyDown;
            //
            // btnTimKiem
            //
            btnTimKiem.Location = new Point(786, 152);
            btnTimKiem.Name = "btnTimKiem";
            btnTimKiem.Size = new Size(86, 30);
            btnTimKiem.TabIndex = 10;
            btnTimKiem.Text = "Tìm";
            btnTimKiem.UseVisualStyleBackColor = true;
            btnTimKiem.Click += btnTimKiem_Click;
            //
            // dgvKhachHang
            //
            dgvKhachHang.AllowUserToAddRows = false;
            dgvKhachHang.AllowUserToDeleteRows = false;
            dgvKhachHang.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvKhachHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvKhachHang.Location = new Point(12, 192);
            dgvKhachHang.MultiSelect = false;
            dgvKhachHang.Name = "dgvKhachHang";
            dgvKhachHang.ReadOnly = true;
            dgvKhachHang.RowHeadersVisible = false;
            dgvKhachHang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvKhachHang.Size = new Size(860, 357);
            dgvKhachHang.TabIndex = 11;
            dgvKhachHang.SelectionChanged += dgvKhachHang_SelectionChanged;
            //
            // FrmKhachHang
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(884, 561);
            Controls.Add(grpThongTin);
            Controls.Add(btnThem);
            Controls.Add(btnSua);
            Controls.Add(btnXoa);
            Controls.Add(btnLamMoi);
            Controls.Add(lblTimKiem);
            Controls.Add(txtTimKiem);
            Controls.Add(btnTimKiem);
            Controls.Add(dgvKhachHang);
            MinimumSize = new Size(900, 600);
            Name = "FrmKhachHang";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý khách hàng";
            Load += FrmKhachHang_Load;
            grpThongTin.ResumeLayout(false);
            grpThongTin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvKhachHang).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox grpThongTin;
        private Label lblMa;
        private TextBox txtMa;
        private Label lblTen;
        private TextBox txtTen;
        private Label lblSdt;
        private TextBox txtSdt;
        private Label lblDiaChi;
        private TextBox txtDiaChi;
        private Label lblEmail;
        private TextBox txtEmail;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLamMoi;
        private Label lblTimKiem;
        private TextBox txtTimKiem;
        private Button btnTimKiem;
        private DataGridView dgvKhachHang;
    }
}
