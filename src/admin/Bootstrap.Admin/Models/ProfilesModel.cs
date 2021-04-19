using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;

namespace Bootstrap.Admin.Models
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
        /// 獲得 是否為第三方用户
        /// </summary>
        /// <remarks>第三方用户不允许修改密码</remarks>
        public bool External { get; }

        /// <summary>
        /// 獲得 當前用户預設應用程序名稱
        /// </summary>
        public string AppName { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="host"></param>
        /// <param name="controller"></param>
        public ProfilesModel(ControllerBase controller, IWebHostEnvironment host) : base(controller)
        {
            if (host != null)
            {
                var fileName = Path.Combine(host.WebRootPath, Icon.TrimStart('~', '/').Replace('/', Path.DirectorySeparatorChar));

                // 資料库存储的個人圖片有後缀 default.jpg?v=1234567
                fileName = fileName.Split('?').FirstOrDefault();
                if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
                {
                    Size = new FileInfo(fileName).Length;
                    FileName = Path.GetFileName(fileName);
                }
            }

            if (controller.User.Identity!.AuthenticationType != CookieAuthenticationDefaults.AuthenticationScheme) External = true;

            // 設置 當前用户預設應用名稱
            AppName = Applications.FirstOrDefault(app => app.Key == AppId).Value;
        }
    }
}
