namespace Bootstrap.Admin.Models
{
    /// <summary>
    /// 系統鎖屏資料模型
    /// </summary>
    public class LockModel : HeaderBarModel
    {
        /// <summary>
        /// 構造函数
        /// </summary>
        /// <param name="userName"></param>
        public LockModel(string? userName) : base(userName)
        {

        }

        /// <summary>
        /// 獲得/設置 返回路徑
        /// </summary>
        public string? ReturnUrl { get; set; }

        /// <summary>
        /// 獲得/設置 認證方式 Cookie Mobile Gitee GitHub
        /// </summary>
        public string? AuthenticationType { get; set; }
    }
}
