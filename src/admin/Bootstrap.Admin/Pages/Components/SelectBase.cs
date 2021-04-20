using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bootstrap.Admin.Pages.Components
{
    /// <summary>
    /// Select 組件基類
    /// </summary>
    public class SelectBase<TItem> : ValidateInputBase<TItem>
    {
        /// <summary>
        /// 獲得/設置 Select 組件 列樣式 預設 col-sm-6
        /// </summary>
        [Parameter]
        public string ColumnClass { get; set; } = "col-sm-6";

        /// <summary>
        /// 當前选择項實例
        /// </summary>
        public SelectedItem SelectedItem { get; set; } = new SelectedItem();

        /// <summary>
        /// 獲得/設置 綁定資料集
        /// </summary>
        [Parameter]
        public List<SelectedItem> Items { get; set; } = new List<SelectedItem>();

        /// <summary>
        /// 獲得/設置 是否禁用
        /// </summary>
        [Parameter]
        public bool Disabled { get; set; }

        /// <summary>
        /// OnParametersSet 方法
        /// </summary>
        protected override void OnParametersSet()
        {
            Items.ForEach(t =>
            {
                t.Active = t.Value == Value?.ToString();
                if (t.Active) SelectedItem = t;
            });
        }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();
            if (!SelectedItem.Active)
            {
                SelectedItem = Items.FirstOrDefault(item => item.Active) ?? Items.First();
            }
        }

        /// <summary>
        /// SelectedItemChanged 方法
        /// </summary>
        [Parameter]
        public Action<SelectedItem>? SelectedItemChanged { get; set; }

        /// <summary>
        /// 下拉框項被选中時調用此方法
        /// </summary>
        public void ItemClickCallback(SelectedItem item)
        {
            SelectedItem = item;
            CurrentValueAsString = item.Value;
        }
    }
}
