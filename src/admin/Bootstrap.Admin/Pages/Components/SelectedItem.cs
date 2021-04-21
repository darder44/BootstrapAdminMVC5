namespace Bootstrap.Admin.Pages.Components
{
    /// <summary>
    /// 選項類
    /// </summary>
    public class SelectedItem
    {
        /// <summary>
        /// 獲得/設置 顯示名稱
        /// </summary>
        public string Text { get; set; } = "";

        /// <summary>
        /// 獲得/設置 選項值
        /// </summary>
        public string Value { get; set; } = "";

        /// <summary>
        /// 獲得/設置 是否選中
        /// </summary>
        public bool Active { get; set; }
    }
}
