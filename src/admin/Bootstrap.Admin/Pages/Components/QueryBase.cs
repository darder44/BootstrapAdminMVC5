using Microsoft.AspNetCore.Components;
using System;

namespace Bootstrap.Admin.Pages.Components
{
    /// <summary>
    /// 查詢組件
    /// </summary>
    public class QueryBase<TItem> : ComponentBase
    {
        private readonly string _defaultTitle = "查詢條件";
        private readonly string _defaultText = "查詢";

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public string Id { get; set; } = "";

        /// <summary>
        /// 查詢組件標題 預設為 查詢條件
        /// </summary>
        [Parameter]
        public string Title { get; set; } = "";

        /// <summary>
        /// 查詢按鈕顯示文字 預設為 查詢
        /// </summary>
        [Parameter]
        public string Text { get; set; } = "";

        /// <summary>
        /// 查詢組件模板
        /// </summary>
        [Parameter]
        public RenderFragment<TItem>? ChildContent { get; set; }

#nullable disable
        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public TItem QueryModel { get; set; }
#nullable restore

        /// <summary>
        /// 查詢按鈕回調方法
        /// </summary>
        [Parameter]
        public Action? OnQuery { get; set; }

        /// <summary>
        /// 参數設置方法
        /// </summary>
        protected override void OnParametersSet()
        {
            if (string.IsNullOrEmpty(Title)) Title = _defaultTitle;
            if (string.IsNullOrEmpty(Text)) Text = _defaultText;
        }
    }
}
