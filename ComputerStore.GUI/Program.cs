using System;
using System.Windows.Forms;
using ComputerStore.BLL.Services;
using ComputerStore.GUI.Forms.Authentication;
using ComputerStore.GUI.Forms.Main;

namespace ComputerStore.GUI
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            DatabaseService databaseService = new DatabaseService();
            if (!databaseService.TestConnection(out string message))
            {
                MessageBox.Show(
                    message,
                    "Lỗi kết nối Database",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            while (true)
            {
                using var frmLogin = new FrmLogin();
                if (frmLogin.ShowDialog() != DialogResult.OK || frmLogin.LoggedInAccount == null)
                    break;

                using var frmMain = new FrmMain(frmLogin.LoggedInAccount, frmLogin.LoggedInEmployee);
                Application.Run(frmMain);

                if (!frmMain.IsLogout)
                    break;
            }
        }
    }
}
