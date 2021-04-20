using System.Linq;

namespace Bootstrap.Admin.Pages.Models
{
    /// <summary>
    /// 個人中心模型
    /// </summary>
    public class ProfilesModel : SettingsModel
    {
        /// <summary>
        /// 獲得 頭像文件大小
        /// </summary>
        public long Size { get; }

        /// <summary>
        /// 獲得 頭像文件名稱
        /// </summary>
        public string FileName { get; } = "";

        /// <summary>
        /// 獲得 當前用户預設應用程式名稱
        /// </summary>
        public string AppName { get; }

        /// <summary>
        /// 獲得 是否為第三方用户
        /// </summary>
        /// <remarks>第三方用户不允许修改密碼</remarks>
        public bool External { get; }

        /// <summary>
        /// 构造函数 Blazor 頁面調用
        /// </summary>
        public ProfilesModel(string? userName) : base(userName)
        {
            // 設置 當前用户預設應用名稱
            AppName = Applications.FirstOrDefault(app => app.Key == AppId).Value;
        }
    }
}
