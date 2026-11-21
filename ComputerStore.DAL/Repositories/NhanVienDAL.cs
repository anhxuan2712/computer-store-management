using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using ComputerStore.DAL.Database;
using ComputerStore.DTO.Entities;

namespace ComputerStore.DAL.Repositories
{
    public class NhanVienDAL
    {
        private const string SelectColumns = @"
            SELECT
                MaNhanVien,
                HoTen,
                GioiTinh,
                NgaySinh,
                SoDienThoai,
                Email,
                DiaChi,
                NgayVaoLam,
                TrangThai,
                NgayTao
            FROM NhanVien";

        // 1. LAY DANH SACH NHAN VIEN
        public List<NhanVienDTO> GetAll()
        {
            string query = SelectColumns + " ORDER BY MaNhanVien;";
            return ReadList(query);
        }

        // 2. TIM KIEM NHAN VIEN THEO MA, HO TEN, SDT HOAC EMAIL
        public List<NhanVienDTO> Search(string keyword)
        {
            string query = SelectColumns + @"
                WHERE CAST(MaNhanVien AS CHAR) = @MaNhanVien
                   OR HoTen LIKE @TuKhoa
                   OR SoDienThoai LIKE @TuKhoa
                   OR Email LIKE @TuKhoa
                ORDER BY MaNhanVien;";

            return ReadList(
                query,
                new MySqlParameter("@MaNhanVien", keyword),
                new MySqlParameter("@TuKhoa", "%" + keyword + "%")
            );
        }

        // 3. TIM NHAN VIEN THEO MA
        public NhanVienDTO? GetById(int id)
        {
            string query = SelectColumns + " WHERE MaNhanVien = @MaNhanVien;";

            using (MySqlConnection connection = DbConnection.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaNhanVien", id);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapNhanVien(reader);
                        }
                    }
                }
            }

            return null;
        }

        // 4. THEM NHAN VIEN
        public bool Insert(NhanVienDTO nhanVien)
        {
            string query = @"
                INSERT INTO NhanVien
                (
                    HoTen,
                    GioiTinh,
                    NgaySinh,
                    SoDienThoai,
                    Email,
                    DiaChi,
                    NgayVaoLam,
                    TrangThai
                )
                VALUES
                (
                    @HoTen,
                    @GioiTinh,
                    @NgaySinh,
                    @SoDienThoai,
                    @Email,
                    @DiaChi,
                    @NgayVaoLam,
                    @TrangThai
                );";

            using (MySqlConnection connection = DbConnection.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    AddNhanVienParameters(command, nhanVien);
                    bool ok = command.ExecuteNonQuery() > 0;
                    if (ok)
                    {
                        nhanVien.MaNhanVien = (int)command.LastInsertedId;
                    }
                    return ok;
                }
            }
        }

        // 5. CAP NHAT NHAN VIEN
        public bool Update(NhanVienDTO nhanVien)
        {
            string query = @"
                UPDATE NhanVien
                SET
                    HoTen = @HoTen,
                    GioiTinh = @GioiTinh,
                    NgaySinh = @NgaySinh,
                    SoDienThoai = @SoDienThoai,
                    Email = @Email,
                    DiaChi = @DiaChi,
                    NgayVaoLam = @NgayVaoLam,
                    TrangThai = @TrangThai
                WHERE MaNhanVien = @MaNhanVien;";

            using (MySqlConnection connection = DbConnection.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    AddNhanVienParameters(command, nhanVien);
                    command.Parameters.AddWithValue("@MaNhanVien", nhanVien.MaNhanVien);

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        // 6. XOA NHAN VIEN
        public bool Delete(int id)
        {
            string query = "DELETE FROM NhanVien WHERE MaNhanVien = @MaNhanVien;";

            using (MySqlConnection connection = DbConnection.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaNhanVien", id);

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        // 7. KIEM TRA TRUNG SO DIEN THOAI
        public bool ExistsPhone(string soDienThoai, int excludeMaNhanVien = 0)
        {
            string query = @"
                SELECT COUNT(*) FROM NhanVien
                WHERE SoDienThoai = @SoDienThoai
                  AND MaNhanVien <> @MaNhanVien;";

            using (MySqlConnection connection = DbConnection.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SoDienThoai", soDienThoai);
                    command.Parameters.AddWithValue("@MaNhanVien", excludeMaNhanVien);

                    return Convert.ToInt32(command.ExecuteScalar()) > 0;
                }
            }
        }

        // 8. KIEM TRA TRUNG EMAIL
        public bool ExistsEmail(string email, int excludeMaNhanVien = 0)
        {
            string query = @"
                SELECT COUNT(*) FROM NhanVien
                WHERE Email = @Email
                  AND MaNhanVien <> @MaNhanVien;";

            using (MySqlConnection connection = DbConnection.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@MaNhanVien", excludeMaNhanVien);

                    return Convert.ToInt32(command.ExecuteScalar()) > 0;
                }
            }
        }

        // 9. KIEM TRA NHAN VIEN DA CO DU LIEU LIEN QUAN CHUA (TAI KHOAN, PHIEU NHAP, HOA DON)
        public bool HasRelatedRecords(int maNhanVien)
        {
            string query = @"
                SELECT
                    (SELECT COUNT(*) FROM TaiKhoan WHERE MaNhanVien = @MaNhanVien)
                  + (SELECT COUNT(*) FROM PhieuNhap WHERE MaNhanVien = @MaNhanVien)
                  + (SELECT COUNT(*) FROM HoaDon WHERE MaNhanVien = @MaNhanVien);";

            using (MySqlConnection connection = DbConnection.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaNhanVien", maNhanVien);

                    return Convert.ToInt32(command.ExecuteScalar()) > 0;
                }
            }
        }

        // 10. HAM DOC DANH SACH NHAN VIEN
        private List<NhanVienDTO> ReadList(string query, params MySqlParameter[] parameters)
        {
            List<NhanVienDTO> danhSach = new List<NhanVienDTO>();

            using (MySqlConnection connection = DbConnection.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    if (parameters.Length > 0)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            danhSach.Add(MapNhanVien(reader));
                        }
                    }
                }
            }

            return danhSach;
        }

        // 11. HAM CHUYEN DU LIEU MYSQL SANG DTO
        private static NhanVienDTO MapNhanVien(MySqlDataReader reader)
        {
            return new NhanVienDTO
            {
                MaNhanVien = reader.GetInt32("MaNhanVien"),
                HoTen = reader.GetString("HoTen"),
                GioiTinh = reader.IsDBNull(reader.GetOrdinal("GioiTinh"))
                    ? null
                    : reader.GetString("GioiTinh"),
                NgaySinh = reader.IsDBNull(reader.GetOrdinal("NgaySinh"))
                    ? null
                    : reader.GetDateTime("NgaySinh"),
                SoDienThoai = reader.IsDBNull(reader.GetOrdinal("SoDienThoai"))
                    ? null
                    : reader.GetString("SoDienThoai"),
                Email = reader.IsDBNull(reader.GetOrdinal("Email"))
                    ? null
                    : reader.GetString("Email"),
                DiaChi = reader.IsDBNull(reader.GetOrdinal("DiaChi"))
                    ? null
                    : reader.GetString("DiaChi"),
                NgayVaoLam = reader.IsDBNull(reader.GetOrdinal("NgayVaoLam"))
                    ? null
                    : reader.GetDateTime("NgayVaoLam"),
                TrangThai = reader.GetInt32("TrangThai"),
                NgayTao = reader.GetDateTime("NgayTao")
            };
        }

        // 12. HAM THEM CAC THAM SO NHAN VIEN
        private static void AddNhanVienParameters(MySqlCommand command, NhanVienDTO nhanVien)
        {
            command.Parameters.AddWithValue("@HoTen", nhanVien.HoTen);
            command.Parameters.AddWithValue("@GioiTinh", (object?)nhanVien.GioiTinh ?? DBNull.Value);
            command.Parameters.AddWithValue("@NgaySinh", (object?)nhanVien.NgaySinh ?? DBNull.Value);
            command.Parameters.AddWithValue("@SoDienThoai", (object?)nhanVien.SoDienThoai ?? DBNull.Value);
            command.Parameters.AddWithValue("@Email", (object?)nhanVien.Email ?? DBNull.Value);
            command.Parameters.AddWithValue("@DiaChi", (object?)nhanVien.DiaChi ?? DBNull.Value);
            command.Parameters.AddWithValue("@NgayVaoLam", (object?)nhanVien.NgayVaoLam ?? DBNull.Value);
            command.Parameters.AddWithValue("@TrangThai", nhanVien.TrangThai);
        }
    }
}
