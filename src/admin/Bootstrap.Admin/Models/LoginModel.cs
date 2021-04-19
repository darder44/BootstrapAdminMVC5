using Bootstrap.DataAccess;

namespace Bootstrap.Admin.Models
{
    /// <summary>
    /// 登陆頁面 Model
    /// </summary>
    public class LoginModel : AdminModel
    {
        /// <summary>
        /// 預設构造函数
        /// </summary>
        /// <param name="appId"></param>
        public LoginModel(string? appId = null) : base(appId)
        {
            ImageLibUrl = DictHelper.RetrieveImagesLibUrl();
        }

        /// <summary>
        /// 验证码圖床地址
        /// </summary>
        public string ImageLibUrl { get; protected set; }

        /// <summary>
        /// 是否登录认证失败 為真时客户端弹出滑块验证码
        /// </summary>
        public bool AuthFailed { get; set; }
    }
}
