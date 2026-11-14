using System.ComponentModel;
using System.Data;
using ComputerStore.BLL;
using ComputerStore.DTO;

namespace ComputerStore.GUI.Forms
{
    public partial class FrmNhapHang : Form
    {
        private readonly NhapHangBLL bll = new NhapHangBLL();
        private readonly BindingList<ChiTietPhieuNhapDTO> chiTiet = new BindingList<ChiTietPhieuNhapDTO>();

        /// <summary>
        /// Mã nhân viên lập phiếu. FrmMain/FrmLogin gán giá trị này theo tài khoản đăng nhập.
        /// Mặc định NV001 (dữ liệu mẫu) để chạy thử độc lập.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string MaNhanVien { get; set; } = "NV001";

        public FrmNhapHang()
        {
            InitializeComponent();
        }

        private void FrmNhapHang_Load(object? sender, EventArgs e)
        {
            dgvChiTiet.AutoGenerateColumns = false;
            dgvChiTiet.Columns.Add(CreateColumn("MaSanPham", "Mã SP", 15));
            dgvChiTiet.Columns.Add(CreateColumn("TenSanPham", "Tên sản phẩm", 40));
            dgvChiTiet.Columns.Add(CreateColumn("SoLuong", "Số lượng", 12));
            dgvChiTiet.Columns.Add(CreateColumn("DonGiaNhap", "Đơn giá nhập", 16, "N0"));
            dgvChiTiet.Columns.Add(CreateColumn("ThanhTien", "Thành tiền", 17, "N0"));
            dgvChiTiet.DataSource = chiTiet;

            LoadDanhMuc();
        }

        private static DataGridViewTextBoxColumn CreateColumn(string property, string header, int weight, string? format = null)
        {
            var col = new DataGridViewTextBoxColumn
            {
                DataPropertyName = property,
                Name = property,
                HeaderText = header,
                FillWeight = weight
            };
            if (format != null)
            {
                col.DefaultCellStyle.Format = format;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            return col;
        }

        private void LoadDanhMuc()
        {
            try
            {
                cboNhaCungCap.DisplayMember = "TenNhaCungCap";
                cboNhaCungCap.ValueMember = "MaNhaCungCap";
                cboNhaCungCap.DataSource = bll.GetNhaCungCap();
                cboNhaCungCap.SelectedIndex = -1;

                cboSanPham.DisplayMember = "HienThi";
                cboSanPham.ValueMember = "MaSanPham";
                cboSanPham.DataSource = bll.GetSanPham();
                cboSanPham.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                ShowError("Không tải được dữ liệu nhà cung cấp / sản phẩm: " + ex.Message);
            }
        }

        private void cboSanPham_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cboSanPham.SelectedItem is DataRowView row)
            {
                lblTonKho.Text = "Tồn kho: " + row["SoLuongTon"];
                decimal giaNhap = Convert.ToDecimal(row["GiaNhap"]);
                numDonGia.Value = Math.Min(Math.Max(giaNhap, numDonGia.Minimum), numDonGia.Maximum);
            }
            else
            {
                lblTonKho.Text = "Tồn kho: -";
            }
        }

        private void btnThemSP_Click(object? sender, EventArgs e)
        {
            if (cboSanPham.SelectedItem is not DataRowView row)
            {
                ShowWarning("Vui lòng chọn sản phẩm.");
                return;
            }

            if (!bll.ThemChiTiet(chiTiet, row["MaSanPham"].ToString()!, row["TenSanPham"].ToString()!,
                    (int)numSoLuong.Value, numDonGia.Value, out string message))
            {
                ShowWarning(message);
                return;
            }

            chiTiet.ResetBindings();
            numSoLuong.Value = 1;
            CapNhatTongTien();
        }

        private void btnXoaSP_Click(object? sender, EventArgs e)
        {
            if (dgvChiTiet.CurrentRow?.DataBoundItem is ChiTietPhieuNhapDTO item)
            {
                chiTiet.Remove(item);
                CapNhatTongTien();
            }
        }

        private void btnXacNhan_Click(object? sender, EventArgs e)
        {
            if (chiTiet.Count == 0)
            {
                ShowWarning("Phiếu nhập chưa có sản phẩm nào.");
                return;
            }
            if (cboNhaCungCap.SelectedValue is not string maNcc)
            {
                ShowWarning("Vui lòng chọn nhà cung cấp.");
                return;
            }

            decimal tong = bll.TinhTongTien(chiTiet);
            if (MessageBox.Show($"Xác nhận nhập {chiTiet.Count} sản phẩm, tổng tiền {tong:N0} đ?",
                    "Xác nhận phiếu nhập", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            var phieu = new PhieuNhapDTO
            {
                MaNhaCungCap = maNcc,
                MaNhanVien = MaNhanVien,
                GhiChu = txtGhiChu.Text,
                ChiTiet = chiTiet.ToList()
            };

            if (bll.XacNhanNhap(phieu, out string message))
            {
                MessageBox.Show(message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetPhieu();
                LoadDanhMuc(); // tải lại tồn kho mới
            }
            else
            {
                ShowError(message);
            }
        }

        private void btnHuy_Click(object? sender, EventArgs e)
        {
            if (chiTiet.Count > 0 &&
                MessageBox.Show("Hủy phiếu đang lập?", "Xác nhận", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            ResetPhieu();
        }

        private void ResetPhieu()
        {
            chiTiet.Clear();
            txtGhiChu.Clear();
            cboNhaCungCap.SelectedIndex = -1;
            cboSanPham.SelectedIndex = -1;
            numSoLuong.Value = 1;
            numDonGia.Value = 0;
            CapNhatTongTien();
        }

        private void CapNhatTongTien()
        {
            lblTongTien.Text = $"{bll.TinhTongTien(chiTiet):N0} đ";
        }

        private static void ShowWarning(string message) =>
            MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        private static void ShowError(string message) =>
            MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
