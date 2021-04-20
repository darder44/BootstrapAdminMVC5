using Bootstrap.DataAccess;
using Bootstrap.Security;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bootstrap.Admin.Models
{
    /// <summary>
    /// 側邊欄導航條 Model
    /// </summary>
    public class NavigatorBarModel : HeaderBarModel
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="controller"></param>
        public NavigatorBarModel(ControllerBase controller) : this(controller.User.Identity!.Name, $"~{controller.Request.Path}")
        {

        }

        /// <summary>
        /// Blazor 使用构造函数
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="activeUrl"></param>
        public NavigatorBarModel(string? userName, string activeUrl = "") : base(userName)
        {
            Navigations = MenuHelper.RetrieveSystemMenus(userName ?? "", activeUrl);
            var authApps = AppHelper.RetrievesByUserName(userName);
            Applications = string.IsNullOrEmpty(userName) ? new KeyValuePair<string, string>[0] : DictHelper.RetrieveApps().Where(app => app.Key.IsNullOrEmpty() || authApps.Any(key => key.Equals(app.Key, StringComparison.OrdinalIgnoreCase)));
        }

        /// <summary>
        /// 獲得 網站選單
        /// </summary>
        public IEnumerable<BootstrapMenu> Navigations { get; private set; }

        /// <summary>
        /// 獲得 網站應用程式
        /// </summary>
        public IEnumerable<KeyValuePair<string, string>> Applications { get; private set; }
    }
}
