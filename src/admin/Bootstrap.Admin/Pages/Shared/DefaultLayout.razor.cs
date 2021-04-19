using Bootstrap.Admin.Models;
using Bootstrap.Admin.Pages.Components;
using Bootstrap.Admin.Pages.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.JSInterop;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Bootstrap.Admin.Pages.Shared
{
    /// <summary>
    ///
    /// </summary>
    public partial class DefaultLayout
    {
        /// <summary>
        ///
        /// </summary>
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; } = new ServerAuthenticationStateProvider();

        /// <summary>
        ///
        /// </summary>
        [Inject]
        public NavigationManager? NavigationManager { get; set; }

        /// <summary>
        /// 獲得/設置 組件名字
        /// </summary>
        [Inject]
        protected IHttpContextAccessor? HttpContextAccessor { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Inject]
        public IJSRuntime? JSRuntime { get; set; }

        /// <summary>
        ///
        /// </summary>
        public NavigatorBarModel Model { get; set; } = new NavigatorBarModel("");

        /// <summary>
        ///
        /// </summary>
        public string UserName { get; set; } = "";

        /// <summary>
        ///
        /// </summary>
        public string DisplayName { get; set; } = "";

        /// <summary>
        ///
        /// </summary>
        public string WebTitle { get; set; } = "";

        /// <summary>
        ///
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// 獲得/設置 系统首頁
        /// </summary>
        public string HomeUrl { get; protected set; } = "Pages";

        /// <summary>
        /// 獲得/設置 當前請求路徑
        /// </summary>
        protected string RequestUrl { get; set; } = "";

        /// <summary>
        /// Header 組件引用實例
        /// </summary>
        protected Header? Header { get; set; }

        /// <summary>
        /// SideBar 組件引用實例
        /// </summary>
        protected SideBar? SideBar { get; set; }

        /// <summary>
        /// Footer 組件引用實例
        /// </summary>
        protected Footer? Footer { get; set; }

        /// <summary>
        /// OnInitializedAsync 方法
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            // 網頁跳转监控
            if (NavigationManager != null)
            {
                NavigationManager.LocationChanged += NavigationManager_LocationChanged;
            }

            var state = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            if (!state.User.Identity!.IsAuthenticated)
            {
                NavigationManager?.NavigateTo("/Account/Login?returnUrl=" + WebUtility.UrlEncode(new Uri(NavigationManager.Uri).PathAndQuery));
            }
            else
            {
                IsAdmin = state.User.IsInRole("Administrators");
                UserName = state.User.Identity.Name ?? "";
            }
        }

        private void NavigationManager_LocationChanged(object? sender, LocationChangedEventArgs e)
        {
            var name = $"/{NavigationManager?.ToBaseRelativePath(e.Location)}";
            if (HttpContextAccessor != null) HttpContextAccessor.HttpContext?.SaveOnlineUser(name);
        }

        /// <summary>
        /// OnParametersSetAsync 方法
        /// </summary>
        protected override void OnParametersSet()
        {
            if (NavigationManager != null)
            {
                RequestUrl = new UriBuilder(NavigationManager.Uri).Path;
                Model = new NavigatorBarModel(UserName, RequestUrl.ToMvcMenuUrl());
                DisplayName = Model.DisplayName;
                WebTitle = Model.Title;
                WebFooter = Model.Footer;
            }
        }

        /// <summary>
        /// 顯示名稱变化时方法
        /// </summary>
        public void OnDisplayNameChanged(string displayName)
        {
            DisplayName = displayName;
            Header?.DisplayNameChanged.InvokeAsync(DisplayName);
            SideBar?.DisplayNameChanged.InvokeAsync(DisplayName);
        }

        /// <summary>
        /// 網站標題变化时触发此方法
        /// </summary>
        /// <param name="title"></param>
        public void OnWebTitleChanged(string title)
        {
            Header?.WebTitleChanged.InvokeAsync(title);
            SideBar?.WebTitleChanged.InvokeAsync(title);
        }

        /// <summary>
        /// 獲得/設置 網站頁脚文字
        /// </summary>
        /// <value></value>
        public string WebFooter { get; set; } = "";

        /// <summary>
        /// 網站頁脚文字变化是触发此方法
        /// </summary>
        /// <param name="text"></param>
        public void OnWebFooterChanged(string text) => Footer?.TextChanged.InvokeAsync(text);

        /// <summary>
        /// OnAfterRender 方法
        /// </summary>
        /// <param name="firstRender"></param>
        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender) JSRuntime.InitDocument();
        }
    }
}
