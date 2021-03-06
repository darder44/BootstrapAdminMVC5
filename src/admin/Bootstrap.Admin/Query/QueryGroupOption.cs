using Bootstrap.DataAccess;
using Longbow.Web.Mvc;
using System;
using System.Linq;

namespace Bootstrap.Admin.Query
{
    /// <summary>
    /// 部門查詢條件類
    /// </summary>
    public class QueryGroupOption : PaginationOption
    {
        /// <summary>
        /// 獲得/設置 部門名稱
        /// </summary>
        public string? GroupName { get; set; }

        /// <summary>
        /// 獲得/設置 部門描述
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 部門查詢資料方法
        /// </summary>
        /// <returns></returns>
        public QueryData<object> RetrieveData()
        {
            // int limit, int offset, string name, string price, string sort, string order
            var data = GroupHelper.Retrieves();
            if (!string.IsNullOrEmpty(GroupName))
            {
                data = data.Where(t => t.GroupName.Contains(GroupName, StringComparison.OrdinalIgnoreCase));
            }
            if (!string.IsNullOrEmpty(Description))
            {
                data = data.Where(t => t.Description?.Contains(Description, StringComparison.OrdinalIgnoreCase) ?? false);
            }
            if (!string.IsNullOrEmpty(Search))
            {
                data = data.Where(t => t.GroupName.Contains(Search, StringComparison.OrdinalIgnoreCase) || (t.Description?.Contains(Search, StringComparison.OrdinalIgnoreCase) ?? false));
            }
            var ret = new QueryData<object>();
            ret.total = data.Count();
            data = Order == "asc" ? data.OrderBy(t => t.GroupName) : data.OrderByDescending(t => t.GroupName);
            ret.rows = data.Skip(Offset).Take(Limit).Select(g => new { g.Id, g.GroupCode, g.GroupName, g.Description });
            return ret;
        }
    }
}
