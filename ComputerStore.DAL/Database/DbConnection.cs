
using System;
using MySql.Data.MySqlClient;

namespace ComputerStore.DAL.Database
{
    public static class DbConnection
    {
        private static string BuildConnectionString()
        {
            string user = Environment.GetEnvironmentVariable(
                "COMPUTERSTORE_DB_USER");
            if (string.IsNullOrWhiteSpace(user))
            {
                user = "root";
            }

            string password = Environment.GetEnvironmentVariable(
                "COMPUTERSTORE_DB_PASSWORD");

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new InvalidOperationException(
                    "Chua cau hinh bien moi truong COMPUTERSTORE_DB_PASSWORD."
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
            return new MySqlConnection(BuildConnectionString());
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
