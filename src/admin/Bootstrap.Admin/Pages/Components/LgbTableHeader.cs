using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Bootstrap.Admin.Pages.Components
{
    /// <summary>
    /// 表頭組件
    /// </summary>
    public class LgbTableHeader<TItem> : ComponentBase, ITableHeader
    {
#nullable disable
        /// <summary>
        /// 獲得/設置 資料绑定 Value
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, object> AdditionalAttributes { get; set; }

        /// <summary>
        /// 獲得/設置 資料绑定 Value
        /// </summary>
        [Parameter] public TItem Value { get; set; }
#nullable restore

        /// <summary>
        /// 獲得/設置 ValueChanged 事件
        /// </summary>
        [Parameter] public EventCallback<TItem> ValueChanged { get; set; }

        /// <summary>
        /// 獲得/設置 ValueExpression 表达式
        /// </summary>
        [Parameter]
        [NotNull]
        public Expression<Func<TItem>>? ValueExpression { get; set; }

        /// <summary>
        /// 獲得/設置 是否排序 預設 false
        /// </summary>
        [Parameter] public bool Sort { get; set; }

        /// <summary>
        /// 獲得/設置 Table Header 實例
        /// </summary>
        [CascadingParameter]
        protected TableHeaderBase? Header { get; set; }

        /// <summary>
        /// 組件初始化方法
        /// </summary>
        protected override void OnInitialized()
        {
            Header?.AddHeaders(this);
        }

        private FieldIdentifier? _fieldIdentifier;
        /// <summary>
        /// 獲取绑定字段显示名稱方法
        /// </summary>
        public string GetDisplayName()
        {
            var ret = "";
            if (_fieldIdentifier == null)
            {
                _fieldIdentifier = FieldIdentifier.Create(ValueExpression);
            }

            if (DisplayNamesExtensions.TryGetValue((_fieldIdentifier.Value.Model.GetType(), _fieldIdentifier.Value.FieldName), out var s))
            {
                ret = s;
            }
            if (string.IsNullOrEmpty(ret))
            {
                ret = _fieldIdentifier.HasValue ? FieldIdentifierExtensions.GetDisplayName(_fieldIdentifier.Value) : "";
            }
            return ret;
        }

        /// <summary>
        /// 獲取绑定字段信息方法
        /// </summary>
        public string GetFieldName()
        {
            if (_fieldIdentifier == null)
            {
                _fieldIdentifier = FieldIdentifier.Create(ValueExpression);
            }

            return _fieldIdentifier?.FieldName ?? "";
        }
    }
}
