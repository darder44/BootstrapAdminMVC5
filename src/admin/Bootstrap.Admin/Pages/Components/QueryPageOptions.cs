namespace Bootstrap.Admin.Pages.Components
{
    /// <summary>
    /// 查詢條件實體類
    /// </summary>
    public class QueryPageOptions
    {
        /// <summary>
        /// 獲得/設置 查詢關鍵字
        /// </summary>
        public string SearchText { get; set; } = "";

        /// <summary>
        /// 獲得/設置 排序字串名稱
        /// </summary>
        public string SortName { get; set; } = "";

        /// <summary>
        /// 獲得/設置 排序方式
        /// </summary>
        public SortOrder SortOrder { get; set; }

        /// <summary>
        /// 獲得/設置 當前頁碼 首頁為 第一頁
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 獲得/設置 每頁條目數量
        /// </summary>
        public int PageItems { get; set; } = 20;
    }
}
