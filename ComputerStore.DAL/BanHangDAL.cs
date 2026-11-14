using System.Data;
using ComputerStore.DAL.Database;
using ComputerStore.DTO;
using MySql.Data.MySqlClient;

namespace ComputerStore.DAL
{
    public class BanHangDAL
    {
        /// <summary>Danh sách khách hàng để chọn trên hóa đơn.</summary>
        public DataTable GetKhachHang()
        {
            return Query(
                "SELECT MaKhachHang, CONCAT(TenKhachHang, ' - ', SoDienThoai) AS HienThi " +
                "FROM KhachHang ORDER BY TenKhachHang");
        }

        /// <summary>Danh sách sản phẩm (mã, tên, giá bán, tồn kho) để chọn khi bán.</summary>
        public DataTable GetSanPham()
        {
            return Query(
                "SELECT MaSanPham, TenSanPham, GiaBan, SoLuongTon, " +
                "CONCAT(MaSanPham, ' - ', TenSanPham) AS HienThi FROM SanPham ORDER BY TenSanPham");
        }

        public int GetSoLuongTon(string maSanPham)
        {
            using var conn = DbConnection.GetConnection();
            using var cmd = new MySqlCommand("SELECT SoLuongTon FROM SanPham WHERE MaSanPham = @ma", conn);
            cmd.Parameters.AddWithValue("@ma", maSanPham);
            conn.Open();
            object? result = cmd.ExecuteScalar();
            return result == null ? 0 : Convert.ToInt32(result);
        }

        public DataTable GetDanhSachHoaDon()
        {
            return Query(
                "SELECT hd.MaHoaDon, hd.NgayLap, IFNULL(kh.TenKhachHang, 'Khách lẻ') AS KhachHang, " +
                "nv.HoTen AS NhanVien, hd.TongTien, hd.TienKhachDua " +
                "FROM HoaDon hd " +
                "LEFT JOIN KhachHang kh ON kh.MaKhachHang = hd.MaKhachHang " +
                "JOIN NhanVien nv ON nv.MaNhanVien = hd.MaNhanVien " +
                "ORDER BY hd.NgayLap DESC");
        }

        /// <summary>
        /// Lưu hóa đơn, chi tiết hóa đơn và trừ tồn kho trong cùng một transaction.
        /// Nếu một sản phẩm không đủ tồn kho thì rollback toàn bộ. Trả về mã hóa đơn đã tạo.
        /// </summary>
        public string Insert(HoaDonDTO hoaDon)
        {
            using var conn = DbConnection.GetConnection();
            conn.Open();
            using var tran = conn.BeginTransaction();
            try
            {
                using (var cmdId = new MySqlCommand(
                    "SELECT MaHoaDon FROM HoaDon WHERE MaHoaDon REGEXP '^HD[0-9]+$' " +
                    "ORDER BY CAST(SUBSTRING(MaHoaDon, 3) AS UNSIGNED) DESC LIMIT 1 FOR UPDATE", conn, tran))
                {
                    hoaDon.MaHoaDon = IdGenerator.Next("HD", cmdId.ExecuteScalar() as string, 5);
                }

                using (var cmd = new MySqlCommand(
                    "INSERT INTO HoaDon (MaHoaDon, NgayLap, MaKhachHang, MaNhanVien, TongTien, TienKhachDua, GhiChu) " +
                    "VALUES (@ma, @ngay, @kh, @nv, @tong, @tienkhach, @ghichu)", conn, tran))
                {
                    cmd.Parameters.AddWithValue("@ma", hoaDon.MaHoaDon);
                    cmd.Parameters.AddWithValue("@ngay", hoaDon.NgayLap);
                    cmd.Parameters.AddWithValue("@kh", (object?)hoaDon.MaKhachHang ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@nv", hoaDon.MaNhanVien);
                    cmd.Parameters.AddWithValue("@tong", hoaDon.TongTien);
                    cmd.Parameters.AddWithValue("@tienkhach", hoaDon.TienKhachDua);
                    cmd.Parameters.AddWithValue("@ghichu", (object?)hoaDon.GhiChu ?? DBNull.Value);
                    cmd.ExecuteNonQuery();
                }

                foreach (var ct in hoaDon.ChiTiet)
                {
                    ct.MaHoaDon = hoaDon.MaHoaDon;

                    // Trừ tồn kho có điều kiện: chỉ trừ khi còn đủ hàng (tránh bán vượt tồn
                    // kể cả khi hai máy bán cùng lúc).
                    using (var cmd = new MySqlCommand(
                        "UPDATE SanPham SET SoLuongTon = SoLuongTon - @sl " +
                        "WHERE MaSanPham = @sp AND SoLuongTon >= @sl", conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@sl", ct.SoLuong);
                        cmd.Parameters.AddWithValue("@sp", ct.MaSanPham);
                        if (cmd.ExecuteNonQuery() == 0)
                        {
                            throw new InvalidOperationException(
                                $"Sản phẩm {ct.MaSanPham} - {ct.TenSanPham} không đủ tồn kho.");
                        }
                    }

                    using (var cmd = new MySqlCommand(
                        "INSERT INTO ChiTietHoaDon (MaHoaDon, MaSanPham, SoLuong, DonGiaBan, ThanhTien) " +
                        "VALUES (@ma, @sp, @sl, @gia, @tt)", conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@ma", ct.MaHoaDon);
                        cmd.Parameters.AddWithValue("@sp", ct.MaSanPham);
                        cmd.Parameters.AddWithValue("@sl", ct.SoLuong);
                        cmd.Parameters.AddWithValue("@gia", ct.DonGiaBan);
                        cmd.Parameters.AddWithValue("@tt", ct.ThanhTien);
                        cmd.ExecuteNonQuery();
                    }
                }

                tran.Commit();
                return hoaDon.MaHoaDon;
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
