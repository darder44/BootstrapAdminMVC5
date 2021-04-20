using Bootstrap.Security;
using Bootstrap.Security.DataAccess;
using Bootstrap.Security.Mvc;
using Longbow.Cache;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bootstrap.DataAccess
{
    /// <summary>
    /// 選單操作類別
    /// </summary>
    public static class MenuHelper
    {
        /// <summary>
        /// 通过指定角色ID相關選單快取鍵值
        /// </summary>
        public const string RetrieveMenusByRoleIdDataKey = "MenuHelper-RetrieveMenusByRoleId";

        /// <summary>
        /// 通过當前用户獲取所有選單資料快取鍵名稱 "BootstrapMenu-RetrieveMenus"
        /// </summary>
        public const string RetrieveMenusAll = DbHelper.RetrieveMenusAll;

        /// <summary>
        /// 保存選單
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static bool Save(BootstrapMenu p)
        {
            // 不允許保存系統選單与前台演示系統的預設選單
            if (DictHelper.RetrieveSystemModel())
            {
                if (p.Category == "0") return true;

                // 查找原有資料比对是否為系統選單与演示選單
                if (!string.IsNullOrEmpty(p.Id))
                {

                    // 系統選單
                    var menus = RetrieveAllMenus("Admin");
#pragma warning disable CS8602 // 取消引用可能出现的空引用。
                    var menu = menus.FirstOrDefault(m => m.Id.Equals(p.Id, System.StringComparison.OrdinalIgnoreCase));
#pragma warning restore CS8602 // 取消引用可能出现的空引用。
                    if (menu != null && menu.Category == "0") return true;

                    // 演示系統
                    var appMenus = BootstrapAppContext.Configuration.GetSection("AppMenus").Get<ICollection<string>>();
                    if (appMenus.Any(m => m.Equals(menu?.Name, StringComparison.OrdinalIgnoreCase))) return true;
                }
            }

            var ret = DbContextManager.Create<Menu>()?.Save(p) ?? false;
            if (ret) CacheCleanUtility.ClearCache(menuIds: string.IsNullOrEmpty(p.Id) ? new List<string>() : new List<string>() { p.Id });
            return ret;
        }

        /// <summary>
        /// 刪除選單
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool Delete(IEnumerable<string> value)
        {
            if (DictHelper.RetrieveSystemModel())
            {
                // 不允許刪除系統選單与前台演示系統的預設選單
                var systemMenus = RetrieveAllMenus("Admin").Where(m => m.Category == "0");
                value = value.Where(v => !systemMenus.Any(m => m.Id == v));
                if (!value.Any()) return true;

                // 演示系統
                var appMenus = BootstrapAppContext.Configuration.GetSection("AppMenus").Get<ICollection<string>>();
                var appIds = RetrieveAllMenus("Admin").Where(m => appMenus.Any(app => m.Name.Equals(app, System.StringComparison.OrdinalIgnoreCase))).Select(m => m.Id);
#pragma warning disable CS8602 // 取消引用可能出现的空引用。
                value = value.Where(m => !appIds.Any(app => app.Equals(m, StringComparison.OrdinalIgnoreCase)));
#pragma warning restore CS8602 // 取消引用可能出现的空引用。
                if (!value.Any()) return true;
            }
            var ret = DbContextManager.Create<Menu>()?.Delete(value) ?? false;
            if (ret) CacheCleanUtility.ClearCache(menuIds: value);
            return ret;
        }

        /// <summary>
        /// 通过用户名獲得所有選單
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static IEnumerable<BootstrapMenu> RetrieveMenusByUserName(string? userName) => RetrieveAllMenus(userName);

        /// <summary>
        /// 通过角色獲取相關選單集合
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public static IEnumerable<string> RetrieveMenusByRoleId(string roleId) => CacheManager.GetOrAdd($"{RetrieveMenusByRoleIdDataKey}-{roleId}", k => DbContextManager.Create<Menu>()?.RetrieveMenusByRoleId(roleId), RetrieveMenusByRoleIdDataKey) ?? new string[0];

        /// <summary>
        /// 保存指定角色的所有選單
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="menuIds"></param>
        /// <returns></returns>
        public static bool SaveMenusByRoleId(string roleId, IEnumerable<string> menuIds)
        {
            var ret = DbContextManager.Create<Menu>()?.SaveMenusByRoleId(roleId, menuIds) ?? false;
            if (ret) CacheCleanUtility.ClearCache(menuIds: menuIds, roleIds: new List<string>() { roleId });
            return ret;
        }

        /// <summary>
        /// 獲取指定用户的應用程式選單
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="userName"></param>
        /// <param name="activeUrl"></param>
        /// <returns></returns>
        public static IEnumerable<BootstrapMenu> RetrieveAppMenus(string? appId, string? userName, string? activeUrl)
        {
            if (string.IsNullOrEmpty(appId) || string.IsNullOrEmpty(userName)) return new BootstrapMenu[0];

            var menus = RetrieveAllMenus(userName).Where(m => m.Category == "1" && m.IsResource == 0);
            menus = menus.Where(m => m.Application.Equals(appId, StringComparison.OrdinalIgnoreCase));
            return DbHelper.CascadeMenus(menus, activeUrl);
        }

        /// <summary>
        /// 通过當前用户名獲得後台選單，层次化後集合
        /// </summary>
        /// <param name="userName">當前登錄的用户名</param>
        /// <param name="activeUrl">當前訪問選單</param>
        /// <returns></returns>
        public static IEnumerable<BootstrapMenu> RetrieveSystemMenus(string userName, string? activeUrl = null)
        {
            if (string.IsNullOrEmpty(userName)) return new BootstrapMenu[0];
            var menus = RetrieveAllMenus(userName).Where(
                    m => m.Category == "0" && m.IsResource == 0
                );
            return DbHelper.CascadeMenus(menus, activeUrl);
        }

        /// <summary>
        /// 通过當前用户名獲得所有選單，层次化後集合
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static IEnumerable<BootstrapMenu> RetrieveMenus(string? userName)
        {
            var menus = RetrieveAllMenus(userName);
            return DbHelper.CascadeMenus(menus);
        }

        /// <summary>
        /// 通过用户獲得所有選單
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static IEnumerable<BootstrapMenu> RetrieveAllMenus(string? userName) => string.IsNullOrEmpty(userName) ? new BootstrapMenu[0] : CacheManager.GetOrAdd($"{RetrieveMenusAll}-{userName}", key => DbContextManager.Create<Menu>()?.RetrieveAllMenus(userName), RetrieveMenusAll) ?? new BootstrapMenu[0];

        /// <summary>
        /// 通过當前用户名与指定選單路徑獲取此選單下所有授權按鈕集合 (userName, url, auths) => bool
        /// </summary>
        /// <param name="userName">當前操作用户名</param>
        /// <param name="url">资源按鈕所属選單</param>
        /// <param name="auths">资源授權碼</param>
        /// <returns></returns>
        public static bool AuthorizateButtons(string userName, string url, string auths) => DbContextManager.Create<Menu>()?.AuthorizateButtons(userName, url, auths) ?? false;
    }
}
