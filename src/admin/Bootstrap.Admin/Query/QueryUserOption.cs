using Bootstrap.DataAccess;
using Longbow.Web.Mvc;
using System;
using System.Linq;

namespace Bootstrap.Admin.Query
{
    /// <summary>
    /// 用户維護查詢條件類
    /// </summary>
    public class QueryUserOption : PaginationOption
    {
        /// <summary>
        /// 獲得/設置 用户登入名稱
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 獲得/設置 用户顯示名稱
        /// </summary>
        public string? DisplayName { get; set; }

        /// <summary>
        /// 獲取用户分頁資料
        /// </summary>
        /// <returns></returns>
        public QueryData<object> RetrieveData()
        {
            // int limit, int offset, string name, string price, string sort, string order
            var data = UserHelper.Retrieves();
            if (!string.IsNullOrEmpty(Name))
            {
                data = data.Where(t => t.UserName.Contains(Name, StringComparison.OrdinalIgnoreCase));
            }
            if (!string.IsNullOrEmpty(DisplayName))
            {
                data = data.Where(t => t.DisplayName.Contains(DisplayName, StringComparison.OrdinalIgnoreCase));
            }
            if (!string.IsNullOrEmpty(Search))
            {
                data = data.Where(t => t.DisplayName.Contains(Search, StringComparison.OrdinalIgnoreCase) || t.Description.Contains(Search, StringComparison.OrdinalIgnoreCase) || t.UserName.Contains(Search, StringComparison.OrdinalIgnoreCase));
            }
            var ret = new QueryData<object>();
            ret.total = data.Count();
            switch (Sort)
            {
                case "UserName":
                    data = Order == "asc" ? data.OrderBy(t => t.UserName) : data.OrderByDescending(t => t.UserName);
                    break;
                case "DisplayName":
                    data = Order == "asc" ? data.OrderBy(t => t.DisplayName) : data.OrderByDescending(t => t.DisplayName);
                    break;
                case "RegisterTime":
                    data = Order == "asc" ? data.OrderBy(t => t.RegisterTime) : data.OrderByDescending(t => t.RegisterTime);
                    break;
                case "ApprovedTime":
                    data = Order == "asc" ? data.OrderBy(t => t.ApprovedTime) : data.OrderByDescending(t => t.ApprovedTime);
                    break;
                case "ApprovedBy":
                    data = Order == "asc" ? data.OrderBy(t => t.ApprovedBy) : data.OrderByDescending(t => t.ApprovedBy);
                    break;
            }
            ret.rows = data.Skip(Offset).Take(Limit).Select(u => new
            {
                u.Id,
                u.UserName,
                u.DisplayName,
                u.RegisterTime,
                u.ApprovedTime,
                u.ApprovedBy,
                u.Description,
                u.IsReset
            });
            return ret;
        }
    }
}
