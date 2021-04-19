using Bootstrap.Admin.Pages.Components;
using Bootstrap.Admin.Pages.Extensions;
using Bootstrap.DataAccess;
using Microsoft.AspNetCore.Components;
using System;

namespace Bootstrap.Admin.Pages.Views.Admin.Components
{
    /// <summary>
    /// 部門維護組件
    /// </summary>
    public class LogsBase : ComponentBase
    {
        /// <summary>
        /// 獲得/設置 编辑類型實例
        /// </summary>
        protected Log DataContext { get; set; } = new Log();

        /// <summary>
        /// 獲得/設置 查詢绑定類型實例
        /// </summary>
        protected Log QueryModel { get; set; } = new Log();

        /// <summary>
        /// 獲得/設置 开始时间
        /// </summary>
        protected DateTime? StartTime { get; set; }

        /// <summary>
        /// 獲得/設置 开始时间
        /// </summary>
        protected DateTime? EndTime { get; set; }

        /// <summary>
        /// 資料查詢方法
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        protected QueryData<Log> Query(QueryPageOptions options)
        {
            var data = LogHelper.RetrievePages(options.ToPaginationOption(), StartTime, EndTime, QueryModel.CRUD);
            return data.ToQueryData();
        }

        /// <summary>
        /// 重置搜索方法
        /// </summary>
        protected void ResetSearch()
        {
            QueryModel.CRUD = "";
        }
    }
}
