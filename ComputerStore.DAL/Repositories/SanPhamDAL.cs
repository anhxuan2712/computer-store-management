using ComputerStore.DAL.Database;
using ComputerStore.DTO.Entities;
using MySql.Data.MySqlClient;

namespace ComputerStore.DAL.Repositories
{
    public class SanPhamDAL
    {
        private const string SelectColumns = @"
            SELECT sp.MaSanPham, sp.TenSanPham, sp.MaDanhMuc, dm.TenDanhMuc,
                   sp.DonViTinh, sp.GiaNhap, sp.GiaBan, sp.SoLuongTon,
                   sp.MoTa, sp.TrangThai, sp.NgayTao
            FROM SanPham sp
            INNER JOIN DanhMuc dm ON dm.MaDanhMuc = sp.MaDanhMuc";

        public List<SanPhamDTO> GetAll() => Search(string.Empty);

        public List<SanPhamDTO> Search(string keyword)
        {
            const string query = SelectColumns + @"
                WHERE @Keyword = ''
                   OR sp.TenSanPham LIKE @Pattern
                   OR sp.MaSanPham = @ProductId
                   OR dm.TenDanhMuc LIKE @Pattern
                   OR dm.MaDanhMuc LIKE @Pattern
                ORDER BY sp.MaSanPham;";
            var result = new List<SanPhamDTO>();
            using var connection = DbConnection.GetConnection();
            connection.Open();
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Keyword", keyword.Trim());
            command.Parameters.AddWithValue("@Pattern", $"%{keyword.Trim()}%");
            command.Parameters.AddWithValue("@ProductId", int.TryParse(keyword.Trim(), out int id) ? id : -1);
            using var reader = command.ExecuteReader();
            while (reader.Read()) result.Add(Map(reader));
            return result;
        }

        public bool Insert(SanPhamDTO item)
        {
            const string query = @"
                INSERT INTO SanPham
                    (TenSanPham, MaDanhMuc, DonViTinh, GiaNhap, GiaBan, MoTa, TrangThai)
                VALUES
                    (@TenSanPham, @MaDanhMuc, @DonViTinh, @GiaNhap, @GiaBan, @MoTa, @TrangThai);";
            using var connection = DbConnection.GetConnection();
            connection.Open();
            using var command = new MySqlCommand(query, connection);
            AddParameters(command, item);
            return command.ExecuteNonQuery() == 1;
        }

        public bool Update(SanPhamDTO item)
        {
            const string query = @"
                UPDATE SanPham
                SET TenSanPham = @TenSanPham,
                    MaDanhMuc = @MaDanhMuc,
                    DonViTinh = @DonViTinh,
                    GiaNhap = @GiaNhap,
                    GiaBan = @GiaBan,
                    MoTa = @MoTa,
                    TrangThai = @TrangThai
                WHERE MaSanPham = @MaSanPham;";
            using var connection = DbConnection.GetConnection();
            connection.Open();
            using var command = new MySqlCommand(query, connection);
            AddParameters(command, item);
            command.Parameters.AddWithValue("@MaSanPham", item.MaSanPham);
            return command.ExecuteNonQuery() == 1;
        }

        public bool Delete(int id)
        {
            const string query = "DELETE FROM SanPham WHERE MaSanPham = @MaSanPham;";
            using var connection = DbConnection.GetConnection();
            connection.Open();
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaSanPham", id);
            return command.ExecuteNonQuery() == 1;
        }

        private static SanPhamDTO Map(MySqlDataReader reader) => new()
        {
            MaSanPham = reader.GetInt32("MaSanPham"),
            TenSanPham = reader.GetString("TenSanPham"),
            MaDanhMuc = reader.GetString("MaDanhMuc"),
            TenDanhMuc = reader.GetString("TenDanhMuc"),
            DonViTinh = reader.GetString("DonViTinh"),
            GiaNhap = reader.GetDecimal("GiaNhap"),
            GiaBan = reader.GetDecimal("GiaBan"),
            SoLuongTon = reader.GetInt32("SoLuongTon"),
            MoTa = reader.IsDBNull(reader.GetOrdinal("MoTa")) ? null : reader.GetString("MoTa"),
            TrangThai = reader.GetInt32("TrangThai"),
            NgayTao = reader.GetDateTime("NgayTao")
        };

        private static void AddParameters(MySqlCommand command, SanPhamDTO item)
        {
            command.Parameters.AddWithValue("@TenSanPham", item.TenSanPham);
            command.Parameters.AddWithValue("@MaDanhMuc", item.MaDanhMuc);
            command.Parameters.AddWithValue("@DonViTinh", item.DonViTinh);
            command.Parameters.AddWithValue("@GiaNhap", item.GiaNhap);
            command.Parameters.AddWithValue("@GiaBan", item.GiaBan);
            command.Parameters.AddWithValue("@MoTa", (object?)item.MoTa ?? DBNull.Value);
            command.Parameters.AddWithValue("@TrangThai", item.TrangThai);
        }
    }
}
