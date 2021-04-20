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
        /// 驗證碼圖床地址
        /// </summary>
        public string ImageLibUrl { get; protected set; }

        /// <summary>
        /// 是否登入認證失败 為真時客户端彈出滑块驗證碼
        /// </summary>
        public bool AuthFailed { get; set; }
    }
}
