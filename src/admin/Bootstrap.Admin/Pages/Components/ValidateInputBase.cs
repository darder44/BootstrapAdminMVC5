using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

namespace Bootstrap.Admin.Pages.Components
{
    /// <summary>
    /// 内置验证組件基類
    /// </summary>
    public abstract class ValidateInputBase<TItem> : InputBase<TItem>, IValidateComponent, IRules
    {
        /// <summary>
        /// 獲得 IJSRuntime 實例
        /// </summary>
        [Inject]
        protected IJSRuntime? JSRuntime { get; set; }

        /// <summary>
        /// 獲得 LgbEditFormBase 實例
        /// </summary>
        [CascadingParameter]
        public LgbEditFormBase? EditForm { get; set; }

        /// <summary>
        /// 獲得 當前組件 Id
        /// </summary>
        public string Id
        {
            get { return $"{EditForm?.Id}_{FieldIdentifier.FieldName}"; }
        }

        /// <summary>
        /// 獲得 子組件 RenderFragment 實例
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// 獲得 PlaceHolder 属性
        /// </summary>
        protected string? PlaceHolder
        {
            get
            {
                if (AdditionalAttributes != null &&
                    AdditionalAttributes.TryGetValue("placeholder", out var ph) &&
                    !string.IsNullOrEmpty(Convert.ToString(ph)))
                {
                    return ph.ToString();
                }
                return null;
            }
        }

        /// <summary>
        /// 獲得/設置 错误描述信息
        /// </summary>
        protected string ErrorMessage { get; set; } = "";

        /// <summary>
        /// 獲得/設置 資料合规样式
        /// </summary>
        protected string ValidCss { get; set; } = "";

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            EditForm?.AddValidator((EditForm, FieldIdentifier.Model.GetType(), FieldIdentifier.FieldName), this);
            DisplayName = FieldIdentifier.GetDisplayName();
        }

        /// <summary>
        /// OnAfterRender 方法
        /// </summary>
        /// <param name="firstRender"></param>
        protected override void OnAfterRender(bool firstRender)
        {
            if (!string.IsNullOrEmpty(_tooltipMethod) && !string.IsNullOrEmpty(Id))
            {
                JSRuntime.Tooltip(Id, _tooltipMethod);
                _tooltipMethod = "";
            }
        }

        /// <summary>
        /// 獲得 資料验证方法集合
        /// </summary>
        public ICollection<ValidatorComponentBase> Rules { get; } = new HashSet<ValidatorComponentBase>();

        private string _tooltipMethod = "";
        /// <summary>
        /// 属性验证方法
        /// </summary>
        /// <param name="propertyValue"></param>
        /// <param name="context"></param>
        /// <param name="results"></param>
        public void ValidateProperty(object? propertyValue, ValidationContext context, List<ValidationResult> results)
        {
            Rules.ToList().ForEach(validator => validator.Validate(propertyValue, context, results));
        }

        /// <summary>
        /// 顯示/隐藏验证结果方法
        /// </summary>
        /// <param name="results"></param>
        /// <param name="validProperty">是否对本属性进行資料验证</param>
        public void ToggleMessage(IEnumerable<ValidationResult> results, bool validProperty)
        {
            if (Rules.Any())
            {
                var messages = results.Where(item => item.MemberNames.Any(m => m == FieldIdentifier.FieldName));
                if (messages.Any())
                {
                    ErrorMessage = messages.First().ErrorMessage ?? string.Empty;
                    ValidCss = "is-invalid";

                    // 控件自身資料验证时顯示 tooltip
                    // EditForm 資料验证时調用 tooltip('enable') 保证 tooltip 組件生成
                    // 調用 tooltip('hide') 後導致鼠標悬停时 tooltip 无法正常顯示
                    _tooltipMethod = validProperty ? "show" : "enable";
                }
                else
                {
                    ErrorMessage = "";
                    ValidCss = "is-valid";
                    _tooltipMethod = "dispose";
                }
            }
        }

        /// <summary>
        /// 将 字符串 Value 属性转化為 泛型 Value 方法
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <param name="validationErrorMessage"></param>
        /// <returns></returns>
        protected override bool TryParseValueFromString(string? value, [MaybeNullWhen(false)] out TItem result, [NotNullWhen(false)] out string? validationErrorMessage)
        {
            if (!string.IsNullOrEmpty(value) && typeof(TItem) == typeof(string))
            {
                result = (TItem)(object)value;
                validationErrorMessage = null;
                return true;
            }
            else if (typeof(TItem).IsEnum)
            {
                var success = BindConverter.TryConvertTo<TItem>(value, CultureInfo.CurrentCulture, out var parsedValue);
                if (success)
                {
                    result = parsedValue!;
                    validationErrorMessage = null;
                    return true;
                }
                else
                {
                    result = default;
                    validationErrorMessage = $"The {FieldIdentifier.FieldName} field is not valid.";
                    return false;
                }
            }
            else if (typeof(TItem).IsValueType)
            {
                result = (TItem)Convert.ChangeType(value, typeof(TItem))!;
                validationErrorMessage = null;
                return true;
            }

            throw new InvalidOperationException($"{GetType()} does not support the type '{typeof(TItem)}'.");
        }
    }
}
