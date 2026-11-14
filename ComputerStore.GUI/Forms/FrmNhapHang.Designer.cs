namespace ComputerStore.GUI.Forms
{
    partial class FrmNhapHang
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
            grpPhieu = new GroupBox();
            lblNhaCungCap = new Label();
            cboNhaCungCap = new ComboBox();
            lblGhiChu = new Label();
            txtGhiChu = new TextBox();
            grpSanPham = new GroupBox();
            lblSanPham = new Label();
            cboSanPham = new ComboBox();
            lblTonKho = new Label();
            lblSoLuong = new Label();
            numSoLuong = new NumericUpDown();
            lblDonGia = new Label();
            numDonGia = new NumericUpDown();
            btnThemSP = new Button();
            btnXoaSP = new Button();
            dgvChiTiet = new DataGridView();
            lblTongTienText = new Label();
            lblTongTien = new Label();
            btnXacNhan = new Button();
            btnHuy = new Button();
            grpPhieu.SuspendLayout();
            grpSanPham.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numSoLuong).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numDonGia).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvChiTiet).BeginInit();
            SuspendLayout();
            //
            // grpPhieu
            //
            grpPhieu.Controls.Add(lblNhaCungCap);
            grpPhieu.Controls.Add(cboNhaCungCap);
            grpPhieu.Controls.Add(lblGhiChu);
            grpPhieu.Controls.Add(txtGhiChu);
            grpPhieu.Location = new Point(12, 12);
            grpPhieu.Name = "grpPhieu";
            grpPhieu.Size = new Size(860, 65);
            grpPhieu.TabIndex = 0;
            grpPhieu.TabStop = false;
            grpPhieu.Text = "Thông tin phiếu nhập";
            //
            // lblNhaCungCap
            //
            lblNhaCungCap.AutoSize = true;
            lblNhaCungCap.Location = new Point(15, 29);
            lblNhaCungCap.Name = "lblNhaCungCap";
            lblNhaCungCap.Text = "Nhà cung cấp:";
            //
            // cboNhaCungCap
            //
            cboNhaCungCap.DropDownStyle = ComboBoxStyle.DropDownList;
            cboNhaCungCap.Location = new Point(110, 26);
            cboNhaCungCap.Name = "cboNhaCungCap";
            cboNhaCungCap.Size = new Size(320, 23);
            cboNhaCungCap.TabIndex = 1;
            //
            // lblGhiChu
            //
            lblGhiChu.AutoSize = true;
            lblGhiChu.Location = new Point(455, 29);
            lblGhiChu.Name = "lblGhiChu";
            lblGhiChu.Text = "Ghi chú:";
            //
            // txtGhiChu
            //
            txtGhiChu.Location = new Point(515, 26);
            txtGhiChu.MaxLength = 255;
            txtGhiChu.Name = "txtGhiChu";
            txtGhiChu.Size = new Size(325, 23);
            txtGhiChu.TabIndex = 2;
            //
            // grpSanPham
            //
            grpSanPham.Controls.Add(lblSanPham);
            grpSanPham.Controls.Add(cboSanPham);
            grpSanPham.Controls.Add(lblTonKho);
            grpSanPham.Controls.Add(lblSoLuong);
            grpSanPham.Controls.Add(numSoLuong);
            grpSanPham.Controls.Add(lblDonGia);
            grpSanPham.Controls.Add(numDonGia);
            grpSanPham.Controls.Add(btnThemSP);
            grpSanPham.Controls.Add(btnXoaSP);
            grpSanPham.Location = new Point(12, 85);
            grpSanPham.Name = "grpSanPham";
            grpSanPham.Size = new Size(860, 100);
            grpSanPham.TabIndex = 3;
            grpSanPham.TabStop = false;
            grpSanPham.Text = "Thêm sản phẩm vào phiếu";
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
            cboSanPham.Location = new Point(110, 26);
            cboSanPham.Name = "cboSanPham";
            cboSanPham.Size = new Size(420, 23);
            cboSanPham.TabIndex = 4;
            cboSanPham.SelectedIndexChanged += cboSanPham_SelectedIndexChanged;
            //
            // lblTonKho
            //
            lblTonKho.AutoSize = true;
            lblTonKho.ForeColor = Color.DimGray;
            lblTonKho.Location = new Point(545, 29);
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
            numSoLuong.Location = new Point(110, 62);
            numSoLuong.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numSoLuong.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numSoLuong.Name = "numSoLuong";
            numSoLuong.Size = new Size(100, 23);
            numSoLuong.TabIndex = 5;
            numSoLuong.Value = new decimal(new int[] { 1, 0, 0, 0 });
            //
            // lblDonGia
            //
            lblDonGia.AutoSize = true;
            lblDonGia.Location = new Point(230, 65);
            lblDonGia.Name = "lblDonGia";
            lblDonGia.Text = "Đơn giá nhập:";
            //
            // numDonGia
            //
            numDonGia.Increment = new decimal(new int[] { 10000, 0, 0, 0 });
            numDonGia.Location = new Point(320, 62);
            numDonGia.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            numDonGia.Name = "numDonGia";
            numDonGia.Size = new Size(150, 23);
            numDonGia.TabIndex = 6;
            numDonGia.ThousandsSeparator = true;
            //
            // btnThemSP
            //
            btnThemSP.Location = new Point(545, 58);
            btnThemSP.Name = "btnThemSP";
            btnThemSP.Size = new Size(140, 30);
            btnThemSP.TabIndex = 7;
            btnThemSP.Text = "Thêm vào phiếu";
            btnThemSP.UseVisualStyleBackColor = true;
            btnThemSP.Click += btnThemSP_Click;
            //
            // btnXoaSP
            //
            btnXoaSP.Location = new Point(695, 58);
            btnXoaSP.Name = "btnXoaSP";
            btnXoaSP.Size = new Size(145, 30);
            btnXoaSP.TabIndex = 8;
            btnXoaSP.Text = "Xóa dòng đã chọn";
            btnXoaSP.UseVisualStyleBackColor = true;
            btnXoaSP.Click += btnXoaSP_Click;
            //
            // dgvChiTiet
            //
            dgvChiTiet.AllowUserToAddRows = false;
            dgvChiTiet.AllowUserToDeleteRows = false;
            dgvChiTiet.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvChiTiet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvChiTiet.Location = new Point(12, 195);
            dgvChiTiet.MultiSelect = false;
            dgvChiTiet.Name = "dgvChiTiet";
            dgvChiTiet.ReadOnly = true;
            dgvChiTiet.RowHeadersVisible = false;
            dgvChiTiet.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvChiTiet.Size = new Size(860, 300);
            dgvChiTiet.TabIndex = 9;
            //
            // lblTongTienText
            //
            lblTongTienText.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblTongTienText.AutoSize = true;
            lblTongTienText.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTongTienText.Location = new Point(12, 515);
            lblTongTienText.Name = "lblTongTienText";
            lblTongTienText.Text = "Tổng tiền:";
            //
            // lblTongTien
            //
            lblTongTien.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblTongTien.AutoSize = true;
            lblTongTien.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTongTien.ForeColor = Color.Firebrick;
            lblTongTien.Location = new Point(100, 515);
            lblTongTien.Name = "lblTongTien";
            lblTongTien.Text = "0 đ";
            //
            // btnXacNhan
            //
            btnXacNhan.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnXacNhan.Location = new Point(632, 508);
            btnXacNhan.Name = "btnXacNhan";
            btnXacNhan.Size = new Size(140, 36);
            btnXacNhan.TabIndex = 10;
            btnXacNhan.Text = "Xác nhận nhập";
            btnXacNhan.UseVisualStyleBackColor = true;
            btnXacNhan.Click += btnXacNhan_Click;
            //
            // btnHuy
            //
            btnHuy.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnHuy.Location = new Point(782, 508);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(90, 36);
            btnHuy.TabIndex = 11;
            btnHuy.Text = "Hủy phiếu";
            btnHuy.UseVisualStyleBackColor = true;
            btnHuy.Click += btnHuy_Click;
            //
            // FrmNhapHang
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(884, 561);
            Controls.Add(grpPhieu);
            Controls.Add(grpSanPham);
            Controls.Add(dgvChiTiet);
            Controls.Add(lblTongTienText);
            Controls.Add(lblTongTien);
            Controls.Add(btnXacNhan);
            Controls.Add(btnHuy);
            MinimumSize = new Size(900, 600);
            Name = "FrmNhapHang";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Lập phiếu nhập hàng";
            Load += FrmNhapHang_Load;
            grpPhieu.ResumeLayout(false);
            grpPhieu.PerformLayout();
            grpSanPham.ResumeLayout(false);
            grpSanPham.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numSoLuong).EndInit();
            ((System.ComponentModel.ISupportInitialize)numDonGia).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvChiTiet).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox grpPhieu;
        private Label lblNhaCungCap;
        private ComboBox cboNhaCungCap;
        private Label lblGhiChu;
        private TextBox txtGhiChu;
        private GroupBox grpSanPham;
        private Label lblSanPham;
        private ComboBox cboSanPham;
        private Label lblTonKho;
        private Label lblSoLuong;
        private NumericUpDown numSoLuong;
        private Label lblDonGia;
        private NumericUpDown numDonGia;
        private Button btnThemSP;
        private Button btnXoaSP;
        private DataGridView dgvChiTiet;
        private Label lblTongTienText;
        private Label lblTongTien;
        private Button btnXacNhan;
        private Button btnHuy;
    }
}
