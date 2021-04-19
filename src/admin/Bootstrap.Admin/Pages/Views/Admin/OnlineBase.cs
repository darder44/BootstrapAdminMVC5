using Bootstrap.Admin.Pages.Components;
using Longbow.Web;
using Microsoft.AspNetCore.Components;
using System.Linq;

namespace Bootstrap.Admin.Pages.Views.Admin.Components
{
    /// <summary>
    /// 網站設置組件
    /// </summary>
    public class OnlineBase : ComponentBase
    {
        /// <summary>
        /// 獲得 EditModel 實例
        /// </summary>
        protected OnlineUser EditModel { get; set; } = new OnlineUser();

        /// <summary>
        /// IOnlineUsers 實例
        /// </summary>
        [Inject]
        public IOnlineUsers? OnlineUSers { get; set; }

        /// <summary>
        /// QueryData 方法
        /// </summary>
        protected QueryData<OnlineUser> QueryData(QueryPageOptions options)
        {
            var data = OnlineUSers?.OnlineUsers ?? new OnlineUser[0];
            return new QueryData<OnlineUser>()
            {
                Items = data,
                TotalCount = data.Count()
            };
        }
    }
}
