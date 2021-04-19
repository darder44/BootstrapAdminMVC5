namespace Bootstrap.Admin.Pages.Components
{
    /// <summary>
    /// PageContentAttributes 實體类
    /// </summary>
    public class PageContentAttributes
    {
        /// <summary>
        /// 獲得/設置 頁面 ID
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// 獲得/設置 頁面名稱
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// 獲得/設置 是否显示
        /// </summary>
        public bool Active { get; set; }
    }
}
