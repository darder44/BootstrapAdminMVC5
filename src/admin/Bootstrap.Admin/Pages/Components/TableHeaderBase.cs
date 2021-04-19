using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;

namespace Bootstrap.Admin.Pages.Components
{
    /// <summary>
    /// Table Header 組件
    /// </summary>
    public class TableHeaderBase : ComponentBase
    {
        /// <summary>
        /// Specifies the content to be rendered inside this
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// 添加表頭組件到集合方法
        /// </summary>
        public void AddHeaders(ITableHeader header) => Headers.Add(header);

        /// <summary>
        /// 獲得 表頭集合
        /// </summary>
        public ICollection<ITableHeader> Headers { get; } = new HashSet<ITableHeader>();

        /// <summary>
        /// 点击表頭排序是触发此回調函数
        /// </summary>
        [Parameter]
        public Action<string, SortOrder>? OnSort { get; set; }
    }
}
