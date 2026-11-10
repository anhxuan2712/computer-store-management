
using System;
using System.Windows.Forms;
using ComputerStore.BLL.Services;

namespace ComputerStore.GUI
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Khởi tạo service kiểm tra database
            DatabaseService databaseService = new DatabaseService();

            // Kiểm tra kết nối MySQL
            bool isConnected = databaseService.TestConnection(
                out string message
            );

            MessageBox.Show(
                message,
                "Kiểm tra kết nối Database",
                MessageBoxButtons.OK,
                isConnected ? MessageBoxIcon.Information
                            : MessageBoxIcon.Error
            );

            // Nếu database không kết nối được thì dừng ứng dụng
            if (!isConnected)
            {
                return;
            }

            // Nếu kết nối thành công thì mở Form đăng nhập
            Application.Run(new FrmLogin());
        }
    }
}
