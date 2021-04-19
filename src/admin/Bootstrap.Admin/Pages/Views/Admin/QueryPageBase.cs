using Bootstrap.Admin.Pages.Components;
using Bootstrap.DataAccess;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace Bootstrap.Admin.Pages.Views.Admin.Components
{
    /// <summary>
    /// 頁面組件基類
    /// </summary>
    public abstract class QueryPageBase<TItem> : PageBase where TItem : class, new()
    {
        /// <summary>
        /// 獲得/設置 是否固定表頭 預設為 false 不固定表頭
        /// </summary>
        [Parameter]
        public bool FixedHeader { get; set; }

        /// <summary>
        /// 獲得/設置 EditPage 實例
        /// </summary>
        protected EditPageBase<TItem>? EditPage { get; set; }

        /// <summary>
        /// 獲得/設置 TItem 實例
        /// </summary>
        protected TItem QueryModel { get; set; } = new TItem();

        /// <summary>
        /// 查詢方法
        /// </summary>
        /// <param name="options"></param>
        protected abstract QueryData<TItem> Query(QueryPageOptions options);

        /// <summary>
        /// OnParametersSet 方法
        /// </summary>
        public override System.Threading.Tasks.Task SetParametersAsync(ParameterView parameters)
        {
            parameters.SetParameterProperties(this);
            FixedHeader = DictHelper.RetrieveFixedTableHeader();
            return base.SetParametersAsync(ParameterView.Empty);
        }

        /// <summary>
        /// 新建方法
        /// </summary>
        /// <returns></returns>
        protected virtual TItem Add() => new TItem();

        /// <summary>
        /// 保存方法
        /// </summary>
        /// <param name="item"></param>
        protected abstract bool Save(TItem item);

        /// <summary>
        /// 删除方法
        /// </summary>
        /// <param name="items"></param>
        protected abstract bool Delete(IEnumerable<TItem> items);
    }
}
