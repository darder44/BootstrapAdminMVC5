namespace Bootstrap.Admin.Pages.Components
{
    /// <summary>
    /// 选項類
    /// </summary>
    public class SelectedItem
    {
        /// <summary>
        /// 獲得/設置 顯示名稱
        /// </summary>
        public string Text { get; set; } = "";

        /// <summary>
        /// 獲得/設置 选項值
        /// </summary>
        public string Value { get; set; } = "";

        /// <summary>
        /// 獲得/設置 是否选中
        /// </summary>
        public bool Active { get; set; }
    }
}
