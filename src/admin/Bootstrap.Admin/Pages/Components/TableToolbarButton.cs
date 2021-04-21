using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Bootstrap.Admin.Pages.Components
{
    /// <summary>
    /// 表格 Toolbar 按鈕組件
    /// </summary>
    public class TableToolbarButton : ComponentBase
    {
        /// <summary>
        /// Gets or sets a collection of additional attributes that will be applied to the created <c>form</c> element.
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, object>? AdditionalAttributes { get; set; }

        /// <summary>
        /// 獲得/設置 Table Toolbar 實例
        /// </summary>
        [CascadingParameter]
        protected TableToolbarBase? Toolbar { get; set; }

        /// <summary>
        /// 獲得/設置 按鈕圖示 fa fa-fa
        /// </summary>
        [Parameter]
        public string Icon { get; set; } = "";

        /// <summary>
        /// 獲得/設置 按鈕顯示文字
        /// </summary>
        [Parameter]
        public string Title { get; set; } = "未設置";

        /// <summary>
        /// 組件初始化方法
        /// </summary>
        protected override void OnInitialized()
        {
            Toolbar?.AddButtons(this);
        }

        /// <summary>
        /// 點擊按鈕回調方法
        /// </summary>
        [Parameter]
        public Action OnClick { get; set; } = () => { };
    }
}
