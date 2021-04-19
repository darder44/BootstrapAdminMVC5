using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace Bootstrap.Admin.Pages.Components
{
    /// <summary>
    /// Dropdown 組件
    /// </summary>
    public class DropdownBase : ComponentBase
    {
        /// <summary>
        /// 獲得/設置 绑定資料集合
        /// </summary>
        [Parameter]
        public IEnumerable<SelectedItem> Items { get; set; } = new SelectedItem[0];

        /// <summary>
        /// 獲得/設置 选中项實例
        /// </summary>
        [Parameter]
        public SelectedItem Value { get; set; } = new SelectedItem();

        /// <summary>
        /// 獲得/設置 选中项改变回調方法
        /// </summary>
        [Parameter]
        public EventCallback<SelectedItem> ValueChanged { get; set; }

        /// <summary>
        ///
        /// </summary>
        protected void OnClick(SelectedItem item)
        {
            Value = item;
            if (ValueChanged.HasDelegate) ValueChanged.InvokeAsync(Value);
        }
    }
}
