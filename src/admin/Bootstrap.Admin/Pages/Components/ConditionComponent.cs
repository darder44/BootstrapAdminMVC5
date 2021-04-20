using System;
using Bootstrap.Admin.Pages.Extensions;
using Bootstrap.Admin.Pages.Shared;
using Bootstrap.Security.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Bootstrap.Admin.Pages.Components
{
    /// <summary>
    /// 條件輸出組件
    /// </summary>
    public class ConditionComponent : ComponentBase
    {
        /// <summary>
        /// 獲得/設置 IButtonAuthoriazation 實例
        /// </summary>
        [Inject]
        protected IButtonAuthorization? ComponentAuthorization { get; set; }

        /// <summary>
        /// 獲得/設置 是否顯示 預設 true 顯示
        /// </summary>
        [Parameter]
        public bool Inverse { get; set; }

        /// <summary>
        /// 獲得/設置 授權碼
        /// </summary>
        [Parameter]
        public string AuthKey { get; set; } = "";

        /// <summary>
        /// 獲得/設置 是否顯示
        /// </summary>
        [Parameter]
        public bool? Condition { get; set; }

        /// <summary>
        /// 獲得/設置 子控件
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// 獲得/設置 預設母版頁實例
        /// </summary>
        [CascadingParameter(Name = "Default")]
        public DefaultLayout? RootLayout { get; protected set; }

        /// <summary>
        /// 渲染組件方法
        /// </summary>
        /// <param name="builder"></param>
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            // 授權碼赋值時使用 IButtonAuthorization 服务進行判斷
            var render = false;
            if (!string.IsNullOrEmpty(AuthKey))
            {
                var task = RootLayout?.AuthenticationStateProvider.GetAuthenticationStateAsync();
                if (task != null)
                {
                    task.Wait();
                    var user = task.Result.User;
                    var url = new UriBuilder(RootLayout?.NavigationManager?.Uri ?? "").Path;
                    render = ComponentAuthorization?.Authorizate(user, url.ToMvcMenuUrl(), AuthKey) ?? false;
                }
            }
            else if (Condition.HasValue) render = Condition.Value;
            else render = RootLayout?.Model.IsDemo ?? false;
            if (Inverse) render = !render;
            if (render) builder.AddContent(0, ChildContent);
        }
    }
}
