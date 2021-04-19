using Bootstrap.Security.DataAccess;
using Longbow.Cache;
using System.Collections.Generic;

namespace Bootstrap.DataAccess
{
    /// <summary>
    /// 前台應用幫助類別
    /// </summary>
    public static class AppHelper
    {
        /// <summary>
        /// 通过角色 ID 獲得授权前台應用資料快取键值
        /// </summary>
        public const string RetrieveAppsByRoleIdDataKey = "AppHelper-RetrieveAppsByRoleId";

        /// <summary>
        /// 通过用户名稱獲得授权前台應用資料快取键值
        /// </summary>
        public const string RetrieveAppsByUserNameDataKey = DbHelper.RetrieveAppsByUserNameDataKey;
        /// <summary>
        /// 根據角色ID指派應用程序
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public static IEnumerable<App> RetrievesByRoleId(string roleId) => CacheManager.GetOrAdd(string.Format("{0}-{1}", RetrieveAppsByRoleIdDataKey, roleId), key => DbContextManager.Create<App>()?.RetrievesByRoleId(roleId), RetrieveAppsByRoleIdDataKey) ?? new App[0];

        /// <summary>
        /// 根據角色ID以及選定的App ID，保存到角色應用表
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="appIds"></param>
        /// <returns></returns>
        public static bool SaveByRoleId(string roleId, IEnumerable<string> appIds)
        {
            var ret = DbContextManager.Create<App>()?.SaveByRoleId(roleId, appIds) ?? false;
            if (ret) CacheCleanUtility.ClearCache(appIds: appIds, roleIds: new List<string>() { roleId });
            return ret;
        }

        /// <summary>
        /// 根據指定用户名獲得授权應用程序集合
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static IEnumerable<string> RetrievesByUserName(string? userName) => string.IsNullOrEmpty(userName) ? new string[0] : CacheManager.GetOrAdd($"{DbHelper.RetrieveAppsByUserNameDataKey}-{userName}", key => DbContextManager.Create<App>()?.RetrievesByUserName(userName), RetrieveAppsByUserNameDataKey) ?? new string[0];
    }
}
