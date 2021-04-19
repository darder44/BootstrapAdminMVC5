using Microsoft.AspNetCore.Components;
using System;

namespace Bootstrap.Admin.Pages.Components
{
    /// <summary>
    /// Checkbox 組件基类
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public class CheckboxBase<TItem> : ComponentBase
    {
#nullable disable
        /// <summary>
        /// 獲得/設置 資料绑定项
        /// </summary>
        [Parameter]
        public TItem Item { get; set; }
#nullable restore

        /// <summary>
        /// 獲得/設置 显示文本
        /// </summary>
        [Parameter]
        public string Text { get; set; } = "";

        /// <summary>
        /// 獲得/設置 是否被选中
        /// </summary>
        protected bool Checked { get; set; }

        /// <summary>
        /// 勾选回調方法
        /// </summary>
        [Parameter]
        public Action<TItem, bool>? OnClick { get; set; }

        /// <summary>
        /// 組件狀態改变回調方法
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
        /// 獲得/設置 选择框狀態
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
        /// 点击选择框方法
        /// </summary>
        protected void ToggleClick()
        {
            Checked = !Checked;
            State = Checked ? CheckBoxState.Checked : CheckBoxState.UnChecked;
            OnClick?.Invoke(Item, Checked);
        }
    }
}
