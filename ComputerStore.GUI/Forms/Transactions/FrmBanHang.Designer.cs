namespace ComputerStore.GUI.Forms.Transactions
{
    partial class FrmBanHang
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
            grpHoaDon = new GroupBox();
            lblKhachHang = new Label();
            cboKhachHang = new ComboBox();
            btnThemKhachHang = new Button();
            lblGhiChu = new Label();
            txtGhiChu = new TextBox();
            lblNhanVien = new Label();
            txtNhanVien = new TextBox();
            lblNgayLap = new Label();
            txtNgayLap = new TextBox();
            grpSanPham = new GroupBox();
            lblSanPham = new Label();
            cboSanPham = new ComboBox();
            lblGiaBan = new Label();
            lblTonKho = new Label();
            lblSoLuong = new Label();
            numSoLuong = new NumericUpDown();
            btnThemVaoGio = new Button();
            btnXoaKhoiGio = new Button();
            dgvGioHang = new DataGridView();
            pnlThanhToan = new Panel();
            lblTongTienText = new Label();
            lblTongTien = new Label();
            lblTienKhachDua = new Label();
            numTienKhachDua = new NumericUpDown();
            lblTienThoiText = new Label();
            lblTienThoi = new Label();
            btnThanhToan = new Button();
            btnHuy = new Button();
            grpHoaDon.SuspendLayout();
            grpSanPham.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numSoLuong).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvGioHang).BeginInit();
            pnlThanhToan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numTienKhachDua).BeginInit();
            SuspendLayout();
            // 
            // grpHoaDon
            // 
            grpHoaDon.Controls.Add(lblKhachHang);
            grpHoaDon.Controls.Add(cboKhachHang);
            grpHoaDon.Controls.Add(btnThemKhachHang);
            grpHoaDon.Controls.Add(lblGhiChu);
            grpHoaDon.Controls.Add(txtGhiChu);
            grpHoaDon.Controls.Add(lblNhanVien);
            grpHoaDon.Controls.Add(txtNhanVien);
            grpHoaDon.Controls.Add(lblNgayLap);
            grpHoaDon.Controls.Add(txtNgayLap);
            grpHoaDon.Location = new Point(14, 16);
            grpHoaDon.Margin = new Padding(3, 4, 3, 4);
            grpHoaDon.Name = "grpHoaDon";
            grpHoaDon.Padding = new Padding(3, 4, 3, 4);
            grpHoaDon.Size = new Size(983, 127);
            grpHoaDon.TabIndex = 0;
            grpHoaDon.TabStop = false;
            grpHoaDon.Text = "Thông tin hóa đơn";
            // 
            // lblKhachHang
            // 
            lblKhachHang.AutoSize = true;
            lblKhachHang.Location = new Point(17, 39);
            lblKhachHang.Name = "lblKhachHang";
            lblKhachHang.Size = new Size(89, 20);
            lblKhachHang.TabIndex = 0;
            lblKhachHang.Text = "Khách hàng:";
            // 
            // cboKhachHang
            // 
            cboKhachHang.DropDownStyle = ComboBoxStyle.DropDownList;
            cboKhachHang.Location = new Point(114, 35);
            cboKhachHang.Margin = new Padding(3, 4, 3, 4);
            cboKhachHang.Name = "cboKhachHang";
            cboKhachHang.Size = new Size(319, 28);
            cboKhachHang.TabIndex = 1;
            // 
            // btnThemKhachHang
            // 
            btnThemKhachHang.Location = new Point(441, 32);
            btnThemKhachHang.Margin = new Padding(3, 4, 3, 4);
            btnThemKhachHang.Name = "btnThemKhachHang";
            btnThemKhachHang.Size = new Size(80, 36);
            btnThemKhachHang.TabIndex = 2;
            btnThemKhachHang.Text = "+ KH";
            btnThemKhachHang.UseVisualStyleBackColor = true;
            btnThemKhachHang.Click += btnThemKhachHang_Click;
            // 
            // lblGhiChu
            // 
            lblGhiChu.AutoSize = true;
            lblGhiChu.Location = new Point(543, 39);
            lblGhiChu.Name = "lblGhiChu";
            lblGhiChu.Size = new Size(61, 20);
            lblGhiChu.TabIndex = 3;
            lblGhiChu.Text = "Ghi chú:";
            // 
            // txtGhiChu
            // 
            txtGhiChu.Location = new Point(612, 37);
            txtGhiChu.Margin = new Padding(3, 4, 3, 4);
            txtGhiChu.MaxLength = 255;
            txtGhiChu.Name = "txtGhiChu";
            txtGhiChu.Size = new Size(348, 27);
            txtGhiChu.TabIndex = 3;
            // 
            // lblNhanVien
            //
            lblNhanVien.AutoSize = true;
            lblNhanVien.Location = new Point(17, 84);
            lblNhanVien.Name = "lblNhanVien";
            lblNhanVien.Size = new Size(79, 20);
            lblNhanVien.TabIndex = 5;
            lblNhanVien.Text = "Nhân viên:";
            //
            // txtNhanVien
            //
            txtNhanVien.Location = new Point(114, 81);
            txtNhanVien.Margin = new Padding(3, 4, 3, 4);
            txtNhanVien.Name = "txtNhanVien";
            txtNhanVien.ReadOnly = true;
            txtNhanVien.Size = new Size(407, 27);
            txtNhanVien.TabIndex = 6;
            txtNhanVien.TabStop = false;
            //
            // lblNgayLap
            //
            lblNgayLap.AutoSize = true;
            lblNgayLap.Location = new Point(543, 84);
            lblNgayLap.Name = "lblNgayLap";
            lblNgayLap.Size = new Size(71, 20);
            lblNgayLap.TabIndex = 7;
            lblNgayLap.Text = "Ngày lập:";
            //
            // txtNgayLap
            //
            txtNgayLap.Location = new Point(612, 81);
            txtNgayLap.Margin = new Padding(3, 4, 3, 4);
            txtNgayLap.Name = "txtNgayLap";
            txtNgayLap.ReadOnly = true;
            txtNgayLap.Size = new Size(348, 27);
            txtNgayLap.TabIndex = 8;
            txtNgayLap.TabStop = false;
            //
            // grpSanPham
            //
            grpSanPham.Controls.Add(lblSanPham);
            grpSanPham.Controls.Add(cboSanPham);
            grpSanPham.Controls.Add(lblGiaBan);
            grpSanPham.Controls.Add(lblTonKho);
            grpSanPham.Controls.Add(lblSoLuong);
            grpSanPham.Controls.Add(numSoLuong);
            grpSanPham.Controls.Add(btnThemVaoGio);
            grpSanPham.Controls.Add(btnXoaKhoiGio);
            grpSanPham.Location = new Point(14, 153);
            grpSanPham.Margin = new Padding(3, 4, 3, 4);
            grpSanPham.Name = "grpSanPham";
            grpSanPham.Padding = new Padding(3, 4, 3, 4);
            grpSanPham.Size = new Size(983, 133);
            grpSanPham.TabIndex = 4;
            grpSanPham.TabStop = false;
            grpSanPham.Text = "Chọn sản phẩm";
            // 
            // lblSanPham
            // 
            lblSanPham.AutoSize = true;
            lblSanPham.Location = new Point(17, 39);
            lblSanPham.Name = "lblSanPham";
            lblSanPham.Size = new Size(78, 20);
            lblSanPham.TabIndex = 0;
            lblSanPham.Text = "Sản phẩm:";
            // 
            // cboSanPham
            // 
            cboSanPham.DropDownStyle = ComboBoxStyle.DropDownList;
            cboSanPham.Location = new Point(114, 35);
            cboSanPham.Margin = new Padding(3, 4, 3, 4);
            cboSanPham.Name = "cboSanPham";
            cboSanPham.Size = new Size(479, 28);
            cboSanPham.TabIndex = 5;
            cboSanPham.SelectedIndexChanged += cboSanPham_SelectedIndexChanged;
            // 
            // lblGiaBan
            // 
            lblGiaBan.AutoSize = true;
            lblGiaBan.Location = new Point(617, 39);
            lblGiaBan.Name = "lblGiaBan";
            lblGiaBan.Size = new Size(73, 20);
            lblGiaBan.TabIndex = 6;
            lblGiaBan.Text = "Giá bán: -";
            // 
            // lblTonKho
            // 
            lblTonKho.AutoSize = true;
            lblTonKho.ForeColor = Color.DimGray;
            lblTonKho.Location = new Point(823, 39);
            lblTonKho.Name = "lblTonKho";
            lblTonKho.Size = new Size(75, 20);
            lblTonKho.TabIndex = 7;
            lblTonKho.Text = "Tồn kho: -";
            // 
            // lblSoLuong
            // 
            lblSoLuong.AutoSize = true;
            lblSoLuong.Location = new Point(17, 87);
            lblSoLuong.Name = "lblSoLuong";
            lblSoLuong.Size = new Size(72, 20);
            lblSoLuong.TabIndex = 8;
            lblSoLuong.Text = "Số lượng:";
            // 
            // numSoLuong
            // 
            numSoLuong.Location = new Point(114, 83);
            numSoLuong.Margin = new Padding(3, 4, 3, 4);
            numSoLuong.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numSoLuong.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numSoLuong.Name = "numSoLuong";
            numSoLuong.Size = new Size(114, 27);
            numSoLuong.TabIndex = 6;
            numSoLuong.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // btnThemVaoGio
            // 
            btnThemVaoGio.Location = new Point(617, 77);
            btnThemVaoGio.Margin = new Padding(3, 4, 3, 4);
            btnThemVaoGio.Name = "btnThemVaoGio";
            btnThemVaoGio.Size = new Size(166, 40);
            btnThemVaoGio.TabIndex = 7;
            btnThemVaoGio.Text = "Thêm vào giỏ";
            btnThemVaoGio.UseVisualStyleBackColor = true;
            btnThemVaoGio.Click += btnThemVaoGio_Click;
            // 
            // btnXoaKhoiGio
            // 
            btnXoaKhoiGio.Location = new Point(794, 77);
            btnXoaKhoiGio.Margin = new Padding(3, 4, 3, 4);
            btnXoaKhoiGio.Name = "btnXoaKhoiGio";
            btnXoaKhoiGio.Size = new Size(166, 40);
            btnXoaKhoiGio.TabIndex = 8;
            btnXoaKhoiGio.Text = "Xóa khỏi giỏ";
            btnXoaKhoiGio.UseVisualStyleBackColor = true;
            btnXoaKhoiGio.Click += btnXoaKhoiGio_Click;
            // 
            // dgvGioHang
            // 
            dgvGioHang.AllowUserToAddRows = false;
            dgvGioHang.AllowUserToDeleteRows = false;
            dgvGioHang.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvGioHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvGioHang.ColumnHeadersHeight = 29;
            dgvGioHang.Location = new Point(14, 300);
            dgvGioHang.Margin = new Padding(3, 4, 3, 4);
            dgvGioHang.MultiSelect = false;
            dgvGioHang.Name = "dgvGioHang";
            dgvGioHang.ReadOnly = true;
            dgvGioHang.RowHeadersVisible = false;
            dgvGioHang.RowHeadersWidth = 51;
            dgvGioHang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvGioHang.Size = new Size(983, 313);
            dgvGioHang.TabIndex = 9;
            // 
            // pnlThanhToan
            // 
            pnlThanhToan.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlThanhToan.Controls.Add(lblTongTienText);
            pnlThanhToan.Controls.Add(lblTongTien);
            pnlThanhToan.Controls.Add(lblTienKhachDua);
            pnlThanhToan.Controls.Add(numTienKhachDua);
            pnlThanhToan.Controls.Add(lblTienThoiText);
            pnlThanhToan.Controls.Add(lblTienThoi);
            pnlThanhToan.Controls.Add(btnThanhToan);
            pnlThanhToan.Controls.Add(btnHuy);
            pnlThanhToan.Location = new Point(14, 621);
            pnlThanhToan.Margin = new Padding(3, 4, 3, 4);
            pnlThanhToan.Name = "pnlThanhToan";
            pnlThanhToan.Size = new Size(983, 113);
            pnlThanhToan.TabIndex = 10;
            // 
            // lblTongTienText
            // 
            lblTongTienText.AutoSize = true;
            lblTongTienText.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTongTienText.Location = new Point(0, 11);
            lblTongTienText.Name = "lblTongTienText";
            lblTongTienText.Size = new Size(103, 25);
            lblTongTienText.TabIndex = 0;
            lblTongTienText.Text = "Tổng tiền:";
            // 
            // lblTongTien
            // 
            lblTongTien.AutoSize = true;
            lblTongTien.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTongTien.ForeColor = Color.Firebrick;
            lblTongTien.Location = new Point(137, 11);
            lblTongTien.Name = "lblTongTien";
            lblTongTien.Size = new Size(40, 25);
            lblTongTien.TabIndex = 1;
            lblTongTien.Text = "0 đ";
            // 
            // lblTienKhachDua
            // 
            lblTienKhachDua.AutoSize = true;
            lblTienKhachDua.Location = new Point(0, 60);
            lblTienKhachDua.Name = "lblTienKhachDua";
            lblTienKhachDua.Size = new Size(112, 20);
            lblTienKhachDua.TabIndex = 2;
            lblTienKhachDua.Text = "Tiền khách đưa:";
            // 
            // numTienKhachDua
            // 
            numTienKhachDua.Increment = new decimal(new int[] { 10000, 0, 0, 0 });
            numTienKhachDua.Location = new Point(137, 56);
            numTienKhachDua.Margin = new Padding(3, 4, 3, 4);
            numTienKhachDua.Maximum = new decimal(new int[] { 1215752192, 23, 0, 0 });
            numTienKhachDua.Name = "numTienKhachDua";
            numTienKhachDua.Size = new Size(183, 27);
            numTienKhachDua.TabIndex = 11;
            numTienKhachDua.ThousandsSeparator = true;
            numTienKhachDua.ValueChanged += numTienKhachDua_ValueChanged;
            // 
            // lblTienThoiText
            // 
            lblTienThoiText.AutoSize = true;
            lblTienThoiText.Location = new Point(343, 60);
            lblTienThoiText.Name = "lblTienThoiText";
            lblTienThoiText.Size = new Size(70, 20);
            lblTienThoiText.TabIndex = 12;
            lblTienThoiText.Text = "Tiền thối:";
            // 
            // lblTienThoi
            // 
            lblTienThoi.AutoSize = true;
            lblTienThoi.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTienThoi.Location = new Point(423, 60);
            lblTienThoi.Name = "lblTienThoi";
            lblTienThoi.Size = new Size(31, 20);
            lblTienThoi.TabIndex = 13;
            lblTienThoi.Text = "0 đ";
            // 
            // btnThanhToan
            // 
            btnThanhToan.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnThanhToan.Location = new Point(709, 47);
            btnThanhToan.Margin = new Padding(3, 4, 3, 4);
            btnThanhToan.Name = "btnThanhToan";
            btnThanhToan.Size = new Size(160, 48);
            btnThanhToan.TabIndex = 12;
            btnThanhToan.Text = "Thanh toán";
            btnThanhToan.UseVisualStyleBackColor = true;
            btnThanhToan.Click += btnThanhToan_Click;
            // 
            // btnHuy
            // 
            btnHuy.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnHuy.Location = new Point(880, 47);
            btnHuy.Margin = new Padding(3, 4, 3, 4);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(103, 48);
            btnHuy.TabIndex = 13;
            btnHuy.Text = "Hủy";
            btnHuy.UseVisualStyleBackColor = true;
            btnHuy.Click += btnHuy_Click;
            // 
            // FrmBanHang
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1010, 748);
            Controls.Add(grpHoaDon);
            Controls.Add(grpSanPham);
            Controls.Add(dgvGioHang);
            Controls.Add(pnlThanhToan);
            Margin = new Padding(3, 4, 3, 4);
            MinimumSize = new Size(1026, 784);
            Name = "FrmBanHang";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Bán hàng";
            Load += FrmBanHang_Load;
            grpHoaDon.ResumeLayout(false);
            grpHoaDon.PerformLayout();
            grpSanPham.ResumeLayout(false);
            grpSanPham.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numSoLuong).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvGioHang).EndInit();
            pnlThanhToan.ResumeLayout(false);
            pnlThanhToan.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numTienKhachDua).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox grpHoaDon;
        private Label lblKhachHang;
        private ComboBox cboKhachHang;
        private Button btnThemKhachHang;
        private Label lblGhiChu;
        private TextBox txtGhiChu;
        private Label lblNhanVien;
        private TextBox txtNhanVien;
        private Label lblNgayLap;
        private TextBox txtNgayLap;
        private GroupBox grpSanPham;
        private Label lblSanPham;
        private ComboBox cboSanPham;
        private Label lblGiaBan;
        private Label lblTonKho;
        private Label lblSoLuong;
        private NumericUpDown numSoLuong;
        private Button btnThemVaoGio;
        private Button btnXoaKhoiGio;
        private DataGridView dgvGioHang;
        private Panel pnlThanhToan;
        private Label lblTongTienText;
        private Label lblTongTien;
        private Label lblTienKhachDua;
        private NumericUpDown numTienKhachDua;
        private Label lblTienThoiText;
        private Label lblTienThoi;
        private Button btnThanhToan;
        private Button btnHuy;
    }
}
