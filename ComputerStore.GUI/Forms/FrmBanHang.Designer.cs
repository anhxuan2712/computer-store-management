namespace ComputerStore.GUI.Forms
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
            grpHoaDon.Location = new Point(12, 12);
            grpHoaDon.Name = "grpHoaDon";
            grpHoaDon.Size = new Size(860, 65);
            grpHoaDon.TabIndex = 0;
            grpHoaDon.TabStop = false;
            grpHoaDon.Text = "Thông tin hóa đơn";
            //
            // lblKhachHang
            //
            lblKhachHang.AutoSize = true;
            lblKhachHang.Location = new Point(15, 29);
            lblKhachHang.Name = "lblKhachHang";
            lblKhachHang.Text = "Khách hàng:";
            //
            // cboKhachHang
            //
            cboKhachHang.DropDownStyle = ComboBoxStyle.DropDownList;
            cboKhachHang.Location = new Point(100, 26);
            cboKhachHang.Name = "cboKhachHang";
            cboKhachHang.Size = new Size(280, 23);
            cboKhachHang.TabIndex = 1;
            //
            // btnThemKhachHang
            //
            btnThemKhachHang.Location = new Point(386, 24);
            btnThemKhachHang.Name = "btnThemKhachHang";
            btnThemKhachHang.Size = new Size(70, 27);
            btnThemKhachHang.TabIndex = 2;
            btnThemKhachHang.Text = "+ KH";
            btnThemKhachHang.UseVisualStyleBackColor = true;
            btnThemKhachHang.Click += btnThemKhachHang_Click;
            //
            // lblGhiChu
            //
            lblGhiChu.AutoSize = true;
            lblGhiChu.Location = new Point(475, 29);
            lblGhiChu.Name = "lblGhiChu";
            lblGhiChu.Text = "Ghi chú:";
            //
            // txtGhiChu
            //
            txtGhiChu.Location = new Point(535, 26);
            txtGhiChu.MaxLength = 255;
            txtGhiChu.Name = "txtGhiChu";
            txtGhiChu.Size = new Size(305, 23);
            txtGhiChu.TabIndex = 3;
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
            grpSanPham.Location = new Point(12, 85);
            grpSanPham.Name = "grpSanPham";
            grpSanPham.Size = new Size(860, 100);
            grpSanPham.TabIndex = 4;
            grpSanPham.TabStop = false;
            grpSanPham.Text = "Chọn sản phẩm";
            //
            // lblSanPham
            //
            lblSanPham.AutoSize = true;
            lblSanPham.Location = new Point(15, 29);
            lblSanPham.Name = "lblSanPham";
            lblSanPham.Text = "Sản phẩm:";
            //
            // cboSanPham
            //
            cboSanPham.DropDownStyle = ComboBoxStyle.DropDownList;
            cboSanPham.Location = new Point(100, 26);
            cboSanPham.Name = "cboSanPham";
            cboSanPham.Size = new Size(420, 23);
            cboSanPham.TabIndex = 5;
            cboSanPham.SelectedIndexChanged += cboSanPham_SelectedIndexChanged;
            //
            // lblGiaBan
            //
            lblGiaBan.AutoSize = true;
            lblGiaBan.Location = new Point(540, 29);
            lblGiaBan.Name = "lblGiaBan";
            lblGiaBan.Text = "Giá bán: -";
            //
            // lblTonKho
            //
            lblTonKho.AutoSize = true;
            lblTonKho.ForeColor = Color.DimGray;
            lblTonKho.Location = new Point(720, 29);
            lblTonKho.Name = "lblTonKho";
            lblTonKho.Text = "Tồn kho: -";
            //
            // lblSoLuong
            //
            lblSoLuong.AutoSize = true;
            lblSoLuong.Location = new Point(15, 65);
            lblSoLuong.Name = "lblSoLuong";
            lblSoLuong.Text = "Số lượng:";
            //
            // numSoLuong
            //
            numSoLuong.Location = new Point(100, 62);
            numSoLuong.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numSoLuong.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numSoLuong.Name = "numSoLuong";
            numSoLuong.Size = new Size(100, 23);
            numSoLuong.TabIndex = 6;
            numSoLuong.Value = new decimal(new int[] { 1, 0, 0, 0 });
            //
            // btnThemVaoGio
            //
            btnThemVaoGio.Location = new Point(540, 58);
            btnThemVaoGio.Name = "btnThemVaoGio";
            btnThemVaoGio.Size = new Size(145, 30);
            btnThemVaoGio.TabIndex = 7;
            btnThemVaoGio.Text = "Thêm vào giỏ";
            btnThemVaoGio.UseVisualStyleBackColor = true;
            btnThemVaoGio.Click += btnThemVaoGio_Click;
            //
            // btnXoaKhoiGio
            //
            btnXoaKhoiGio.Location = new Point(695, 58);
            btnXoaKhoiGio.Name = "btnXoaKhoiGio";
            btnXoaKhoiGio.Size = new Size(145, 30);
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
            dgvGioHang.Location = new Point(12, 195);
            dgvGioHang.MultiSelect = false;
            dgvGioHang.Name = "dgvGioHang";
            dgvGioHang.ReadOnly = true;
            dgvGioHang.RowHeadersVisible = false;
            dgvGioHang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvGioHang.Size = new Size(860, 265);
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
            pnlThanhToan.Location = new Point(12, 466);
            pnlThanhToan.Name = "pnlThanhToan";
            pnlThanhToan.Size = new Size(860, 85);
            pnlThanhToan.TabIndex = 10;
            //
            // lblTongTienText
            //
            lblTongTienText.AutoSize = true;
            lblTongTienText.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTongTienText.Location = new Point(0, 8);
            lblTongTienText.Name = "lblTongTienText";
            lblTongTienText.Text = "Tổng tiền:";
            //
            // lblTongTien
            //
            lblTongTien.AutoSize = true;
            lblTongTien.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTongTien.ForeColor = Color.Firebrick;
            lblTongTien.Location = new Point(120, 8);
            lblTongTien.Name = "lblTongTien";
            lblTongTien.Text = "0 đ";
            //
            // lblTienKhachDua
            //
            lblTienKhachDua.AutoSize = true;
            lblTienKhachDua.Location = new Point(0, 45);
            lblTienKhachDua.Name = "lblTienKhachDua";
            lblTienKhachDua.Text = "Tiền khách đưa:";
            //
            // numTienKhachDua
            //
            numTienKhachDua.Increment = new decimal(new int[] { 10000, 0, 0, 0 });
            numTienKhachDua.Location = new Point(120, 42);
            numTienKhachDua.Maximum = new decimal(new int[] { 1215752192, 23, 0, 0 });
            numTienKhachDua.Name = "numTienKhachDua";
            numTienKhachDua.Size = new Size(160, 23);
            numTienKhachDua.TabIndex = 11;
            numTienKhachDua.ThousandsSeparator = true;
            numTienKhachDua.ValueChanged += numTienKhachDua_ValueChanged;
            //
            // lblTienThoiText
            //
            lblTienThoiText.AutoSize = true;
            lblTienThoiText.Location = new Point(300, 45);
            lblTienThoiText.Name = "lblTienThoiText";
            lblTienThoiText.Text = "Tiền thối:";
            //
            // lblTienThoi
            //
            lblTienThoi.AutoSize = true;
            lblTienThoi.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTienThoi.Location = new Point(370, 45);
            lblTienThoi.Name = "lblTienThoi";
            lblTienThoi.Text = "0 đ";
            //
            // btnThanhToan
            //
            btnThanhToan.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnThanhToan.Location = new Point(620, 35);
            btnThanhToan.Name = "btnThanhToan";
            btnThanhToan.Size = new Size(140, 36);
            btnThanhToan.TabIndex = 12;
            btnThanhToan.Text = "Thanh toán";
            btnThanhToan.UseVisualStyleBackColor = true;
            btnThanhToan.Click += btnThanhToan_Click;
            //
            // btnHuy
            //
            btnHuy.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnHuy.Location = new Point(770, 35);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(90, 36);
            btnHuy.TabIndex = 13;
            btnHuy.Text = "Hủy";
            btnHuy.UseVisualStyleBackColor = true;
            btnHuy.Click += btnHuy_Click;
            //
            // FrmBanHang
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(884, 561);
            Controls.Add(grpHoaDon);
            Controls.Add(grpSanPham);
            Controls.Add(dgvGioHang);
            Controls.Add(pnlThanhToan);
            MinimumSize = new Size(900, 600);
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
