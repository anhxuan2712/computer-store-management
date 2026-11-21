using ComputerStore.DAL.Database;
using ComputerStore.DTO.Entities;
using MySql.Data.MySqlClient;

namespace ComputerStore.DAL.Repositories
{
    public class NhaCungCapDAL
    {
        public List<NhaCungCapDTO> GetAll() => Search(string.Empty);

        public List<NhaCungCapDTO> Search(string keyword)
        {
            const string query = @"
                SELECT MaNhaCungCap, TenNhaCungCap, SoDienThoai, Email, DiaChi, TrangThai
                FROM NhaCungCap
                WHERE @Keyword = ''
                   OR TenNhaCungCap LIKE @Pattern
                   OR CAST(MaNhaCungCap AS CHAR) LIKE @Pattern
                   OR SoDienThoai LIKE @Pattern
                   OR Email LIKE @Pattern
                ORDER BY MaNhaCungCap;";
            var result = new List<NhaCungCapDTO>();
            using var connection = DbConnection.GetConnection();
            connection.Open();
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Keyword", keyword.Trim());
            command.Parameters.AddWithValue("@Pattern", $"%{keyword.Trim()}%");
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new NhaCungCapDTO
                {
                    MaNhaCungCap = reader.GetInt32("MaNhaCungCap"),
                    TenNhaCungCap = reader.GetString("TenNhaCungCap"),
                    SoDienThoai = reader.IsDBNull(reader.GetOrdinal("SoDienThoai")) ? null : reader.GetString("SoDienThoai"),
                    Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString("Email"),
                    DiaChi = reader.IsDBNull(reader.GetOrdinal("DiaChi")) ? null : reader.GetString("DiaChi"),
                    TrangThai = reader.GetInt32("TrangThai")
                });
            }
            return result;
        }

        public bool Insert(NhaCungCapDTO item)
        {
            const string query = @"
                INSERT INTO NhaCungCap (TenNhaCungCap, SoDienThoai, Email, DiaChi, TrangThai)
                VALUES (@TenNhaCungCap, @SoDienThoai, @Email, @DiaChi, @TrangThai);";
            using var connection = DbConnection.GetConnection();
            connection.Open();
            using var command = new MySqlCommand(query, connection);
            AddParameters(command, item);
            return command.ExecuteNonQuery() == 1;
        }

        public bool Update(NhaCungCapDTO item)
        {
            const string query = @"
                UPDATE NhaCungCap
                SET TenNhaCungCap = @TenNhaCungCap,
                    SoDienThoai = @SoDienThoai,
                    Email = @Email,
                    DiaChi = @DiaChi,
                    TrangThai = @TrangThai
                WHERE MaNhaCungCap = @MaNhaCungCap;";
            using var connection = DbConnection.GetConnection();
            connection.Open();
            using var command = new MySqlCommand(query, connection);
            AddParameters(command, item);
            command.Parameters.AddWithValue("@MaNhaCungCap", item.MaNhaCungCap);
            return command.ExecuteNonQuery() == 1;
        }

        public bool Delete(int id)
        {
            const string query = "DELETE FROM NhaCungCap WHERE MaNhaCungCap = @MaNhaCungCap;";
            using var connection = DbConnection.GetConnection();
            connection.Open();
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaNhaCungCap", id);
            return command.ExecuteNonQuery() == 1;
        }

        public bool HasPhieuNhap(int maNhaCungCap)
        {
            const string query = "SELECT COUNT(*) FROM PhieuNhap WHERE MaNhaCungCap = @MaNhaCungCap;";
            using var connection = DbConnection.GetConnection();
            connection.Open();
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaNhaCungCap", maNhaCungCap);
            return Convert.ToInt32(command.ExecuteScalar()) > 0;
        }

        private static void AddParameters(MySqlCommand command, NhaCungCapDTO item)
        {
            command.Parameters.AddWithValue("@TenNhaCungCap", item.TenNhaCungCap);
            command.Parameters.AddWithValue("@SoDienThoai", (object?)item.SoDienThoai ?? DBNull.Value);
            command.Parameters.AddWithValue("@Email", (object?)item.Email ?? DBNull.Value);
            command.Parameters.AddWithValue("@DiaChi", (object?)item.DiaChi ?? DBNull.Value);
            command.Parameters.AddWithValue("@TrangThai", item.TrangThai);
        }
    }
}
