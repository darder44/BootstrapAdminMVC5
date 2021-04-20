using Bootstrap.Admin.Pages.Extensions;
using Bootstrap.Admin.Pages.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Bootstrap.Admin.Pages.Components
{
    /// <summary>
    /// 表格組件類
    /// </summary>
    public class TableBase<TItem> : ComponentBase
    {
        /// <summary>
        /// 獲得 IJSRuntime 實例
        /// </summary>
        [Inject]
        [NotNull]
        protected IJSRuntime? JSRuntime { get; set; }

        /// <summary>
        /// 每頁資料数量 預設 20 行
        /// </summary>
        protected const int DefaultPageItems = 20;

        /// <summary>
        /// 獲得/設置 組件 Id
        /// </summary>
        [Parameter]
        public string Id { get; set; } = "";

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
        /// 獲得/設置 按鈕模板 實例
        /// </summary>
        [Parameter]
        public RenderFragment<TItem>? ButtonTemplate { get; set; }

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
        /// 獲得/設置 表格 Toolbar 按鈕模板
        /// </summary>
        [Parameter]
        public RenderFragment? TableToolbarTemplate { get; set; }

        /// <summary>
        /// 獲得/設置 TableFooter 實例
        /// </summary>
        [Parameter]
        public RenderFragment? TableFooter { get; set; }

        /// <summary>
        /// 獲得/設置 是否固定表頭 預設為 false 不固定表頭
        /// </summary>
        [Parameter]
        public bool FixedHeader { get; set; }

        /// <summary>
        /// 獲得/設置 是否自适應高度 預設為 false 不自适應高度
        /// </summary>
        [Parameter]
        public bool AutoHeight { get; set; }

        /// <summary>
        /// 獲得/設置 是否顯示查詢框 預設為 false 不顯示查詢框
        /// </summary>
        [Parameter]
        public bool ShowSearch { get; set; }

        /// <summary>
        /// 獲得/設置 是否顯示高級查詢按鈕 預設顯示
        /// </summary>
        [Parameter]
        public bool ShowAdvancedSearch { get; set; } = true;

        /// <summary>
        /// 獲得/設置 資料集合
        /// </summary>
        protected IEnumerable<TItem> Items { get; set; } = new TItem[0];

        /// <summary>
        /// 獲得/設置 已选择的資料集合
        /// </summary>
        public List<TItem> SelectedItems { get; } = new List<TItem>();

        /// <summary>
        /// 獲得/設置 是否顯示行号
        /// </summary>
        [Parameter]
        public bool ShowLineNo { get; set; } = true;

        /// <summary>
        /// 獲得/設置 是否顯示选择列 預設為 false
        /// </summary>
        [Parameter]
        public bool ShowCheckbox { get; set; }

        /// <summary>
        /// 獲得/設置 是否顯示按鈕列 預設為 false
        /// </summary>
        [Parameter]
        public bool ShowDefaultButtons { get; set; }

        /// <summary>
        /// 獲得/設置 是否顯示表腳 預設為 false
        /// </summary>
        [Parameter]
        public bool ShowFooter { get; set; }

        /// <summary>
        /// 獲得/設置 是否顯示擴展按鈕 預設為 true
        /// </summary>
        [Parameter]
        public bool ShowExtendButtons { get; set; }

        /// <summary>
        /// 獲得/設置 是否顯示刷新按鈕 預設為 true
        /// </summary>
        [Parameter]
        public bool ShowRefresh { get; set; }

        /// <summary>
        /// 獲得/設置 是否分頁組件 預設為 false
        /// </summary>
        [Parameter]
        public bool ShowPagination { get; set; } = true;

        /// <summary>
        /// 獲得/設置 是否顯示工具欄 預設為 true
        /// </summary>
        [Parameter]
        public bool ShowToolBar { get; set; }

        /// <summary>
        /// 獲得/設置 按鈕列 Header 文本 預設為 操作
        /// </summary>
        [Parameter]
        public string ButtonTemplateHeaderText { get; set; } = "操作";

        /// <summary>
        /// 点击翻頁回調方法
        /// </summary>
        [Parameter]
        public Func<QueryPageOptions, QueryData<TItem>>? OnQuery { get; set; }

        /// <summary>
        /// 新建按鈕回調方法
        /// </summary>
        [Parameter]
        public Func<TItem>? OnAdd { get; set; }

        /// <summary>
        /// 編輯按鈕回調方法
        /// </summary>
        [Parameter]
        public Action<TItem>? OnEdit { get; set; }

        /// <summary>
        /// 保存按鈕回調方法
        /// </summary>
        [Parameter]
        public Func<TItem, bool>? OnSave { get; set; }

        /// <summary>
        /// 表頭排序時回調方法
        /// </summary>
        [Parameter]
        public Action<string, SortOrder> OnSort { get; set; } = new Action<string, SortOrder>((name, order) => { });

        /// <summary>
        /// 刪除按鈕回調方法
        /// </summary>
        [Parameter]
        public Func<IEnumerable<TItem>, bool>? OnDelete { get; set; }

        /// <summary>
        /// 獲得/設置 每頁資料数量
        /// </summary>
        [Parameter]
        public int PageItems { get; set; } = DefaultPageItems;

#nullable disable
        /// <summary>
        /// 獲得/設置 EditModel 實例
        /// </summary>
        [Parameter]
        public TItem EditModel { get; set; }

        /// <summary>
        /// 獲得/設置 QueryModel 實例
        /// </summary>
        [Parameter]
        public TItem QueryModel { get; set; }
#nullable restore

        /// <summary>
        /// 編輯資料彈窗 Title
        /// </summary>
        [Parameter]
        public string SubmitModalTitle { get; set; } = "";

        /// <summary>
        /// 編輯資料彈窗
        /// </summary>
        protected SubmitModal<TItem>? EditModal { get; set; }

        /// <summary>
        /// 確認刪除彈窗
        /// </summary>
        protected Modal? ConfirmModal { get; set; }

        /// <summary>
        /// 高級查詢彈窗
        /// </summary>
        protected Modal? SearchModal { get; set; }

        /// <summary>
        /// 獲得/設置 資料總條目
        /// </summary>
        protected int TotalCount { get; set; }

        /// <summary>
        /// 獲得/設置 當前頁碼
        /// </summary>
        protected int PageIndex { get; set; } = 1;

        /// <summary>
        /// 獲得/設置 當前排序字串名稱
        /// </summary>
        protected string SortName { get; set; } = "";

        /// <summary>
        /// 獲得/設置 當前排序规则
        /// </summary>
        protected SortOrder SortOrder { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            OnSort = new Action<string, SortOrder>((sortName, sortOrder) =>
            {
                (SortName, SortOrder) = (sortName, sortOrder);
                Query();
            });
            if (EditModel == null && OnAdd != null) EditModel = OnAdd.Invoke();
            if (OnQuery != null)
            {
                var queryData = OnQuery(new QueryPageOptions() { PageItems = DefaultPageItems, SearchText = SearchText, SortName = SortName, SortOrder = SortOrder });
                Items = queryData.Items;
                TotalCount = queryData.TotalCount;
            }
        }

        /// <summary>
        /// OnAfterRender 方法
        /// </summary>
        protected override void OnAfterRender(bool firstRender)
        {
            // 調用客户端腳本
            JSRuntime.InitTableAsync(RetrieveId(), firstRender);
        }

        /// <summary>
        /// 点击頁碼調用此方法
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageItems"></param>
        protected void PageClick(int pageIndex, int pageItems)
        {
            if (pageIndex != PageIndex)
            {
                PageIndex = pageIndex;
                PageItems = pageItems;
                Query();
            }
        }

        /// <summary>
        /// 每頁紀錄條数變化是調用此方法
        /// </summary>
        protected void PageItemsChange(int pageItems)
        {
            if (OnQuery != null)
            {
                PageIndex = 1;
                PageItems = pageItems;
                Query();
            }
        }

        /// <summary>
        /// 选择框点击時調用此方法
        /// </summary>
        /// <param name="item"></param>
        /// <param name="check"></param>
        protected void ToggleCheck(TItem item, bool check)
        {
            if (item == null)
            {
                SelectedItems.Clear();
                if (check) SelectedItems.AddRange(Items);
            }
            else
            {
                if (check) SelectedItems.Add(item);
                else SelectedItems.Remove(item);
            }
            StateHasChanged();
        }

        /// <summary>
        /// 表頭 CheckBox 狀態更新方法
        /// </summary>
        /// <returns></returns>
        protected CheckBoxState CheckState(TItem item)
        {
            var ret = CheckBoxState.UnChecked;
            if (SelectedItems.Count > 0)
            {
                ret = SelectedItems.Count == Items.Count() ? CheckBoxState.Checked : CheckBoxState.Mixed;
            }
            return ret;
        }

        /// <summary>
        /// 新建按鈕方法
        /// </summary>
        public void Add()
        {
            if (OnAdd != null) EditModel = OnAdd.Invoke();
            SelectedItems.Clear();
            EditModal?.Toggle();
        }

        /// <summary>
        /// 顯示提示信息
        /// </summary>
        /// <param name="title"></param>
        /// <param name="text"></param>
        /// <param name="cate"></param>
        protected void ShowMessage(string title, string text, ToastCategory cate = ToastCategory.Success) => JSRuntime?.ShowToast(title, text, cate);

        /// <summary>
        /// 編輯按鈕方法
        /// </summary>
        public void Edit()
        {
            if (SelectedItems.Count == 1)
            {
#nullable disable
                EditModel = SelectedItems[0].Clone();
#nullable restore
                EditModal?.Toggle();
            }
            else
            {
                ShowMessage("編輯資料", "請选择一個要編輯的資料", ToastCategory.Information);
            }
        }

        /// <summary>
        /// 查詢按鈕調用此方法
        /// </summary>
        public void Query()
        {
            if (OnQuery != null)
            {
                SelectedItems.Clear();
                var queryData = OnQuery(new QueryPageOptions()
                {
                    PageIndex = PageIndex,
                    PageItems = PageItems,
                    SearchText = SearchText,
                    SortOrder = SortOrder,
                    SortName = SortName
                });
                Items = queryData.Items;
                PageIndex = queryData.PageIndex;
                TotalCount = queryData.TotalCount;
                StateHasChanged();
            }
        }

        /// <summary>
        /// 保存資料
        /// </summary>
        /// <param name="context"></param>
        protected void Save(EditContext context)
        {
            var valid = OnSave?.Invoke((TItem)context.Model) ?? false;
            if (valid)
            {
                EditModal?.Toggle();
                Query();
            }
            ShowMessage("保存資料", "保存資料" + (valid ? "成功" : "失败"), valid ? ToastCategory.Success : ToastCategory.Error);
        }

        /// <summary>
        /// 刪除按鈕方法
        /// </summary>
        public void Delete()
        {
            if (SelectedItems.Count > 0)
            {
                ConfirmModal?.Toggle();
            }
            else
            {
                ShowMessage("刪除資料", "請选择要刪除的資料", ToastCategory.Information);
            }
        }

        /// <summary>
        /// 確認刪除方法
        /// </summary>
        public void Confirm()
        {
            var result = OnDelete?.Invoke(SelectedItems) ?? false;
            if (result)
            {
                ConfirmModal?.Toggle();
                Query();
            }
            ShowMessage("刪除資料", "刪除資料" + (result ? "成功" : "失败"), result ? ToastCategory.Success : ToastCategory.Error);
        }

        /// <summary>
        /// 獲取 Id 字符串
        /// </summary>
        protected string RetrieveId() => $"{Id}_table";

        /// <summary>
        /// 重置查詢按鈕回調方法
        /// </summary>
        [Parameter]
        public Action? OnResetSearch { get; set; }

        /// <summary>
        /// 重置查詢方法
        /// </summary>
        protected void ResetSearchClick()
        {
            OnResetSearch?.Invoke();
            SearchClick();
        }

        /// <summary>
        /// 查詢方法
        /// </summary>
        protected void SearchClick()
        {
            // 查詢控件按鈕触發此事件
            PageIndex = 1;
            Query();
        }

        /// <summary>
        /// 高級查詢按鈕点击時調用此方法
        /// </summary>
        protected void AdvancedSearchClick()
        {
            // 彈出高級查詢彈窗
            SearchModal?.Toggle();
        }

        /// <summary>
        /// 獲得/設置 查詢關鍵字
        /// </summary>
        [Parameter]
        public string SearchText { get; set; } = "";

        /// <summary>
        /// 獲得/設置 查詢關鍵字改變事件
        /// </summary>
        [Parameter]
        public EventCallback<string> SearchTextChanged { get; set; }

        /// <summary>
        /// 重置查詢按鈕調用此方法
        /// </summary>
        protected void ClearSearchClick()
        {
            SearchText = "";
            Query();
        }
    }
}
