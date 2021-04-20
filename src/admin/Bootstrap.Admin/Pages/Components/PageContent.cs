using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;

namespace Bootstrap.Admin.Pages.Components
{
    /// <summary>
    /// PageContent 網頁組件
    /// </summary>
    public class PageContent : ComponentBase
    {
        /// <summary>
        /// 獲得/設置 組件名字
        /// </summary>
        [Parameter]
        public string Name { get; set; } = "";

        /// <summary>
        /// 渲染組件方法
        /// </summary>
        /// <param name="builder"></param>
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var name = Name.Replace("/", ".");
            if (!string.IsNullOrEmpty(name))
            {
                var t = Type.GetType($"Bootstrap.Admin.Pages.Views.{name}");
                if (t != null)
                {

                    builder.OpenComponent(0, t);
                    builder.CloseComponent();
                }
                else
                {
                    builder.OpenElement(0, "h6");
                    builder.AddContent(1, "正在玩命开發中... 敬請期待");
                    builder.CloseElement();
                }
            }
        }
    }
}
