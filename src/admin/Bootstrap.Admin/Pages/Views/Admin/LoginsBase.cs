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
    public class LoginsBase : ComponentBase
    {
        /// <summary>
        /// 獲得/設置 編輯類型實例
        /// </summary>
        protected LoginUser DataContext { get; set; } = new LoginUser();

        /// <summary>
        /// 獲得/設置 查詢绑定類型實例
        /// </summary>
        protected LoginUser QueryModel { get; set; } = new LoginUser();

        /// <summary>
        /// 獲得/設置 开始時间
        /// </summary>
        protected DateTime? StartTime { get; set; }

        /// <summary>
        /// 獲得/設置 开始時间
        /// </summary>
        protected DateTime? EndTime { get; set; }

        /// <summary>
        /// 資料查詢方法
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        protected QueryData<LoginUser> Query(QueryPageOptions options)
        {
            var data = LoginHelper.RetrievePages(options.ToPaginationOption(), StartTime, EndTime, QueryModel.Ip);
            return data.ToQueryData();
        }

        /// <summary>
        /// 格式化登入结果方法
        /// </summary>
        protected MarkupString FormatResult(string result)
        {
            var css = result == "登入成功" ? "success" : "danger";
            var icon = css == "success" ? "check" : "remove";
            return new MarkupString($"<span class=\"badge badge-md badge-{css}\"><i class=\"fa fa-{icon}\"></i>{result}</span>");
        }

        /// <summary>
        /// 重置查詢方法
        /// </summary>
        protected void ResetSearch()
        {
            QueryModel.Ip = "";
        }
    }
}
