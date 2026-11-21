using System.Data;
using ComputerStore.DAL.Database;
using ComputerStore.DTO.Entities;
using MySql.Data.MySqlClient;

namespace ComputerStore.DAL.Repositories
{
    public class DanhMucDAL
    {
        public List<DanhMucDTO> GetAll() => Search(string.Empty);

        public List<DanhMucDTO> Search(string keyword)
        {
            const string query = @"
                SELECT MaDanhMuc, TenDanhMuc, MoTa
                FROM DanhMuc
                WHERE @Keyword = ''
                   OR MaDanhMuc LIKE @Pattern
                   OR TenDanhMuc LIKE @Pattern
                ORDER BY MaDanhMuc;";
            var result = new List<DanhMucDTO>();
            using var connection = DbConnection.GetConnection();
            connection.Open();
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Keyword", keyword.Trim());
            command.Parameters.AddWithValue("@Pattern", $"%{keyword.Trim()}%");
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new DanhMucDTO
                {
                    MaDanhMuc = reader.GetString("MaDanhMuc"),
                    TenDanhMuc = reader.GetString("TenDanhMuc"),
                    MoTa = reader.IsDBNull("MoTa") ? null : reader.GetString("MoTa")
                });
            }
            return result;
        }

        public bool Insert(DanhMucDTO item)
        {
            const string query = @"
                INSERT INTO DanhMuc (MaDanhMuc, TenDanhMuc, MoTa)
                VALUES (@MaDanhMuc, @TenDanhMuc, @MoTa);";
            using var connection = DbConnection.GetConnection();
            connection.Open();
            using var command = new MySqlCommand(query, connection);
            AddParameters(command, item);
            return command.ExecuteNonQuery() == 1;
        }

        public bool Update(DanhMucDTO item)
        {
            const string query = @"
                UPDATE DanhMuc
                SET TenDanhMuc = @TenDanhMuc, MoTa = @MoTa
                WHERE MaDanhMuc = @MaDanhMuc;";
            using var connection = DbConnection.GetConnection();
            connection.Open();
            using var command = new MySqlCommand(query, connection);
            AddParameters(command, item);
            return command.ExecuteNonQuery() == 1;
        }

        public bool Delete(string id)
        {
            const string query = "DELETE FROM DanhMuc WHERE MaDanhMuc = @MaDanhMuc;";
            using var connection = DbConnection.GetConnection();
            connection.Open();
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaDanhMuc", id);
            return command.ExecuteNonQuery() == 1;
        }

        private static void AddParameters(MySqlCommand command, DanhMucDTO item)
        {
            command.Parameters.AddWithValue("@MaDanhMuc", item.MaDanhMuc);
            command.Parameters.AddWithValue("@TenDanhMuc", item.TenDanhMuc);
            command.Parameters.Add("@MoTa", MySqlDbType.VarChar).Value = (object?)item.MoTa ?? DBNull.Value;
        }
    }
}
