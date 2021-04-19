namespace Bootstrap.DataAccess.Helper
{
    /// <summary>
    /// 資料庫自動生成幫助類別
    /// </summary>
    public static class AutoDbHelper
    {
        /// <summary>
        /// 資料庫檢查方法
        /// </summary>
        public static void EnsureCreated(string folder) => DbContextManager.Create<AutoDB>()?.EnsureCreated(folder);
    }
}
