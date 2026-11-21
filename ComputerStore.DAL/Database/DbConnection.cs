using System;
using MySql.Data.MySqlClient;

namespace ComputerStore.DAL.Database
{
    public static class DbConnection
    {
        private static string? connectionString;

        private static string GetConnectionString()
        {
            if (!string.IsNullOrEmpty(connectionString))
                return connectionString;

            string? user = Environment.GetEnvironmentVariable("COMPUTERSTORE_DB_USER");
            string? password = Environment.GetEnvironmentVariable("COMPUTERSTORE_DB_PASSWORD");

            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(password))
            {
                throw new InvalidOperationException(
                    "Chưa cấu hình biến môi trường MySQL (COMPUTERSTORE_DB_USER, COMPUTERSTORE_DB_PASSWORD)."
                );
            }

            string host = Environment.GetEnvironmentVariable("COMPUTERSTORE_DB_HOST") ?? "localhost";
            string database = Environment.GetEnvironmentVariable("COMPUTERSTORE_DB_NAME") ?? "ComputerStore";
            uint port = uint.TryParse(Environment.GetEnvironmentVariable("COMPUTERSTORE_DB_PORT"), out uint p) ? p : 3306;

            var builder = new MySqlConnectionStringBuilder
            {
                Server = host,
                Port = port,
                Database = database,
                UserID = user,
                Password = password,
                CharacterSet = "utf8mb4"
            };

            connectionString = builder.ConnectionString;
            return connectionString;
        }

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(GetConnectionString());
        }

        public static bool TestConnection(out string message)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    message = "Kết nối MySQL thành công!";
                    return true;
                }
            }
            catch (Exception ex)
            {
                message = "Kết nối MySQL thất bại: " + ex.Message;
                return false;
            }
        }
    }
}
