using Bootstrap.DataAccess;
using Longbow.Web.Mvc;
using System;

namespace Bootstrap.Admin.Query
{
    /// <summary>
    /// 操作日誌查詢條件類
    /// </summary>
    public class QueryLogOption : PaginationOption
    {
        /// <summary>
        /// 獲得/設置 操作類型
        /// </summary>
        public string? OperateType { get; set; }

        /// <summary>
        /// 獲得/設置 开始時间
        /// </summary>
        public DateTime? OperateTimeStart { get; set; }

        /// <summary>
        /// 獲得/設置 结束時间
        /// </summary>
        public DateTime? OperateTimeEnd { get; set; }

        /// <summary>
        /// 獲得/設置 獲取查詢分頁資料
        /// </summary>
        /// <returns></returns>
        public QueryData<Log> RetrieveData()
        {
            var data = LogHelper.RetrievePages(this, OperateTimeStart, OperateTimeEnd, OperateType);
            var ret = new QueryData<Log>();
            ret.total = data.TotalItems;
            ret.rows = data.Items;
            return ret;
        }
    }
}
