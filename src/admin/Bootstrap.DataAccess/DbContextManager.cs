using System;

namespace Bootstrap.DataAccess
{
    /// <summary>
    /// 根據配置文件動態加载不同資料庫實體静態類別
    /// </summary>
    public static class DbContextManager
    {
        /// <summary>
        /// 創建資料庫實體類別時發生異常實例
        /// </summary>
        public static Exception? Exception { get; private set; }

        /// <summary>
        /// 根據配置文件動態創建資料庫實體類別方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T? Create<T>() where T : class
        {
            T? t = default;
            try
            {
                Exception = null;
                t = Longbow.Data.DbContextManager.Create<T>();
            }
            catch (Exception ex)
            {
                Exception = ex;
            }
            return t;
        }
    }
}
