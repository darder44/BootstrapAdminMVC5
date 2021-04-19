namespace Bootstrap.Admin.Pages.Components
{
    /// <summary>
    /// 查詢条件實體類
    /// </summary>
    public class QueryPageOptions
    {
        /// <summary>
        /// 獲得/設置 查詢關鍵字
        /// </summary>
        public string SearchText { get; set; } = "";

        /// <summary>
        /// 獲得/設置 排序字段名稱
        /// </summary>
        public string SortName { get; set; } = "";

        /// <summary>
        /// 獲得/設置 排序方式
        /// </summary>
        public SortOrder SortOrder { get; set; }

        /// <summary>
        /// 獲得/設置 當前頁码 首頁為 第一頁
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 獲得/設置 每頁条目数量
        /// </summary>
        public int PageItems { get; set; } = 20;
    }
}
