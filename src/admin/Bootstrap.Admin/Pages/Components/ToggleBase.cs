using Microsoft.AspNetCore.Components;

namespace Bootstrap.Admin.Pages.Components
{
    /// <summary>
    /// Toggle 开關組件
    /// </summary>
    public class ToggleBase : ComponentBase
    {
        /// <summary>
        /// 獲得/設置 組件高度
        /// </summary>
        [Parameter]
        public int Width { get; set; } = 120;

        /// <summary>
        /// 獲得/設置 組件 On 時顯示文本
        /// </summary>
        [Parameter]
        public string OnText { get; set; } = "展开";

        /// <summary>
        /// 獲得/設置 組件 Off 時顯示文本
        /// </summary>
        [Parameter]
        public string OffText { get; set; } = "收缩";

        /// <summary>
        /// 獲得/設置 組件是否處于 On 狀態 預設為 Off 狀態
        /// </summary>
        [Parameter]
        public bool Value { get; set; } = false;

        /// <summary>
        /// 獲得/設置 Value 值改变時回調事件
        /// </summary>
        [Parameter]
        public EventCallback<bool> ValueChanged { get; set; }

        /// <summary>
        /// 獲得/設置 Value 值改变時回調事件
        /// </summary>
        protected void ToggleClick()
        {
            Value = !Value;
            ValueChanged.InvokeAsync(Value);
        }
    }
}
