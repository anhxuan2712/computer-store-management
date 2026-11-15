
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using ComputerStore.DAL.Database;
using ComputerStore.DTO.Entities;

namespace ComputerStore.DAL.Repositories
{
    public class NhanVienDAL
    {
        // 1. LAY DANH SACH NHAN VIEN
        public List<NhanVienDTO> GetAll()
        {
            List<NhanVienDTO> danhSach = new List<NhanVienDTO>();

            string query = @"
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
                FROM NhanVien
                ORDER BY MaNhanVien;
            ";

            using (MySqlConnection connection = DbConnection.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        danhSach.Add(MapNhanVien(reader));
                    }
                }
            }

            return danhSach;
        }

        // 2. TIM NHAN VIEN THEO MA
        public NhanVienDTO GetById(int id)
        {
            string query = @"
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
                FROM NhanVien
                WHERE MaNhanVien = @MaNhanVien;
            ";

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

        // 3. THEM NHAN VIEN
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
                );
            ";

            using (MySqlConnection connection = DbConnection.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    AddNhanVienParameters(command, nhanVien);

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        // 4. CAP NHAT NHAN VIEN
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
                WHERE MaNhanVien = @MaNhanVien;
            ";

            using (MySqlConnection connection = DbConnection.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    AddNhanVienParameters(command, nhanVien);

                    command.Parameters.AddWithValue(
                        "@MaNhanVien",
                        nhanVien.MaNhanVien
                    );

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        // 5. XOA NHAN VIEN
        public bool Delete(int id)
        {
            string query = @"
                DELETE FROM NhanVien
                WHERE MaNhanVien = @MaNhanVien;
            ";

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

        // 6. HAM CHUYEN DU LIEU MYSQL SANG DTO
        private NhanVienDTO MapNhanVien(MySqlDataReader reader)
        {
            return new NhanVienDTO
            {
                MaNhanVien = reader.GetInt32("MaNhanVien"),
                HoTen = reader.GetString("HoTen"),

                GioiTinh = reader.IsDBNull(reader.GetOrdinal("GioiTinh"))
                    ? null
                    : reader.GetString("GioiTinh"),

                NgaySinh = reader.IsDBNull(reader.GetOrdinal("NgaySinh"))
                    ? (DateTime?)null
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
                    ? (DateTime?)null
                    : reader.GetDateTime("NgayVaoLam"),

                TrangThai = reader.GetInt32("TrangThai"),
                NgayTao = reader.GetDateTime("NgayTao")
            };
        }

        // 7. HAM THEM CAC THAM SO NHAN VIEN
        private void AddNhanVienParameters(
            MySqlCommand command,
            NhanVienDTO nhanVien)
        {
            command.Parameters.AddWithValue("@HoTen", nhanVien.HoTen);

            command.Parameters.AddWithValue(
                "@GioiTinh",
                (object)nhanVien.GioiTinh ?? DBNull.Value
            );

            command.Parameters.AddWithValue(
                "@NgaySinh",
                (object)nhanVien.NgaySinh ?? DBNull.Value
            );

            command.Parameters.AddWithValue(
                "@SoDienThoai",
                (object)nhanVien.SoDienThoai ?? DBNull.Value
            );

            command.Parameters.AddWithValue(
                "@Email",
                (object)nhanVien.Email ?? DBNull.Value
            );

            command.Parameters.AddWithValue(
                "@DiaChi",
                (object)nhanVien.DiaChi ?? DBNull.Value
            );

            command.Parameters.AddWithValue(
                "@NgayVaoLam",
                (object)nhanVien.NgayVaoLam ?? DBNull.Value
            );

            command.Parameters.AddWithValue(
                "@TrangThai",
                nhanVien.TrangThai
            );
        }
    }
}
