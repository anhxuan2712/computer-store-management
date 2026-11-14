using System.Data;
using ComputerStore.DAL.Database;
using ComputerStore.DTO;
using MySql.Data.MySqlClient;

namespace ComputerStore.DAL
{
    public class NhapHangDAL
    {
        /// <summary>Danh sách nhà cung cấp để chọn trên phiếu nhập.</summary>
        public DataTable GetNhaCungCap()
        {
            return Query("SELECT MaNhaCungCap, TenNhaCungCap FROM NhaCungCap ORDER BY TenNhaCungCap");
        }

        /// <summary>Danh sách sản phẩm (mã, tên, giá nhập, tồn kho) để chọn khi nhập hàng.</summary>
        public DataTable GetSanPham()
        {
            return Query(
                "SELECT MaSanPham, TenSanPham, GiaNhap, SoLuongTon, " +
                "CONCAT(MaSanPham, ' - ', TenSanPham) AS HienThi FROM SanPham ORDER BY TenSanPham");
        }

        public DataTable GetDanhSachPhieuNhap()
        {
            return Query(
                "SELECT pn.MaPhieuNhap, pn.NgayNhap, ncc.TenNhaCungCap, nv.HoTen AS NhanVien, pn.TongTien, pn.GhiChu " +
                "FROM PhieuNhap pn " +
                "JOIN NhaCungCap ncc ON ncc.MaNhaCungCap = pn.MaNhaCungCap " +
                "JOIN NhanVien nv ON nv.MaNhanVien = pn.MaNhanVien " +
                "ORDER BY pn.NgayNhap DESC");
        }

        public DataTable GetChiTietPhieuNhap(string maPhieuNhap)
        {
            return Query(
                "SELECT ct.MaSanPham, sp.TenSanPham, ct.SoLuong, ct.DonGiaNhap, ct.ThanhTien " +
                "FROM ChiTietPhieuNhap ct JOIN SanPham sp ON sp.MaSanPham = ct.MaSanPham " +
                "WHERE ct.MaPhieuNhap = @ma",
                new MySqlParameter("@ma", maPhieuNhap));
        }

        /// <summary>
        /// Lưu phiếu nhập, chi tiết phiếu và cộng tồn kho trong cùng một transaction.
        /// Có lỗi ở bất kỳ bước nào sẽ rollback toàn bộ. Trả về mã phiếu nhập đã tạo.
        /// </summary>
        public string Insert(PhieuNhapDTO phieu)
        {
            using var conn = DbConnection.GetConnection();
            conn.Open();
            using var tran = conn.BeginTransaction();
            try
            {
                using (var cmdId = new MySqlCommand(
                    "SELECT MaPhieuNhap FROM PhieuNhap WHERE MaPhieuNhap REGEXP '^PN[0-9]+$' " +
                    "ORDER BY CAST(SUBSTRING(MaPhieuNhap, 3) AS UNSIGNED) DESC LIMIT 1 FOR UPDATE", conn, tran))
                {
                    phieu.MaPhieuNhap = IdGenerator.Next("PN", cmdId.ExecuteScalar() as string, 5);
                }

                using (var cmd = new MySqlCommand(
                    "INSERT INTO PhieuNhap (MaPhieuNhap, NgayNhap, MaNhaCungCap, MaNhanVien, TongTien, GhiChu) " +
                    "VALUES (@ma, @ngay, @ncc, @nv, @tong, @ghichu)", conn, tran))
                {
                    cmd.Parameters.AddWithValue("@ma", phieu.MaPhieuNhap);
                    cmd.Parameters.AddWithValue("@ngay", phieu.NgayNhap);
                    cmd.Parameters.AddWithValue("@ncc", phieu.MaNhaCungCap);
                    cmd.Parameters.AddWithValue("@nv", phieu.MaNhanVien);
                    cmd.Parameters.AddWithValue("@tong", phieu.TongTien);
                    cmd.Parameters.AddWithValue("@ghichu", (object?)phieu.GhiChu ?? DBNull.Value);
                    cmd.ExecuteNonQuery();
                }

                foreach (var ct in phieu.ChiTiet)
                {
                    ct.MaPhieuNhap = phieu.MaPhieuNhap;

                    using (var cmd = new MySqlCommand(
                        "INSERT INTO ChiTietPhieuNhap (MaPhieuNhap, MaSanPham, SoLuong, DonGiaNhap, ThanhTien) " +
                        "VALUES (@ma, @sp, @sl, @gia, @tt)", conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@ma", ct.MaPhieuNhap);
                        cmd.Parameters.AddWithValue("@sp", ct.MaSanPham);
                        cmd.Parameters.AddWithValue("@sl", ct.SoLuong);
                        cmd.Parameters.AddWithValue("@gia", ct.DonGiaNhap);
                        cmd.Parameters.AddWithValue("@tt", ct.ThanhTien);
                        cmd.ExecuteNonQuery();
                    }

                    // Tăng tồn kho và cập nhật giá nhập mới nhất của sản phẩm.
                    using (var cmd = new MySqlCommand(
                        "UPDATE SanPham SET SoLuongTon = SoLuongTon + @sl, GiaNhap = @gia WHERE MaSanPham = @sp",
                        conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@sl", ct.SoLuong);
                        cmd.Parameters.AddWithValue("@gia", ct.DonGiaNhap);
                        cmd.Parameters.AddWithValue("@sp", ct.MaSanPham);
                        if (cmd.ExecuteNonQuery() == 0)
                        {
                            throw new InvalidOperationException($"Không tìm thấy sản phẩm {ct.MaSanPham}.");
                        }
                    }
                }

                tran.Commit();
                return phieu.MaPhieuNhap;
            }
            catch
            {
                tran.Rollback();
                throw;
            }
        }

        private static DataTable Query(string sql, params MySqlParameter[] parameters)
        {
            using var conn = DbConnection.GetConnection();
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddRange(parameters);
            using var adapter = new MySqlDataAdapter(cmd);
            var table = new DataTable();
            adapter.Fill(table);
            return table;
        }
    }
}
