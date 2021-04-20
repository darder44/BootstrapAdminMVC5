using Bootstrap.DataAccess;
using Bootstrap.Security;
using Bootstrap.Security.Mvc;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bootstrap.Admin.Models
{
    /// <summary>
    /// 網站設置 Model 實體類
    /// </summary>
    public class SettingsModel : NavigatorBarModel
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="controller"></param>
        public SettingsModel(ControllerBase controller) : base(controller)
        {
            Themes = DictHelper.RetrieveThemes();
            AutoLockScreen = EnableAutoLockScreen;
            DefaultApp = DictHelper.RetrieveDefaultApp();
            IPLocators = DictHelper.RetireveLocators();
            IPLocatorSvr = DictHelper.RetrieveLocaleIPSvr();
            ErrorLogPeriod = DictHelper.RetrieveExceptionsLogPeriod();
            OpLog = DictHelper.RetrieveLogsPeriod();
            LogLog = DictHelper.RetrieveLoginLogsPeriod();
            TraceLog = DictHelper.RetrieveAccessLogPeriod();
            CookiePeriod = DictHelper.RetrieveCookieExpiresPeriod();
            IPCachePeriod = DictHelper.RetrieveLocaleIPSvrCachePeriod();
            EnableDemo = DictHelper.RetrieveSystemModel();
            AdminPathBase = DictHelper.RetrievePathBase();
            EnableHealth = DictHelper.RetrieveHealth();
            Logins = DictHelper.RetrieveLogins();
            var view = DictHelper.RetrieveLoginView();
            var viewName = Logins.FirstOrDefault(d => d.Code == view)?.Name ?? "系統預設";
            LoginView = new KeyValuePair<string, string>(view, viewName);

            var dicts = DictHelper.RetrieveDicts();
            Apps = DictHelper.RetrieveApps().Where(d => !d.Key.Equals("BA", StringComparison.OrdinalIgnoreCase)).Select(k =>
            {
                var url = dicts.FirstOrDefault(d => d.Category == "應用首頁" && d.Name == k.Key && d.Define == 0)?.Code ?? "未設置";
                return (k.Key, k.Value, url);
            });

            // 實际後台網站名稱
            WebSiteTitle = DictHelper.RetrieveWebTitle(BootstrapAppContext.AppId);

            // 實际後台網站頁脚
            WebSiteFooter = DictHelper.RetrieveWebFooter(BootstrapAppContext.AppId);
        }

        /// <summary>
        /// 獲得 系統配置的所有样式表
        /// </summary>
        public IEnumerable<BootstrapDict> Themes { get; }

        /// <summary>
        /// 獲得 地理位置信息集合
        /// </summary>
        public IEnumerable<BootstrapDict> IPLocators { get; }

        /// <summary>
        /// 獲得 資料庫中配置的地理位置信息接口
        /// </summary>
        public string IPLocatorSvr { get; }

        /// <summary>
        /// 獲得 是否开啟自動锁屏
        /// </summary>
        public bool AutoLockScreen { get; }

        /// <summary>
        /// 獲得 是否开啟自動锁屏
        /// </summary>
        public bool DefaultApp { get; }

        /// <summary>
        /// 程式异常日誌保留時長
        /// </summary>
        public int ErrorLogPeriod { get; }

        /// <summary>
        /// 操作日誌保留時長
        /// </summary>
        public int OpLog { get; }

        /// <summary>
        /// 登入日誌保留時長
        /// </summary>
        public int LogLog { get; }

        /// <summary>
        /// 訪問日誌保留時長
        /// </summary>
        public int TraceLog { get; }

        /// <summary>
        /// Cookie保留時長
        /// </summary>
        public int CookiePeriod { get; }

        /// <summary>
        /// IP請求缓存時長
        /// </summary>
        public int IPCachePeriod { get; }

        /// <summary>
        /// 獲得/設置 是否為演示系統
        /// </summary>
        public bool EnableDemo { get; set; }

        /// <summary>
        /// 獲得/設置 後台管理網站地址
        /// </summary>
        public string AdminPathBase { get; set; }

        /// <summary>
        /// 獲得/設置 系統應用程式集合
        /// </summary>
        public IEnumerable<(string Key, string Name, string Url)> Apps { get; set; }

        /// <summary>
        /// 獲得/設置 是否开啟健康檢查
        /// </summary>
        public bool EnableHealth { get; set; }

        /// <summary>
        /// 獲得/設置 字典表中登入首頁集合
        /// </summary>
        public IEnumerable<BootstrapDict> Logins { get; set; }

        /// <summary>
        /// 獲得/設置 登入視圖名稱 預設是 Login
        /// </summary>
        public KeyValuePair<string, string> LoginView { get; set; }

        /// <summary>
        /// 獲得/設置 實际 BA 後台網站名稱
        /// </summary>
        public string WebSiteTitle { get; set; }

        /// <summary>
        /// 獲得/設置 實际 BA 後台網站頁脚
        /// </summary>
        public string WebSiteFooter { get; set; }
    }
}
