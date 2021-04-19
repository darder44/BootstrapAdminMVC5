using Bootstrap.DataAccess;
using Longbow.Web.Mvc;
using System;

namespace Bootstrap.Admin.Query
{
    /// <summary>
    /// SQL執行查詢配置类
    /// </summary>
    public class QuerySQLOption : PaginationOption
    {
        /// <summary>
        /// 獲得/設置 用户登录名
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// 獲得/設置 开始时间
        /// </summary>
        public DateTime? OperateTimeStart { get; set; }

        /// <summary>
        /// 獲得/設置 结束时间
        /// </summary>
        public DateTime? OperateTimeEnd { get; set; }

        /// <summary>
        /// 查詢資料方法
        /// </summary>
        /// <returns></returns>
        public QueryData<DBLog> RetrieveData()
        {
            var data = LogHelper.RetrieveDBLogs(this, OperateTimeStart, OperateTimeEnd, UserName);
            var ret = new QueryData<DBLog>();
            ret.total = data.TotalItems;
            ret.rows = data.Items;
            return ret;
        }
    }
}
