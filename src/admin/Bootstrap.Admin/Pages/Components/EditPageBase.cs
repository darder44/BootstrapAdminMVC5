using Bootstrap.Admin.Pages.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;

namespace Bootstrap.Admin.Pages.Components
{
    /// <summary>
    /// 可编辑頁面組件包含查詢与資料表格
    /// </summary>
    public class EditPageBase<TItem> : ComponentBase
    {
        /// <summary>
        /// 獲得/設置 Id
        /// </summary>
        [Parameter]
        public string Id { get; set; } = "";

#nullable disable
        /// <summary>
        /// 獲得/設置 QueryModel 實例
        /// </summary>
        [Parameter]
        public TItem QueryModel { get; set; }
#nullable restore

        /// <summary>
        /// 查詢模板
        /// </summary>
        [Parameter]
        public RenderFragment<TItem>? QueryBody { get; set; }

        /// <summary>
        /// 查詢按钮回調方法
        /// </summary>
        [Parameter]
        public Func<QueryPageOptions, QueryData<TItem>>? OnQuery { get; set; }

        /// <summary>
        /// 獲得/設置 TableHeader 實例
        /// </summary>
        [Parameter]
        public RenderFragment<TItem>? TableHeader { get; set; }

        /// <summary>
        /// 獲得/設置 RowTemplate 實例
        /// </summary>
        [Parameter]
        public RenderFragment<TItem>? RowTemplate { get; set; }

        /// <summary>
        /// 獲得/設置 按钮模板
        /// </summary>
        [Parameter]
        public RenderFragment<TItem>? ButtonTemplate { get; set; }

        /// <summary>
        /// 獲得/設置 表格 Toolbar 按钮模板
        /// </summary>
        [Parameter]
        public RenderFragment? TableToolbarTemplate { get; set; }

        /// <summary>
        /// 獲得/設置 提示信息模板
        /// </summary>
        [Parameter]
        public RenderFragment? TableInfoTemplate { get; set; }

        /// <summary>
        /// 獲得/設置 TableFooter 實例
        /// </summary>
        [Parameter]
        public RenderFragment? TableFooter { get; set; }

        /// <summary>
        /// 獲得/設置 EditTemplate 實例
        /// </summary>
        [Parameter]
        public RenderFragment<TItem>? EditTemplate { get; set; }

        /// <summary>
        /// 獲得/設置 SearchTemplate 實例
        /// </summary>
        [Parameter]
        public RenderFragment<TItem>? SearchTemplate { get; set; }

        /// <summary>
        /// 獲得/設置 是否固定表頭 預設為 false 不固定表頭
        /// </summary>
        [Parameter]
        public bool FixedHeader { get; set; }

        /// <summary>
        /// 獲得/設置 Table 實例
        /// </summary>
        protected Table<TItem>? Table { get; set; }

        /// <summary>
        /// 编辑資料弹窗 Title
        /// </summary>
        [Parameter]
        public string SubmitModalTitle { get; set; } = "";

        /// <summary>
        /// 新建按钮回調方法
        /// </summary>
        [Parameter]
        public Func<TItem> OnAdd { get; set; } = () => throw new InvalidOperationException($"The property {nameof(OnAdd)} can't be set to Null");

        /// <summary>
        /// 保存按钮回調方法
        /// </summary>
        [Parameter]
        public Func<TItem, bool> OnSave { get; set; } = item => false;

        /// <summary>
        /// 重置搜索条件回調方法
        /// </summary>
        [Parameter]
        public Action OnResetSearch { get; set; } = () => { };

        /// <summary>
        /// 删除按钮回調方法
        /// </summary>
        [Parameter]
        public Func<IEnumerable<TItem>, bool> OnDelete { get; set; } = item => false;

        /// <summary>
        /// 組件初始化方法
        /// </summary>
        protected override void OnInitialized()
        {
            if (string.IsNullOrEmpty(Id)) throw new InvalidOperationException($"The property {nameof(Id)} can't be set to Null");
        }

        /// <summary>
        /// 資料表格内删除按钮方法
        /// </summary>
        /// <param name="item"></param>
        protected void Delete(TItem item)
        {
            if (Table != null)
            {
                Table.SelectedItems.Clear();
                Table.SelectedItems.Add(item);
                Table.Delete();
            }
        }

        /// <summary>
        /// 编辑方法
        /// </summary>
        protected void Edit(TItem item)
        {
            if (Table != null)
            {
                Table.SelectedItems.Clear();
                Table.SelectedItems.Add(item);
                Table.Edit();
            }
        }

        /// <summary>
        /// 獲得 Table 組件选择項目集合
        /// </summary>
        public IEnumerable<TItem> SelectedItems { get { return Table?.SelectedItems ?? new List<TItem>(); } }

        /// <summary>
        /// 分頁查詢方法
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        protected QueryData<TItem> QueryData(QueryPageOptions options) => OnQuery?.Invoke(options) ?? new QueryData<TItem>();
    }
}
