
using ComputerStore.DAL.Database;

namespace ComputerStore.BLL.Services
{
    public class DatabaseService
    {
        public bool TestConnection(out string message)
        {
            return DbConnection.TestConnection(out message);
        }
    }
}
