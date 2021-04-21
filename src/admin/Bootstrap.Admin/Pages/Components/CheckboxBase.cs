using Microsoft.AspNetCore.Components;
using System;

namespace Bootstrap.Admin.Pages.Components
{
    /// <summary>
    /// Checkbox 組件基類
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public class CheckboxBase<TItem> : ComponentBase
    {
#nullable disable
        /// <summary>
        /// 獲得/設置 資料綁定項
        /// </summary>
        [Parameter]
        public TItem Item { get; set; }
#nullable restore

        /// <summary>
        /// 獲得/設置 顯示文本
        /// </summary>
        [Parameter]
        public string Text { get; set; } = "";

        /// <summary>
        /// 獲得/設置 是否被選中
        /// </summary>
        protected bool Checked { get; set; }

        /// <summary>
        /// 勾選回調方法
        /// </summary>
        [Parameter]
        public Action<TItem, bool>? OnClick { get; set; }

        /// <summary>
        /// 組件狀態改變回調方法
        /// </summary>
        [Parameter]
        public Func<TItem, CheckBoxState>? SetCheckCallback { get; set; }

        /// <summary>
        /// OnParametersSet 方法
        /// </summary>
        protected override void OnParametersSet()
        {
            State = SetCheckCallback?.Invoke(Item) ?? CheckBoxState.UnChecked;
            Checked = State == CheckBoxState.Checked;
        }

        /// <summary>
        /// 獲得/設置 選擇框狀態
        /// </summary>
        [Parameter]
        public CheckBoxState State { get; set; }

        /// <summary>
        /// RenderStateCss 方法
        /// </summary>
        /// <returns></returns>
        protected string RenderStateCss()
        {
            var ret = "form-checkbox";
            switch (State)
            {
                case CheckBoxState.Mixed:
                    ret = "form-checkbox is-indeterminate";
                    break;
                case CheckBoxState.Checked:
                    ret = "form-checkbox is-checked";
                    break;
                case CheckBoxState.UnChecked:
                    break;
            }
            return ret;
        }

        /// <summary>
        /// 點擊選擇框方法
        /// </summary>
        protected void ToggleClick()
        {
            Checked = !Checked;
            State = Checked ? CheckBoxState.Checked : CheckBoxState.UnChecked;
            OnClick?.Invoke(Item, Checked);
        }
    }
}
