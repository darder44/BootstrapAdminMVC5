using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bootstrap.Admin.Pages.Components
{
    /// <summary>
    /// LgbEditForm 組件
    /// </summary>
    public class LgbEditFormBase : ComponentBase
    {
        /// <summary>
        /// 獲得/設置 Id 用於内部 Label-For
        /// </summary>
        [Parameter]
        public string Id { get; set; } = "";

        /// <summary>
        /// Gets or sets a collection of additional attributes that will be applied to the created <c>form</c> element.
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, object>? AdditionalAttributes { get; set; }

        /// <summary>
        /// Specifies the top-level model object for the form. An edit context will
        /// be constructed for this model. If using this parameter, do not also supply
        /// a value for <see cref="EditContext"/>.
        /// </summary>
        [Parameter]
        public object? Model { get; set; }

        /// <summary>
        /// Specifies the content to be rendered inside this
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// A callback that will be invoked when the form is submitted.
        /// If using this parameter, you are responsible for triggering any validation
        /// manually, e.g., by calling <see cref="EditContext.Validate"/>.
        /// </summary>
        [Parameter] public EventCallback<EditContext> OnSubmit { get; set; }

        /// <summary>
        /// A callback that will be invoked when the form is submitted and the
        /// <see cref="EditContext"/> is determined to be valid.
        /// </summary>
        [Parameter] public EventCallback<EditContext> OnValidSubmit { get; set; }

        /// <summary>
        /// A callback that will be invoked when the form is submitted and the
        /// <see cref="EditContext"/> is determined to be invalid.
        /// </summary>
        [Parameter] public EventCallback<EditContext> OnInvalidSubmit { get; set; }

        /// <summary>
        /// 驗證組件缓存 静態全局提高性能
        /// </summary>
        private static ConcurrentDictionary<(LgbEditFormBase EditForm, Type ModelType, string FieldName), IValidateComponent> _validatorCache = new ConcurrentDictionary<(LgbEditFormBase, Type, string), IValidateComponent>();

        /// <summary>
        /// 添加資料驗證組件到 EditForm 中
        /// </summary>
        /// <param name="key"></param>
        /// <param name="comp"></param>
        public void AddValidator((LgbEditFormBase EditForm, Type ModelType, string FieldName) key, IValidateComponent comp) => _validatorCache.AddOrUpdate(key, k => comp, (k, c) => c = comp);

        /// <summary>
        /// EditModel 資料模型驗證方法
        /// </summary>
        /// <param name="model"></param>
        /// <param name="context"></param>
        /// <param name="results"></param>
        public void ValidateObject(object model, ValidationContext context, List<ValidationResult> results)
        {
            // 遍历所有可驗證組件進行資料驗證
            foreach (var key in _validatorCache)
            {
                if (key.Key.EditForm == this && key.Key.ModelType == context.ObjectType)
                {
                    if (BootstrapAdminEditContextDataAnnotationsExtensions.TryGetValidatableProperty(new FieldIdentifier(model, key.Key.FieldName), out var propertyInfo))
                    {
                        // 設置其關联属性字串
                        var propertyValue = propertyInfo.GetValue(model);
                        context.MemberName = propertyInfo.Name;

                        var validator = _validatorCache[key.Key];

                        // 資料驗證
                        validator.ValidateProperty(propertyValue, context, results);
                        validator.ToggleMessage(results, false);
                    }
                }
            }
        }

        /// <summary>
        /// 字串驗證方法
        /// </summary>
        /// <param name="propertyValue"></param>
        /// <param name="context"></param>
        /// <param name="results"></param>
        public void ValidateProperty(object? propertyValue, ValidationContext context, List<ValidationResult> results)
        {
            if (_validatorCache.TryGetValue((this, context.ObjectType, context.MemberName ?? string.Empty), out var validator))
            {
                validator.ValidateProperty(propertyValue, context, results);
                validator.ToggleMessage(results, true);
            }
        }
    }
}
