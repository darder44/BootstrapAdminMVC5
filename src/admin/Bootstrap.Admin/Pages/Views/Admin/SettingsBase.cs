using Bootstrap.Admin.Pages.Components;
using Bootstrap.Admin.Pages.Shared;
using Bootstrap.DataAccess;
using Bootstrap.Security;
using Bootstrap.Security.Mvc;
using Longbow.Cache;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bootstrap.Admin.Pages.Views.Admin.Components
{
    /// <summary>
    /// 網站設置組件
    /// </summary>
    public class SettingsBase : ComponentBase
    {
        /// <summary>
        /// 獲得 EditModel 實例
        /// </summary>
        protected EditModel Model { get; set; } = new EditModel();

        /// <summary>
        /// 獲得 CacheItem 實例
        /// </summary>
        protected ICacheItem ConsoleCaCheModel { get; set; } = new CacheItem("");

        /// <summary>
        /// 獲得 CacheItem 實例
        /// </summary>
        protected ICacheItem ClientCaCheModel { get; set; } = new CacheItem("");

        /// <summary>
        /// 獲得/設置 預設母版頁實例
        /// </summary>
        [CascadingParameter(Name = "Default")]
        public DefaultLayout? RootLayout { get; protected set; }

        /// <summary>
        /// IJSRuntime 接口實例
        /// </summary>
        [Inject]
        protected IJSRuntime? JSRuntime { get; set; }

        /// <summary>
        /// NavigationManager 實例
        /// </summary>
        [Inject]
        public NavigationManager? NavigationManager { get; set; }

        /// <summary>
        /// 顯示提示信息
        /// </summary>
        /// <param name="text"></param>
        /// <param name="ret"></param>
        protected void ShowMessage(string text, bool ret = true) => JSRuntime?.ShowToast("網站設置", ret ? $"{text}成功" : $"{text}失败", ret ? ToastCategory.Success : ToastCategory.Error);

        /// <summary>
        /// 設置参数方法
        /// </summary>
        protected override void OnInitialized()
        {
            Model.Title = DictHelper.RetrieveWebTitle(BootstrapAppContext.AppId);
            Model.Footer = DictHelper.RetrieveWebFooter(BootstrapAppContext.AppId);
            Model.Theme = RootLayout?.Model.Theme ?? "";
            Model.ShowSideBar = DictHelper.RetrieveSidebarStatus();
            Model.ShowCardTitle = DictHelper.RetrieveCardTitleStatus();
            Model.ShowMobile = DictHelper.RetrieveMobileLogin();
            Model.ShowOAuth = DictHelper.RetrieveOAuthLogin();
            Model.AutoLockScreen = DictHelper.RetrieveAutoLockScreen();
            Model.LockScreenPeriod = DictHelper.RetrieveAutoLockScreenPeriod();
            Model.DefaultApp = DictHelper.RetrieveDefaultApp();
            Model.EnableBlazor = DictHelper.RetrieveEnableBlazor();
            Model.FixedTableHeader = DictHelper.RetrieveFixedTableHeader();
            Model.Themes = DictHelper.RetrieveThemes();
            Model.EnableHealth = DictHelper.RetrieveHealth();

            Model.IPLocators = new SelectedItem[] { new SelectedItem() { Text = "未設置", Value = "None" } }.Union(DictHelper.RetireveLocators().Select(d => new SelectedItem()
            {
                Text = d.Name,
                Value = d.Code
            }));

            var ipSvr = DictHelper.RetrieveLocaleIPSvr();
            var ipSvrText = Model.IPLocators.FirstOrDefault(i => i.Value == ipSvr)?.Text ?? "未設置";
            var ipSvrValue = ipSvrText == "未設置" ? "None" : ipSvr;
            Model.SelectedIPLocator = new SelectedItem()
            {
                Text = ipSvrText,
                Value = ipSvrValue
            };

            Model.ErrorLogPeriod = DictHelper.RetrieveExceptionsLogPeriod();
            Model.OpLog = DictHelper.RetrieveLogsPeriod();
            Model.LogLog = DictHelper.RetrieveLoginLogsPeriod();
            Model.TraceLog = DictHelper.RetrieveAccessLogPeriod();
            Model.CookiePeriod = DictHelper.RetrieveCookieExpiresPeriod();
            Model.IPCachePeriod = DictHelper.RetrieveLocaleIPSvrCachePeriod();
            Model.EnableDemo = DictHelper.RetrieveSystemModel();

            Model.Logins = DictHelper.RetrieveLogins().Select(d => new SelectedItem(){
                Value = d.Code,
                Text = d.Name
            });
            var view = DictHelper.RetrieveLoginView();
            var viewName = Model.Logins.FirstOrDefault(d => d.Value == view)?.Text ?? "系统預設";
            Model.SelectedLogin = new SelectedItem() {  Value = view, Text = viewName };
            Model.AdminPathBase = DictHelper.RetrievePathBase();

            var dicts = DictHelper.RetrieveDicts();
            Model.Apps = DictHelper.RetrieveApps().Where(d => !d.Key.Equals("BA", StringComparison.OrdinalIgnoreCase)).Select(k =>
            {
                var url = dicts.FirstOrDefault(d => d.Category == "應用首頁" && d.Name == k.Key && d.Define == 0)?.Code ?? "未設置";
                return (k.Key, k.Value, url);
            });
        }

        /// <summary>
        /// QueryData 方法
        /// </summary>
        protected QueryData<ICacheItem> QueryData(QueryPageOptions options)
        {
            var data = CacheManager.ToList();
            var pageItems = data.Count();
            return new QueryData<ICacheItem>()
            {
                Items = data,
                PageItems = pageItems,
                TotalCount = pageItems
            };
        }

        /// <summary>
        /// 清除指定鍵值的方法
        /// </summary>
        protected void DeleteCache(string key) => CacheManager.Clear(key);

        /// <summary>
        /// 清除所有缓存方法
        /// </summary>
        protected void ClearCache() => CacheManager.Clear();

        /// <summary>
        /// 保存 Balzor 方法
        /// </summary>
        protected void SaveBlazor()
        {
            var ret = DictHelper.SaveSettings(new BootstrapDict[] { new BootstrapDict() { Category = "網站設置", Name = "Blazor", Code = Model.EnableBlazor ? "1" : "0" } });
            if (Model.EnableBlazor) ShowMessage("MVC 切换設置保存", ret);

            // 根据保存结果隐藏 Header 挂件
            if (ret) RootLayout?.JSRuntime?.ToggleBlazor(Model.EnableBlazor);
        }

        /// <summary>
        /// 保存 網站調整 方法
        /// </summary>
        protected void SaveWebSettings()
        {
            var ret = DictHelper.SaveSettings(new BootstrapDict[]{
                 new BootstrapDict() { Category = "網站設置", Name = "側邊欄狀態", Code = Model.ShowSideBar ? "1" : "0" },
                 new BootstrapDict() { Category = "網站設置", Name = "卡片標題狀態", Code = Model.ShowCardTitle ? "1" : "0" },
                 new BootstrapDict() { Category = "網站設置", Name = "固定表頭", Code = Model.FixedTableHeader ? "1" : "0" }
            });
            ShowMessage("保存網站調整", ret);

            // 根据保存结果設置網站样式
            if (ret) RootLayout?.JSRuntime?.SetWebSettings(Model.ShowSideBar, Model.ShowCardTitle, Model.FixedTableHeader);
        }

        /// <summary>
        /// 保存 登陆設置
        /// </summary>
        protected void SaveLogin()
        {
            var ret = DictHelper.SaveSettings(new BootstrapDict[]{
                 new BootstrapDict() { Category = "網站設置", Name = "OAuth 認证登录", Code = Model.ShowOAuth ? "1" : "0" },
                 new BootstrapDict() { Category = "網站設置", Name = "短信验证码登录", Code = Model.ShowMobile ? "1" : "0" }
            });
            ShowMessage("保存登录設置", ret);
        }

        /// <summary>
        /// 保存地理位置信息
        /// </summary>
        protected void SaveIPLocator()
        {
            var ret = DictHelper.SaveSettings(new BootstrapDict[]
            {
                new BootstrapDict() { Category = "網站設置", Name = "IP地理位置接口", Code = Model.SelectedIPLocator.Value }
            });
            ShowMessage("保存 IP 地理位置", ret);
        }

        /// <summary>
        /// 保存網站標題
        /// </summary>
        protected void SaveWebTitle()
        {
            var ret = DictHelper.SaveSettings(new BootstrapDict[]{
                new BootstrapDict() {
                    Category = "網站設置",
                    Name = "網站標題",
                    Code = Model.Title
                }
            });
            RootLayout?.OnWebTitleChanged(Model.Title);
            ShowMessage("保存網站標題", ret);
        }

        /// <summary>
        /// 保存網站頁脚
        /// </summary>
        protected void SaveWebFooter()
        {
            var ret = DictHelper.SaveSettings(new BootstrapDict[]{
                new BootstrapDict() {
                    Category = "網站設置",
                    Name = "網站頁脚",
                    Code = Model.Footer
                }
            });
            RootLayout?.OnWebFooterChanged(Model.Footer);
            ShowMessage("保存網站頁脚", ret);
        }

        /// <summary>
        /// 保存網站日志保留时長配置信息
        /// </summary>
        protected void SaveLogSettings()
        {
            var items = new BootstrapDict[]{
                new BootstrapDict() { Category = "網站設置", Name="程序异常保留时長", Code = Model.ErrorLogPeriod.ToString(), Define = 0 },
                new BootstrapDict() { Category = "網站設置", Name="操作日志保留时長", Code = Model.OpLog.ToString(), Define = 0 },
                new BootstrapDict() { Category = "網站設置", Name="登录日志保留时長", Code = Model.LogLog.ToString(), Define = 0 },
                new BootstrapDict() { Category = "網站設置", Name="访问日志保留时長", Code = Model.TraceLog.ToString(), Define = 0 },
                new BootstrapDict() { Category = "網站設置", Name="Cookie保留时長", Code = Model.CookiePeriod.ToString(), Define = 0 },
                new BootstrapDict() { Category = "網站設置", Name="IP請求缓存时長", Code = Model.IPCachePeriod.ToString(), Define = 0 }
            };
            var ret = DictHelper.SaveSettings(items);
            ShowMessage("保存日志缓存設置", ret);
        }

        /// <summary>
        /// 保存是否开启預設應用設置
        /// </summary>
        protected void SaveDefaultApp()
        {
            var ret = DictHelper.SaveSettings(new BootstrapDict[]{
                new BootstrapDict() {
                    Category = "網站設置",
                    Name = "預設應用程序",
                    Code = Model.DefaultApp ? "1" : "0"
                }
            });
            RootLayout?.OnWebFooterChanged(Model.Footer);
            ShowMessage("保存預設應用程序", ret);
        }

        /// <summary>
        /// 保存網站是否為演示模式
        /// </summary>
        protected async System.Threading.Tasks.Task SaveSystemModel()
        {
            var ret = DictHelper.UpdateSystemModel(Model.EnableDemo, Model.AuthKey);
            ShowMessage("保存演示系统設置", ret);
            if (ret)
            {
                await System.Threading.Tasks.Task.Delay(500);
                NavigationManager?.NavigateTo($"{RootLayout?.HomeUrl}/Admin/Settings", true);
            }
        }

        /// <summary>
        /// 網站設置编辑模型實體類
        /// </summary>
        protected class EditModel
        {
            /// <summary>
            /// 獲得/設置
            /// </summary>
            public string Title { get; set; } = "";

            /// <summary>
            /// 獲得/設置
            /// </summary>
            public string Footer { get; set; } = "";

            /// <summary>
            /// 獲得/設置
            /// </summary>
            public string Theme { get; set; } = "";

            /// <summary>
            /// 獲得/設置
            /// </summary>
            public bool ShowSideBar { get; set; }

            /// <summary>
            /// 獲得/設置
            /// </summary>
            public bool ShowCardTitle { get; set; }

            /// <summary>
            /// 獲得/設置
            /// </summary>
            public bool ShowMobile { get; set; }

            /// <summary>
            /// 獲得/設置
            /// </summary>
            public bool ShowOAuth { get; set; }

            /// <summary>
            /// 獲得/設置
            /// </summary>
            public bool AutoLockScreen { get; set; }

            /// <summary>
            /// 獲得/設置
            /// </summary>
            public int LockScreenPeriod { get; set; }

            /// <summary>
            /// 獲得/設置
            /// </summary>
            public bool DefaultApp { get; set; }

            /// <summary>
            /// 獲得/設置
            /// </summary>
            public bool EnableBlazor { get; set; }

            /// <summary>
            /// 獲得/設置 是否固定表頭
            /// </summary>
            public bool FixedTableHeader { get; set; }

            /// <summary>
            /// 獲得/設置 系统样式集合
            /// </summary>
            public IEnumerable<BootstrapDict> Themes { get; set; } = new HashSet<BootstrapDict>();

            /// <summary>
            /// 獲得/設置 地理位置配置信息集合
            /// </summary>
            public IEnumerable<SelectedItem> IPLocators { get; set; } = new SelectedItem[0];

            /// <summary>
            /// 獲得/設置 选中的地理位置配置信息
            /// </summary>
            public SelectedItem SelectedIPLocator { get; set; } = new SelectedItem();

            /// <summary>
            /// 程序异常日志保留时長
            /// </summary>
            public int ErrorLogPeriod { get; set; }

            /// <summary>
            /// 操作日志保留时長
            /// </summary>
            public int OpLog { get; set; }

            /// <summary>
            /// 登录日志保留时長
            /// </summary>
            public int LogLog { get; set; }

            /// <summary>
            /// 访问日志保留时長
            /// </summary>
            public int TraceLog { get; set; }

            /// <summary>
            /// Cookie保留时長
            /// </summary>
            public int CookiePeriod { get; set; }

            /// <summary>
            /// IP請求缓存时長
            /// </summary>
            public int IPCachePeriod { get; set; }

            /// <summary>
            /// 獲得/設置 授權码
            /// </summary>
            public string AuthKey { get; set; } = "";

            /// <summary>
            /// 獲得 系统是否為演示模式
            /// </summary>
            public bool EnableDemo { get; set; }

            /// <summary>
            /// 獲得 系统是否允许健康檢查
            /// </summary>
            public bool EnableHealth { get; set; }

            /// <summary>
            /// 獲得/設置 字典表中登录首頁集合
            /// </summary>
            public IEnumerable<SelectedItem> Logins { get; set; } = new SelectedItem[0];

            /// <summary>
            /// 獲得/設置 登录视圖名稱 預設是 Login
            /// </summary>
            public SelectedItem SelectedLogin { get; set; } = new SelectedItem();

            /// <summary>
            /// 獲得/設置 後台管理網站地址
            /// </summary>
            public string AdminPathBase { get; set; } = "";

            /// <summary>
            /// 獲得/設置 系统應用程序集合
            /// </summary>
            public IEnumerable<(string Key, string Name, string Url)> Apps { get; set; } = new List<(string, string, string)>();
        }
    }
}
