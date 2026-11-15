using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using ComputerStore.DAL.Database;
using ComputerStore.DTO.Entities;

namespace ComputerStore.DAL.Repositories
{
    public class BanHangDAL
    {
        // 1. DANH SACH KHACH HANG DE CHON TREN HOA DON
        public DataTable GetKhachHang()
        {
            return Query(@"
                SELECT
                    MaKhachHang,
                    CONCAT(HoTen, IFNULL(CONCAT(' - ', SoDienThoai), '')) AS HienThi
                FROM KhachHang
                ORDER BY HoTen;");
        }

        // 2. DANH SACH SAN PHAM DANG KINH DOANH (MA, TEN, GIA BAN, TON KHO)
        public DataTable GetSanPham()
        {
            return Query(@"
                SELECT
                    MaSanPham,
                    TenSanPham,
                    GiaBan,
                    SoLuongTon,
                    CONCAT(MaSanPham, ' - ', TenSanPham) AS HienThi
                FROM SanPham
                WHERE TrangThai = 1
                ORDER BY TenSanPham;");
        }

        // 3. LAY SO LUONG TON HIEN TAI CUA SAN PHAM
        public int GetSoLuongTon(int maSanPham)
        {
            string query = "SELECT SoLuongTon FROM SanPham WHERE MaSanPham = @MaSanPham;";

            using (MySqlConnection connection = DbConnection.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaSanPham", maSanPham);

                    object? result = command.ExecuteScalar();
                    return result == null ? 0 : Convert.ToInt32(result);
                }
            }
        }

        // 4. DANH SACH HOA DON
        public DataTable GetDanhSachHoaDon()
        {
            return Query(@"
                SELECT
                    hd.MaHoaDon,
                    hd.NgayLap,
                    IFNULL(kh.HoTen, 'Khách lẻ') AS KhachHang,
                    nv.HoTen AS NhanVien,
                    hd.TongTien,
                    hd.TrangThai
                FROM HoaDon hd
                LEFT JOIN KhachHang kh ON kh.MaKhachHang = hd.MaKhachHang
                JOIN NhanVien nv ON nv.MaNhanVien = hd.MaNhanVien
                ORDER BY hd.NgayLap DESC;");
        }

        // 5. LUU HOA DON DA THANH TOAN
        // Luu hoa don, chi tiet va tru ton kho trong cung mot transaction.
        // Neu mot san pham khong du ton kho thi rollback toan bo. Tra ve ma hoa don vua tao.
        public int Insert(HoaDonDTO hoaDon, List<ChiTietHoaDonDTO> chiTiet)
        {
            using (MySqlConnection connection = DbConnection.GetConnection())
            {
                connection.Open();

                using (MySqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string insertHoaDon = @"
                            INSERT INTO HoaDon
                                (MaKhachHang, MaNhanVien, NgayLap, TongTien, TrangThai, GhiChu)
                            VALUES
                                (@MaKhachHang, @MaNhanVien, @NgayLap, @TongTien, @TrangThai, @GhiChu);";

                        using (MySqlCommand command = new MySqlCommand(insertHoaDon, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@MaKhachHang", (object?)hoaDon.MaKhachHang ?? DBNull.Value);
                            command.Parameters.AddWithValue("@MaNhanVien", hoaDon.MaNhanVien);
                            command.Parameters.AddWithValue("@NgayLap", hoaDon.NgayLap);
                            command.Parameters.AddWithValue("@TongTien", hoaDon.TongTien);
                            command.Parameters.AddWithValue("@TrangThai", hoaDon.TrangThai);
                            command.Parameters.AddWithValue("@GhiChu", (object?)hoaDon.GhiChu ?? DBNull.Value);
                            command.ExecuteNonQuery();

                            hoaDon.MaHoaDon = (int)command.LastInsertedId;
                        }

                        foreach (ChiTietHoaDonDTO ct in chiTiet)
                        {
                            ct.MaHoaDon = hoaDon.MaHoaDon;

                            // Tru ton kho co dieu kien: chi tru khi con du hang
                            // (tranh ban vuot ton ke ca khi hai may ban cung luc).
                            string updateTonKho = @"
                                UPDATE SanPham
                                SET SoLuongTon = SoLuongTon - @SoLuong
                                WHERE MaSanPham = @MaSanPham
                                  AND SoLuongTon >= @SoLuong;";

                            using (MySqlCommand command = new MySqlCommand(updateTonKho, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@SoLuong", ct.SoLuong);
                                command.Parameters.AddWithValue("@MaSanPham", ct.MaSanPham);

                                if (command.ExecuteNonQuery() == 0)
                                {
                                    throw new InvalidOperationException(
                                        $"Sản phẩm có mã {ct.MaSanPham} không đủ tồn kho.");
                                }
                            }

                            // ThanhTien la generated column nen khong insert.
                            string insertChiTiet = @"
                                INSERT INTO ChiTietHoaDon (MaHoaDon, MaSanPham, SoLuong, DonGiaBan)
                                VALUES (@MaHoaDon, @MaSanPham, @SoLuong, @DonGiaBan);";

                            using (MySqlCommand command = new MySqlCommand(insertChiTiet, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@MaHoaDon", ct.MaHoaDon);
                                command.Parameters.AddWithValue("@MaSanPham", ct.MaSanPham);
                                command.Parameters.AddWithValue("@SoLuong", ct.SoLuong);
                                command.Parameters.AddWithValue("@DonGiaBan", ct.DonGiaBan);
                                command.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                        return hoaDon.MaHoaDon;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        // 6. HAM CHAY CAU SELECT TRA VE DATATABLE
        private DataTable Query(string query, params MySqlParameter[] parameters)
        {
            using (MySqlConnection connection = DbConnection.GetConnection())
            using (MySqlCommand command = new MySqlCommand(query, connection))
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
            {
                command.Parameters.AddRange(parameters);

                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }
    }
}
