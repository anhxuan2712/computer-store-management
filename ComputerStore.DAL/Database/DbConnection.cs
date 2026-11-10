
using System;
using MySql.Data.MySqlClient;

namespace ComputerStore.DAL.Database
{
    public static class DbConnection
    {
        private static readonly string connectionString =
            BuildConnectionString();

        private static string BuildConnectionString()
        {
            string password =
                Environment.GetEnvironmentVariable(
                    "COMPUTERSTORE_DB_PASSWORD"
                );

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new InvalidOperationException(
                    "Chưa cấu hình biến môi trường COMPUTERSTORE_DB_PASSWORD."
                );
            }

            var builder = new MySqlConnectionStringBuilder
            {
                Server = "localhost",
                Port = 3306,
                Database = "ComputerStore",
                UserID = "root",
                Password = password,
                CharacterSet = "utf8mb4"
            };

            return builder.ConnectionString;
        }

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
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
