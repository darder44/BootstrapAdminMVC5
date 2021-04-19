namespace Bootstrap.Admin.Models
{
    /// <summary>
    /// Model 基類
    /// </summary>
    public class ModelBase
    {
        /// <summary>
        /// 獲取 網站 logo 小圖標
        /// </summary>
        public string WebSiteIcon { get; protected set; } = "~/favicon.ico";

        /// <summary>
        /// 獲得 網站圖標
        /// </summary>
        public string WebSiteLogo { get; protected set; } = "~/favicon.png";
    }
}
