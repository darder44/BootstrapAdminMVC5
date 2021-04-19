using Bootstrap.Admin.Models;
using Bootstrap.Admin.Pages.Shared;
using Microsoft.AspNetCore.Components;

namespace Bootstrap.Admin.Pages.Components
{
    /// <summary>
    /// 側邊欄組件
    /// </summary>
    public class SideBarBase : ComponentBase
    {
        /// <summary>
        /// 獲得/設置 側邊欄绑定 Model 實例
        /// </summary>
        [Parameter]
        public NavigatorBarModel Model { get; set; } = new NavigatorBarModel("");

        /// <summary>
        /// 獲得 根模板頁實例
        /// </summary>
        [CascadingParameter(Name = "Default")]
        public DefaultLayout? RootLayout { get; protected set; }

        /// <summary>
        /// 獲得/設置 用户显示名稱
        /// </summary>
        [Parameter]
        public string DisplayName { get; set; } = "";

        /// <summary>
        /// 獲得/設置 用户显示名稱改变事件回調方法
        /// </summary>
        [Parameter]
        public EventCallback<string> DisplayNameChanged { get; set; }

        /// <summary>
        /// 獲得/設置 網站標題
        /// </summary>
        [Parameter]
        public string WebTitle { get; set; } = "";

        /// <summary>
        /// 獲得/設置 網站標題改变事件回調方法
        /// </summary>
        [Parameter]
        public EventCallback<string> WebTitleChanged { get; set; }
    }
}
