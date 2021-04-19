using Microsoft.AspNetCore.Components;

namespace Bootstrap.Admin.Pages.Views.Admin.Components
{
    /// <summary>
    /// 頁面組件基类
    /// </summary>
    public abstract class PageBase : ComponentBase
    {
        /// <summary>
        /// 是否重新绘制組件方法
        /// </summary>
        /// <returns></returns>
        protected override bool ShouldRender() => false;
    }
}
