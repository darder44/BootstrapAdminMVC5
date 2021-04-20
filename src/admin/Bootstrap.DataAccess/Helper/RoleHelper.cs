using Bootstrap.Security.DataAccess;
using Longbow.Cache;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bootstrap.DataAccess
{
    /// <summary>
    /// 角色操作幫助類別
    /// </summary>
    public static class RoleHelper
    {
        /// <summary>
        /// 獲取所有角色資料快取鍵值 RoleHelper-RetrieveRoles
        /// </summary>
        public const string RetrieveRolesDataKey = "RoleHelper-RetrieveRoles";
        /// <summary>
        /// 通过用户 ID 獲取相關角色集合鍵值 RoleHelper-RetrieveRolesByUserId
        /// </summary>
        public const string RetrieveRolesByUserIdDataKey = "RoleHelper-RetrieveRolesByUserId";
        /// <summary>
        /// 通过選單 ID 獲得相關角色集合鍵值 RoleHelper-RetrieveRolesByMenuId
        /// </summary>
        public const string RetrieveRolesByMenuIdDataKey = "RoleHelper-RetrieveRolesByMenuId";
        /// <summary>
        /// 通过部门 ID 獲得相關角色集合鍵值 RoleHelper-RetrieveRolesByGroupId
        /// </summary>
        public const string RetrieveRolesByGroupIdDataKey = "RoleHelper-RetrieveRolesByGroupId";

        /// <summary>
        /// 查詢所有角色
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Role> Retrieves() => CacheManager.GetOrAdd(RetrieveRolesDataKey, key => DbContextManager.Create<Role>()?.Retrieves()) ?? new Role[0];

        /// <summary>
        /// 保存用户角色關係
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        public static bool SaveByUserId(string userId, IEnumerable<string> roleIds)
        {
            // 演示模式時禁止修改 Admin 对 Administrators 角色的移除操作
            var ret = false;
            if (DictHelper.RetrieveSystemModel())
            {
                var users = new string[] { "Admin", "User" };
                var userIds = UserHelper.Retrieves().Where(u => users.Any(usr => usr.Equals(u.UserName, StringComparison.OrdinalIgnoreCase))).Select(u => u.Id);
                if (userIds.Any(u => (u ?? string.Empty).Equals(userId, StringComparison.OrdinalIgnoreCase))) ret = true;
            }
            if (ret) return ret;

            ret = DbContextManager.Create<Role>()?.SaveByUserId(userId, roleIds) ?? false;
            if (ret) CacheCleanUtility.ClearCache(userIds: new List<string>() { userId }, roleIds: roleIds);
            return ret;
        }

        /// <summary>
        /// 查詢某個用户所拥有的角色
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Role> RetrievesByUserId(string userId) => CacheManager.GetOrAdd($"{RetrieveRolesByUserIdDataKey}-{userId}", key => DbContextManager.Create<Role>()?.RetrievesByUserId(userId), RetrieveRolesByUserIdDataKey) ?? new Role[0];

        /// <summary>
        /// 刪除角色表
        /// </summary>
        /// <param name="value"></param>
        public static bool Delete(IEnumerable<string> value)
        {
            // 内置两個角色禁止修改
            var roles = new string[] { "Administrators", "Default" };
            var rs = Retrieves().Where(r => roles.Any(rl => rl.Equals(r.RoleName, StringComparison.OrdinalIgnoreCase)));
            value = value.Where(v => !rs.Any(r => r.Id == v));
            if (!value.Any()) return true;

            var ret = DbContextManager.Create<Role>()?.Delete(value) ?? false;
            if (ret) CacheCleanUtility.ClearCache(roleIds: value);
            return ret;
        }

        /// <summary>
        /// 保存新建/更新的角色訊息
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static bool Save(Role p)
        {
            // 内置两個角色禁止修改
            var roles = Retrieves().Where(r => new string[] { "Administrators", "Default" }.Any(s => s.Equals(r.RoleName, StringComparison.OrdinalIgnoreCase))).Select(r => r.Id ?? "");
            if (!string.IsNullOrEmpty(p.Id) && roles.Any(r => r.Equals(p.Id, StringComparison.OrdinalIgnoreCase))) return true;

            var ret = DbContextManager.Create<Role>()?.Save(p) ?? false;
            if (ret) CacheCleanUtility.ClearCache(roleIds: string.IsNullOrEmpty(p.Id) ? new List<string>() : new List<string> { p.Id });
            return ret;
        }

        /// <summary>
        /// 查詢某個選單所拥有的角色
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public static IEnumerable<Role> RetrievesByMenuId(string menuId) => CacheManager.GetOrAdd(string.Format("{0}-{1}", RetrieveRolesByMenuIdDataKey, menuId), key => DbContextManager.Create<Role>()?.RetrievesByMenuId(menuId), RetrieveRolesByMenuIdDataKey) ?? new Role[0];

        /// <summary>
        /// 通过指定選單ID保存角色
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        public static bool SavaByMenuId(string menuId, IEnumerable<string> roleIds)
        {
            var ret = DbContextManager.Create<Role>()?.SavaByMenuId(menuId, roleIds) ?? false;
            if (ret) CacheCleanUtility.ClearCache(roleIds: roleIds, menuIds: new List<string>() { menuId });
            return ret;
        }

        /// <summary>
        /// 根據GroupId查詢和該Group有關的所有Roles
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static IEnumerable<Role> RetrievesByGroupId(string groupId) => CacheManager.GetOrAdd(string.Format("{0}-{1}", RetrieveRolesByGroupIdDataKey, groupId), key => DbContextManager.Create<Role>()?.RetrievesByGroupId(groupId), RetrieveRolesByGroupIdDataKey) ?? new Role[0];

        /// <summary>
        /// 根據GroupId更新Roles訊息，刪除旧的Roles訊息，插入新的Roles訊息
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        public static bool SaveByGroupId(string groupId, IEnumerable<string> roleIds)
        {
            var ret = DbContextManager.Create<Role>()?.SaveByGroupId(groupId, roleIds) ?? false;
            if (ret) CacheCleanUtility.ClearCache(roleIds: roleIds, groupIds: new List<string>() { groupId });
            return ret;
        }

        /// <summary>
        /// 通过用户名獲取授權角色集合
        /// </summary>
        /// <param name="userName">指定用户名</param>
        /// <returns>角色名稱集合</returns>
        public static IEnumerable<string> RetrievesByUserName(string userName) => CacheManager.GetOrAdd(string.Format("{0}-{1}", DbHelper.RetrieveRolesByUserNameDataKey, userName), key => DbContextManager.Create<Role>()?.RetrievesByUserName(userName), DbHelper.RetrieveRolesByUserNameDataKey) ?? new string[0];

        /// <summary>
        /// 通过指定 Url 地址獲得授權角色集合
        /// </summary>
        /// <param name="url">請求 Url 地址</param>
        /// <param name="appId">應用程式Id</param>
        /// <returns>角色名稱集合</returns>
        public static IEnumerable<string> RetrievesByUrl(string url, string appId) => CacheManager.GetOrAdd(string.Format("{0}-{1}-{2}", DbHelper.RetrieveRolesByUrlDataKey, url, appId), key => DbContextManager.Create<Role>()?.RetrievesByUrl(url, appId), DbHelper.RetrieveRolesByUrlDataKey) ?? new string[0];
    }
}
