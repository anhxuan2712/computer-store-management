
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
            string user = Environment.GetEnvironmentVariable(
                "COMPUTERSTORE_DB_USER");

            string password = Environment.GetEnvironmentVariable(
                "COMPUTERSTORE_DB_PASSWORD");

            if (string.IsNullOrWhiteSpace(user) ||
                string.IsNullOrWhiteSpace(password))
            {
                throw new InvalidOperationException(
                    "Chua cau hinh bien moi truong MySQL."
                );
            }

            var builder = new MySqlConnectionStringBuilder
            {
                Server = "localhost",
                Port = 3306,
                Database = "ComputerStore",
                UserID = user,
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
                    message = "Ket noi MySQL thanh cong!";
                    return true;
                }
            }
            catch (Exception ex)
            {
                message = "Ket noi MySQL that bai: " + ex.Message;
                return false;
            }
        }
    }
}
