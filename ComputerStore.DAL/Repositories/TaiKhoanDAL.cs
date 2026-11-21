using MySql.Data.MySqlClient;
using ComputerStore.DAL.Database;
using ComputerStore.DTO.Entities;

namespace ComputerStore.DAL.Repositories
{
    public class TaiKhoanDAL
    {
        private const string SelectColumns = @"
            SELECT
                MaTaiKhoan,
                MaNhanVien,
                TenDangNhap,
                MatKhau,
                VaiTro,
                TrangThai,
                NgayTao
            FROM TaiKhoan";

        // 1. TIM TAI KHOAN THEO TEN DANG NHAP
        public TaiKhoanDTO? GetByUsername(string tenDangNhap)
        {
            string query = SelectColumns + " WHERE TenDangNhap = @TenDangNhap LIMIT 1;";

            using (MySqlConnection connection = DbConnection.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapTaiKhoan(reader);
                        }
                    }
                }
            }

            return null;
        }

        // 2. TIM TAI KHOAN THEO MA NHAN VIEN
        public TaiKhoanDTO? GetByMaNhanVien(int maNhanVien)
        {
            string query = SelectColumns + " WHERE MaNhanVien = @MaNhanVien LIMIT 1;";

            using (MySqlConnection connection = DbConnection.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaNhanVien", maNhanVien);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapTaiKhoan(reader);
                        }
                    }
                }
            }

            return null;
        }

        // 3. KIEM TRA TRUNG TEN DANG NHAP
        public bool ExistsUsername(string tenDangNhap, int excludeMaTaiKhoan = 0)
        {
            string query = @"
                SELECT COUNT(*)
                FROM TaiKhoan
                WHERE TenDangNhap = @TenDangNhap
                  AND MaTaiKhoan <> @MaTaiKhoan;";

            using (MySqlConnection connection = DbConnection.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                    command.Parameters.AddWithValue("@MaTaiKhoan", excludeMaTaiKhoan);

                    return Convert.ToInt32(command.ExecuteScalar()) > 0;
                }
            }
        }

        // 4. THEM TAI KHOAN MOI
        public bool Insert(TaiKhoanDTO taiKhoan)
        {
            string query = @"
                INSERT INTO TaiKhoan (MaNhanVien, TenDangNhap, MatKhau, VaiTro, TrangThai)
                VALUES (@MaNhanVien, @TenDangNhap, @MatKhau, @VaiTro, @TrangThai);";

            using (MySqlConnection connection = DbConnection.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaNhanVien", taiKhoan.MaNhanVien);
                    command.Parameters.AddWithValue("@TenDangNhap", taiKhoan.TenDangNhap);
                    command.Parameters.AddWithValue("@MatKhau", taiKhoan.MatKhau);
                    command.Parameters.AddWithValue("@VaiTro", taiKhoan.VaiTro);
                    command.Parameters.AddWithValue("@TrangThai", taiKhoan.TrangThai);

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        // 5. CAP NHAT TAI KHOAN (CO HOAC KHONG DOI MAT KHAU)
        public bool Update(TaiKhoanDTO taiKhoan, bool updatePassword)
        {
            string query = updatePassword
                ? @"
                    UPDATE TaiKhoan
                    SET TenDangNhap = @TenDangNhap,
                        MatKhau = @MatKhau,
                        VaiTro = @VaiTro,
                        TrangThai = @TrangThai
                    WHERE MaTaiKhoan = @MaTaiKhoan;"
                : @"
                    UPDATE TaiKhoan
                    SET TenDangNhap = @TenDangNhap,
                        VaiTro = @VaiTro,
                        TrangThai = @TrangThai
                    WHERE MaTaiKhoan = @MaTaiKhoan;";

            using (MySqlConnection connection = DbConnection.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TenDangNhap", taiKhoan.TenDangNhap);
                    command.Parameters.AddWithValue("@VaiTro", taiKhoan.VaiTro);
                    command.Parameters.AddWithValue("@TrangThai", taiKhoan.TrangThai);
                    command.Parameters.AddWithValue("@MaTaiKhoan", taiKhoan.MaTaiKhoan);

                    if (updatePassword)
                    {
                        command.Parameters.AddWithValue("@MatKhau", taiKhoan.MatKhau);
                    }

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        // 6. CHUYEN DU LIEU MYSQL SANG DTO
        private static TaiKhoanDTO MapTaiKhoan(MySqlDataReader reader)
        {
            return new TaiKhoanDTO
            {
                MaTaiKhoan = reader.GetInt32("MaTaiKhoan"),
                MaNhanVien = reader.GetInt32("MaNhanVien"),
                TenDangNhap = reader.GetString("TenDangNhap"),
                MatKhau = reader.GetString("MatKhau"),
                VaiTro = reader.GetString("VaiTro"),
                TrangThai = reader.GetInt32("TrangThai"),
                NgayTao = reader.GetDateTime("NgayTao")
            };
        }
    }
}
