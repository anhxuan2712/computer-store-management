using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using ComputerStore.DAL.Database;
using ComputerStore.DTO.Entities;

namespace ComputerStore.DAL.Repositories
{
    public class KhachHangDAL
    {
        private const string SelectColumns = @"
            SELECT
                MaKhachHang,
                HoTen,
                SoDienThoai,
                Email,
                DiaChi,
                NgayTao
            FROM KhachHang";

        // 1. LAY DANH SACH KHACH HANG
        public List<KhachHangDTO> GetAll()
        {
            string query = SelectColumns + " ORDER BY MaKhachHang;";

            using (MySqlConnection connection = DbConnection.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    return ReadList(command);
                }
            }
        }

        // 2. TIM KIEM THEO MA, HO TEN HOAC SO DIEN THOAI
        public List<KhachHangDTO> Search(string keyword)
        {
            string query = SelectColumns + @"
                WHERE CAST(MaKhachHang AS CHAR) = @MaKhachHang
                   OR HoTen LIKE @TuKhoa
                   OR SoDienThoai LIKE @TuKhoa
                ORDER BY MaKhachHang;";

            using (MySqlConnection connection = DbConnection.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaKhachHang", keyword);
                    command.Parameters.AddWithValue("@TuKhoa", "%" + keyword + "%");

                    return ReadList(command);
                }
            }
        }

        // 3. THEM KHACH HANG, TRA VE MA VUA TAO
        public int Insert(KhachHangDTO khachHang)
        {
            string query = @"
                INSERT INTO KhachHang (HoTen, SoDienThoai, Email, DiaChi)
                VALUES (@HoTen, @SoDienThoai, @Email, @DiaChi);";

            using (MySqlConnection connection = DbConnection.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    AddKhachHangParameters(command, khachHang);
                    command.ExecuteNonQuery();

                    return (int)command.LastInsertedId;
                }
            }
        }

        // 4. CAP NHAT KHACH HANG
        public bool Update(KhachHangDTO khachHang)
        {
            string query = @"
                UPDATE KhachHang
                SET
                    HoTen = @HoTen,
                    SoDienThoai = @SoDienThoai,
                    Email = @Email,
                    DiaChi = @DiaChi
                WHERE MaKhachHang = @MaKhachHang;";

            using (MySqlConnection connection = DbConnection.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    AddKhachHangParameters(command, khachHang);
                    command.Parameters.AddWithValue("@MaKhachHang", khachHang.MaKhachHang);

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        // 5. XOA KHACH HANG
        public bool Delete(int maKhachHang)
        {
            string query = "DELETE FROM KhachHang WHERE MaKhachHang = @MaKhachHang;";

            using (MySqlConnection connection = DbConnection.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaKhachHang", maKhachHang);

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        // 6. KIEM TRA SO DIEN THOAI DA DUOC KHACH HANG KHAC DUNG CHUA
        public bool ExistsPhone(string soDienThoai, int maKhachHangBoQua)
        {
            string query = @"
                SELECT COUNT(*) FROM KhachHang
                WHERE SoDienThoai = @SoDienThoai
                  AND MaKhachHang <> @MaKhachHang;";

            using (MySqlConnection connection = DbConnection.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SoDienThoai", soDienThoai);
                    command.Parameters.AddWithValue("@MaKhachHang", maKhachHangBoQua);

                    return Convert.ToInt32(command.ExecuteScalar()) > 0;
                }
            }
        }

        // 7. KIEM TRA KHACH HANG DA CO HOA DON CHUA
        public bool HasHoaDon(int maKhachHang)
        {
            string query = "SELECT COUNT(*) FROM HoaDon WHERE MaKhachHang = @MaKhachHang;";

            using (MySqlConnection connection = DbConnection.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaKhachHang", maKhachHang);

                    return Convert.ToInt32(command.ExecuteScalar()) > 0;
                }
            }
        }

        // 8. HAM THEM CAC THAM SO KHACH HANG
        private void AddKhachHangParameters(MySqlCommand command, KhachHangDTO khachHang)
        {
            command.Parameters.AddWithValue("@HoTen", khachHang.HoTen);
            command.Parameters.AddWithValue("@SoDienThoai", (object?)khachHang.SoDienThoai ?? DBNull.Value);
            command.Parameters.AddWithValue("@Email", (object?)khachHang.Email ?? DBNull.Value);
            command.Parameters.AddWithValue("@DiaChi", (object?)khachHang.DiaChi ?? DBNull.Value);
        }

        // 9. HAM CHUYEN DU LIEU MYSQL SANG DTO
        private List<KhachHangDTO> ReadList(MySqlCommand command)
        {
            List<KhachHangDTO> danhSach = new List<KhachHangDTO>();

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    danhSach.Add(new KhachHangDTO
                    {
                        MaKhachHang = reader.GetInt32("MaKhachHang"),
                        HoTen = reader.GetString("HoTen"),
                        SoDienThoai = reader.IsDBNull(reader.GetOrdinal("SoDienThoai"))
                            ? null! : reader.GetString("SoDienThoai"),
                        Email = reader.IsDBNull(reader.GetOrdinal("Email"))
                            ? null! : reader.GetString("Email"),
                        DiaChi = reader.IsDBNull(reader.GetOrdinal("DiaChi"))
                            ? null! : reader.GetString("DiaChi"),
                        NgayTao = reader.GetDateTime("NgayTao")
                    });
                }
            }

            return danhSach;
        }
    }
}
