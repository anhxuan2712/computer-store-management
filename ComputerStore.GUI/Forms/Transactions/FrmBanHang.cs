using System.ComponentModel;
using System.Data;
using ComputerStore.BLL.Services;
using ComputerStore.DTO.Models;
using ComputerStore.GUI.Forms.Customers;

namespace ComputerStore.GUI.Forms.Transactions
{
    public partial class FrmBanHang : Form
    {
        private readonly BanHangBLL bll = new BanHangBLL();
        private readonly BindingList<DongChiTietModel> gioHang = new BindingList<DongChiTietModel>();

        /// <summary>
        /// Mã nhân viên bán hàng. FrmMain/FrmLogin gán giá trị này theo tài khoản đăng nhập.
        /// Mặc định 1 (nhân viên đầu tiên trong dữ liệu mẫu) để chạy thử độc lập.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int MaNhanVien { get; set; } = 1;

        public FrmBanHang()
        {
            InitializeComponent();
        }

        private void FrmBanHang_Load(object? sender, EventArgs e)
        {
            dgvGioHang.AutoGenerateColumns = false;
            dgvGioHang.Columns.Add(CreateColumn("MaSanPham", "Mã SP", 15));
            dgvGioHang.Columns.Add(CreateColumn("TenSanPham", "Tên sản phẩm", 40));
            dgvGioHang.Columns.Add(CreateColumn("SoLuong", "Số lượng", 12));
            dgvGioHang.Columns.Add(CreateColumn("DonGia", "Đơn giá", 16, "N0"));
            dgvGioHang.Columns.Add(CreateColumn("ThanhTien", "Thành tiền", 17, "N0"));
            dgvGioHang.DataSource = gioHang;

            LoadKhachHang();
            LoadSanPham();
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

        /// <summary>Mã khách hàng đang chọn; null nếu là khách lẻ.</summary>
        private int? MaKhachHangDangChon()
        {
            int ma = cboKhachHang.SelectedValue == null ? 0 : Convert.ToInt32(cboKhachHang.SelectedValue);
            return ma > 0 ? ma : null;
        }

        private void LoadKhachHang(int? chonMaKhachHang = null)
        {
            try
            {
                DataTable table = bll.GetKhachHang();
                DataRow khachLe = table.NewRow();
                khachLe["MaKhachHang"] = 0; // 0 = khách lẻ
                khachLe["HienThi"] = "-- Khách lẻ --";
                table.Rows.InsertAt(khachLe, 0);

                cboKhachHang.DisplayMember = "HienThi";
                cboKhachHang.ValueMember = "MaKhachHang";
                cboKhachHang.DataSource = table;
                if (chonMaKhachHang != null) cboKhachHang.SelectedValue = chonMaKhachHang.Value;
                else cboKhachHang.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ShowError("Không tải được danh sách khách hàng: " + ex.Message);
            }
        }

        private void LoadSanPham()
        {
            try
            {
                cboSanPham.DisplayMember = "HienThi";
                cboSanPham.ValueMember = "MaSanPham";
                cboSanPham.DataSource = bll.GetSanPham();
                cboSanPham.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                ShowError("Không tải được danh sách sản phẩm: " + ex.Message);
            }
        }

        private void cboSanPham_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cboSanPham.SelectedItem is DataRowView row)
            {
                lblGiaBan.Text = $"Giá bán: {Convert.ToDecimal(row["GiaBan"]):N0} đ";
                lblTonKho.Text = "Tồn kho: " + row["SoLuongTon"];
            }
            else
            {
                lblGiaBan.Text = "Giá bán: -";
                lblTonKho.Text = "Tồn kho: -";
            }
        }

        private void btnThemKhachHang_Click(object? sender, EventArgs e)
        {
            int? dangChon = MaKhachHangDangChon();
            using (var frm = new FrmKhachHang())
            {
                frm.ShowDialog(this);
            }
            LoadKhachHang(dangChon);
        }

        private void btnThemVaoGio_Click(object? sender, EventArgs e)
        {
            if (cboSanPham.SelectedItem is not DataRowView row)
            {
                ShowWarning("Vui lòng chọn sản phẩm.");
                return;
            }

            try
            {
                if (!bll.ThemVaoGio(gioHang, Convert.ToInt32(row["MaSanPham"]), row["TenSanPham"].ToString()!,
                        (int)numSoLuong.Value, Convert.ToDecimal(row["GiaBan"]), out string message))
                {
                    ShowWarning(message);
                    return;
                }
            }
            catch (Exception ex)
            {
                ShowError("Không kiểm tra được tồn kho: " + ex.Message);
                return;
            }

            gioHang.ResetBindings();
            numSoLuong.Value = 1;
            CapNhatTongTien();
        }

        private void btnXoaKhoiGio_Click(object? sender, EventArgs e)
        {
            if (dgvGioHang.CurrentRow?.DataBoundItem is DongChiTietModel item)
            {
                gioHang.Remove(item);
                CapNhatTongTien();
            }
        }

        private void numTienKhachDua_ValueChanged(object? sender, EventArgs e) => CapNhatTongTien();

        private void btnThanhToan_Click(object? sender, EventArgs e)
        {
            if (gioHang.Count == 0)
            {
                ShowWarning("Giỏ hàng đang trống.");
                return;
            }

            decimal tong = bll.TinhTongTien(gioHang);
            if (MessageBox.Show($"Xác nhận thanh toán {tong:N0} đ?", "Xác nhận thanh toán",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            if (bll.ThanhToan(MaKhachHangDangChon(), MaNhanVien, numTienKhachDua.Value, txtGhiChu.Text,
                    gioHang, out string message))
            {
                MessageBox.Show(message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetHoaDon();
                LoadSanPham(); // tải lại tồn kho mới
            }
            else
            {
                ShowError(message);
            }
        }

        private void btnHuy_Click(object? sender, EventArgs e)
        {
            if (gioHang.Count > 0 &&
                MessageBox.Show("Hủy hóa đơn đang lập?", "Xác nhận", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            ResetHoaDon();
        }

        private void ResetHoaDon()
        {
            gioHang.Clear();
            txtGhiChu.Clear();
            if (cboKhachHang.Items.Count > 0) cboKhachHang.SelectedIndex = 0;
            cboSanPham.SelectedIndex = -1;
            numSoLuong.Value = 1;
            numTienKhachDua.Value = 0;
            CapNhatTongTien();
        }

        private void CapNhatTongTien()
        {
            decimal tong = bll.TinhTongTien(gioHang);
            lblTongTien.Text = $"{tong:N0} đ";
            decimal thoi = numTienKhachDua.Value - tong;
            lblTienThoi.Text = thoi >= 0 ? $"{thoi:N0} đ" : $"Còn thiếu {-thoi:N0} đ";
            lblTienThoi.ForeColor = thoi >= 0 ? Color.ForestGreen : Color.Firebrick;
        }

        private static void ShowWarning(string message) =>
            MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        private static void ShowError(string message) =>
            MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
