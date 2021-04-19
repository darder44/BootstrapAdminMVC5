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
    public class SQLBase : ComponentBase
    {
        /// <summary>
        /// 獲得/設置 编辑类型實例
        /// </summary>
        protected DBLog DataContext { get; set; } = new DBLog();

        /// <summary>
        /// 獲得/設置 查詢绑定类型實例
        /// </summary>
        protected DBLog QueryModel { get; set; } = new DBLog();

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
        protected QueryData<DBLog> Query(QueryPageOptions options)
        {
            var data = LogHelper.RetrieveDBLogs(options.ToPaginationOption(), StartTime, EndTime, QueryModel.UserName);
            return data.ToQueryData();
        }

        /// <summary>
        /// 重置搜索方法
        /// </summary>
        protected void ResetSearch()
        {
            QueryModel.UserName = "";
        }
    }
}
