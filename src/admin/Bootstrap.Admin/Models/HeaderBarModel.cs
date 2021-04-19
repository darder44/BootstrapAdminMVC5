using Bootstrap.DataAccess;
using Bootstrap.Security.Mvc;
using System;

namespace Bootstrap.Admin.Models
{
    /// <summary>
    /// Header Model
    /// </summary>
    public class HeaderBarModel : AdminModel
    {
        /// <summary>
        /// 預設构造函数
        /// </summary>
        /// <param name="userName"></param>
        public HeaderBarModel(string? userName)
        {
            var user = UserHelper.RetrieveUserByUserName(userName);
            if (user != null)
            {
                Icon = user.Icon.Contains("://", StringComparison.OrdinalIgnoreCase) ? user.Icon : string.Format("{0}{1}", DictHelper.RetrieveIconFolderPath(), user.Icon);
                DisplayName = user.DisplayName;
                UserName = user.UserName;
                AppId = user.App;
                Css = user.Css;
                ActiveCss = string.IsNullOrEmpty(Css) ? Theme : Css;

                // 當前用户未設置應用程序时 使用當前配置 appId
                if (AppId.IsNullOrEmpty()) AppId = BootstrapAppContext.AppId;

                // 通過 AppCode 獲取用户預設應用的標題
                Title = DictHelper.RetrieveWebTitle(AppId);
                Footer = DictHelper.RetrieveWebFooter(AppId);

                // feat: https://gitee.com/LongbowEnterprise/dashboard/issues?id=I12VKZ
                // 後台系统網站圖標跟随個人中心設置的預設應用站点的展示
                WebSiteIcon = DictHelper.RetrieveWebIcon(AppId);
                WebSiteLogo = DictHelper.RetrieveWebLogo(AppId);
            }
            EnableBlazor = DictHelper.RetrieveEnableBlazor();
        }

        /// <summary>
        /// 獲得 當前用户登录名
        /// </summary>
        public string UserName { get; } = "";

        /// <summary>
        /// 獲得 當前用户显示名稱
        /// </summary>
        public string DisplayName { get; set; } = "";

        /// <summary>
        /// 獲得 用户頭像地址
        /// </summary>
        public string Icon { get; } = "";

        /// <summary>
        /// 獲取 個人網站样式
        /// </summary>
        public string Css { get; } = "";

        /// <summary>
        /// 獲得 當前設置的預設應用
        /// </summary>
        public string AppId { get; } = "";

        /// <summary>
        /// 獲得 當前样式
        /// </summary>
        public string ActiveCss { get; } = "";

        /// <summary>
        /// 獲得 是否开启 Blazor
        /// </summary>
        public bool EnableBlazor { get; }
    }
}
