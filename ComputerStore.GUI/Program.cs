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

            DatabaseService databaseService = new DatabaseService();

            bool isConnected = databaseService.TestConnection(
                out string message
            );

            if (!isConnected)
            {
                MessageBox.Show(
                    message,
                    "Loi ket noi Database",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }

            MessageBox.Show(
                message,
                "Kiem tra ket noi Database",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            Application.Run(new FrmLogin());
        }
    }
}