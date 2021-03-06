using Bootstrap.Admin.Pages.Components;
using PetaPoco;

namespace Bootstrap.Admin.Pages.Extensions
{
    /// <summary>
    /// Pages 擴展操作類
    /// </summary>
    public static class PagesExtensions
    {
        /// <summary>
        /// Pages 转化為 QueryData 實例方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pages"></param>
        /// <returns></returns>
        public static QueryData<T> ToQueryData<T>(this Page<T> pages)
        {
            return new QueryData<T>()
            {
                Items = pages.Items,
                PageIndex = (int)pages.CurrentPage,
                PageItems = (int)pages.ItemsPerPage,
                TotalCount = (int)pages.TotalItems,
            };
        }
    }
}
