using System.ComponentModel;
using System.Data;
using ComputerStore.BLL.Services;
using ComputerStore.DTO.Models;

namespace ComputerStore.GUI.Forms.Transactions
{
    public partial class FrmNhapHang : Form
    {
        private readonly NhapHangBLL bll = new NhapHangBLL();
        private readonly BindingList<DongChiTietModel> chiTiet = new BindingList<DongChiTietModel>();

        /// <summary>
        /// Mã nhân viên lập phiếu. FrmMain/FrmLogin gán giá trị này theo tài khoản đăng nhập.
        /// Mặc định 1 (nhân viên đầu tiên trong dữ liệu mẫu) để chạy thử độc lập.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int MaNhanVien { get; set; } = 1;

        /// <summary>Họ tên nhân viên đăng nhập, chỉ dùng để hiển thị.</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string? TenNhanVien { get; set; }

        private const string DinhDangNgayLap = "dd/MM/yyyy HH:mm";

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
            dgvChiTiet.Columns.Add(CreateColumn("DonGia", "Đơn giá nhập", 16, "N0"));
            dgvChiTiet.Columns.Add(CreateColumn("ThanhTien", "Thành tiền", 17, "N0"));
            // Cột Ngày lập không gắn với model: giá trị lấy từ ô Ngày lập của phiếu (CellFormatting).
            dgvChiTiet.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NgayLap",
                HeaderText = "Ngày lập",
                FillWeight = 18
            });
            dgvChiTiet.CellFormatting += dgvChiTiet_CellFormatting;
            dgvChiTiet.DataSource = chiTiet;

            txtNhanVien.Text = string.IsNullOrWhiteSpace(TenNhanVien)
                ? $"Mã NV: {MaNhanVien}"
                : $"Mã NV: {MaNhanVien} - {TenNhanVien}";
            txtNgayLap.Text = DateTime.Now.ToString(DinhDangNgayLap);

            LoadDanhMuc();
        }

        private void dgvChiTiet_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvChiTiet.Columns[e.ColumnIndex].Name == "NgayLap")
            {
                e.Value = txtNgayLap.Text;
                e.FormattingApplied = true;
            }
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

            if (!bll.ThemChiTiet(chiTiet, Convert.ToInt32(row["MaSanPham"]), row["TenSanPham"].ToString()!,
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
            if (dgvChiTiet.CurrentRow?.DataBoundItem is DongChiTietModel item)
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
            if (cboNhaCungCap.SelectedValue == null || cboNhaCungCap.SelectedIndex < 0)
            {
                ShowWarning("Vui lòng chọn nhà cung cấp.");
                return;
            }

            int maNhaCungCap = Convert.ToInt32(cboNhaCungCap.SelectedValue);
            decimal tong = bll.TinhTongTien(chiTiet);
            if (MessageBox.Show($"Xác nhận nhập {chiTiet.Count} sản phẩm, tổng tiền {tong:N0} đ?",
                    "Xác nhận phiếu nhập", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            // Ngày lập lấy đúng lúc xác nhận, hiển thị và lưu cùng một giá trị.
            DateTime ngayLap = DateTime.Now;
            txtNgayLap.Text = ngayLap.ToString(DinhDangNgayLap);

            if (bll.XacNhanNhap(maNhaCungCap, MaNhanVien, txtGhiChu.Text, chiTiet, out string message, ngayLap))
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
            txtNgayLap.Text = DateTime.Now.ToString(DinhDangNgayLap);
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
