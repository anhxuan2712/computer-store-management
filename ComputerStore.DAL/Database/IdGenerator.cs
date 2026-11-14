namespace ComputerStore.DAL.Database
{
    /// <summary>Sinh mã tự tăng dạng tiền tố + số, ví dụ KH001, PN00001.</summary>
    internal static class IdGenerator
    {
        public static string Next(string prefix, string? lastId, int digits)
        {
            int number = 0;
            if (!string.IsNullOrEmpty(lastId) && lastId.Length > prefix.Length)
            {
                int.TryParse(lastId.Substring(prefix.Length), out number);
            }
            return prefix + (number + 1).ToString().PadLeft(digits, '0');
        }
    }
}
