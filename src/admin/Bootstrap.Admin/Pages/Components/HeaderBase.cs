using Bootstrap.Admin.Pages.Extensions;
using Bootstrap.Admin.Pages.Shared;
using Bootstrap.DataAccess;
using Microsoft.AspNetCore.Components;

namespace Bootstrap.Admin.Pages.Components
{
    /// <summary>
    ///
    /// </summary>
    public class HeaderBase : ComponentBase
    {
        /// <summary>
        /// 獲得 網站標題
        /// </summary>
        [Parameter]
        public string WebTitle { get; set; } = "";

        /// <summary>
        /// 獲得/設置 網站標題改變事件回調方法
        /// </summary>
        [Parameter]
        public EventCallback<string> WebTitleChanged { get; set; }

        /// <summary>
        /// 獲得 根模板頁實例
        /// </summary>
        [CascadingParameter(Name = "Default")]
        protected DefaultLayout? RootLayout { get; set; }

        /// <summary>
        /// 獲得/設置 用户圖示
        /// </summary>
        [Parameter]
        public string Icon { get; set; } = "";

        /// <summary>
        /// 獲得/設置 用户顯示名稱
        /// </summary>
        [Parameter]
        public string DisplayName { get; set; } = "";

        /// <summary>
        /// 獲得/設置 用户顯示名稱改變事件回調方法
        /// </summary>
        [Parameter]
        public EventCallback<string> DisplayNameChanged { get; set; }

        /// <summary>
        /// 獲得/設置 是否顯示 Blazor MVC 切换圖示
        /// </summary>
        protected bool EnableBlazor { get; set; }

        /// <summary>
        /// 参數赋值方法
        /// </summary>
        public override System.Threading.Tasks.Task SetParametersAsync(ParameterView parameters)
        {
            parameters.SetParameterProperties(this);
            EnableBlazor = DictHelper.RetrieveEnableBlazor();
            return base.SetParametersAsync(ParameterView.Empty);
        }
    }
}
