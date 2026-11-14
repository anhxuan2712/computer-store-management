using ComputerStore.DAL.Database;
using ComputerStore.DTO;
using MySql.Data.MySqlClient;

namespace ComputerStore.DAL
{
    public class KhachHangDAL
    {
        private const string SelectColumns =
            "SELECT MaKhachHang, TenKhachHang, SoDienThoai, DiaChi, Email FROM KhachHang";

        public List<KhachHangDTO> GetAll()
        {
            using var conn = DbConnection.GetConnection();
            using var cmd = new MySqlCommand(SelectColumns + " ORDER BY MaKhachHang", conn);
            conn.Open();
            return ReadList(cmd);
        }

        public List<KhachHangDTO> Search(string keyword)
        {
            using var conn = DbConnection.GetConnection();
            using var cmd = new MySqlCommand(
                SelectColumns +
                " WHERE MaKhachHang LIKE @kw OR TenKhachHang LIKE @kw OR SoDienThoai LIKE @kw" +
                " ORDER BY MaKhachHang", conn);
            cmd.Parameters.AddWithValue("@kw", "%" + keyword + "%");
            conn.Open();
            return ReadList(cmd);
        }

        public bool Insert(KhachHangDTO kh)
        {
            using var conn = DbConnection.GetConnection();
            conn.Open();
            using var tran = conn.BeginTransaction();
            try
            {
                kh.MaKhachHang = GenerateNewId(conn, tran);
                using var cmd = new MySqlCommand(
                    "INSERT INTO KhachHang (MaKhachHang, TenKhachHang, SoDienThoai, DiaChi, Email) " +
                    "VALUES (@ma, @ten, @sdt, @diachi, @email)", conn, tran);
                AddParameters(cmd, kh);
                bool ok = cmd.ExecuteNonQuery() > 0;
                tran.Commit();
                return ok;
            }
            catch
            {
                tran.Rollback();
                throw;
            }
        }

        public bool Update(KhachHangDTO kh)
        {
            using var conn = DbConnection.GetConnection();
            using var cmd = new MySqlCommand(
                "UPDATE KhachHang SET TenKhachHang = @ten, SoDienThoai = @sdt, DiaChi = @diachi, Email = @email " +
                "WHERE MaKhachHang = @ma", conn);
            AddParameters(cmd, kh);
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Delete(string maKhachHang)
        {
            using var conn = DbConnection.GetConnection();
            using var cmd = new MySqlCommand("DELETE FROM KhachHang WHERE MaKhachHang = @ma", conn);
            cmd.Parameters.AddWithValue("@ma", maKhachHang);
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        /// <summary>Kiểm tra số điện thoại đã được khách hàng khác sử dụng chưa.</summary>
        public bool ExistsPhone(string soDienThoai, string? excludeMaKhachHang)
        {
            using var conn = DbConnection.GetConnection();
            using var cmd = new MySqlCommand(
                "SELECT COUNT(*) FROM KhachHang WHERE SoDienThoai = @sdt AND MaKhachHang <> @ma", conn);
            cmd.Parameters.AddWithValue("@sdt", soDienThoai);
            cmd.Parameters.AddWithValue("@ma", excludeMaKhachHang ?? string.Empty);
            conn.Open();
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        public bool HasHoaDon(string maKhachHang)
        {
            using var conn = DbConnection.GetConnection();
            using var cmd = new MySqlCommand("SELECT COUNT(*) FROM HoaDon WHERE MaKhachHang = @ma", conn);
            cmd.Parameters.AddWithValue("@ma", maKhachHang);
            conn.Open();
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        private static string GenerateNewId(MySqlConnection conn, MySqlTransaction tran)
        {
            using var cmd = new MySqlCommand(
                "SELECT MaKhachHang FROM KhachHang WHERE MaKhachHang REGEXP '^KH[0-9]+$' " +
                "ORDER BY CAST(SUBSTRING(MaKhachHang, 3) AS UNSIGNED) DESC LIMIT 1 FOR UPDATE", conn, tran);
            return IdGenerator.Next("KH", cmd.ExecuteScalar() as string, 3);
        }

        private static void AddParameters(MySqlCommand cmd, KhachHangDTO kh)
        {
            cmd.Parameters.AddWithValue("@ma", kh.MaKhachHang);
            cmd.Parameters.AddWithValue("@ten", kh.TenKhachHang);
            cmd.Parameters.AddWithValue("@sdt", kh.SoDienThoai);
            cmd.Parameters.AddWithValue("@diachi", (object?)kh.DiaChi ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@email", (object?)kh.Email ?? DBNull.Value);
        }

        private static List<KhachHangDTO> ReadList(MySqlCommand cmd)
        {
            var list = new List<KhachHangDTO>();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new KhachHangDTO
                {
                    MaKhachHang = reader.GetString("MaKhachHang"),
                    TenKhachHang = reader.GetString("TenKhachHang"),
                    SoDienThoai = reader.GetString("SoDienThoai"),
                    DiaChi = reader.IsDBNull(reader.GetOrdinal("DiaChi")) ? null : reader.GetString("DiaChi"),
                    Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString("Email")
                });
            }
            return list;
        }
    }
}
