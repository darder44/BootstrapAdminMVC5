using Bootstrap.DataAccess;
using Bootstrap.Security.Mvc;

namespace Bootstrap.Admin.Models
{
    /// <summary>
    /// AdminModel 實體類
    /// </summary>
    public class AdminModel : ModelBase
    {
        /// <summary>
        /// 預設構造函數
        /// </summary>
        /// <param name="appId"></param>
        public AdminModel(string? appId = null)
        {
            if (string.IsNullOrEmpty(appId)) appId = BootstrapAppContext.AppId;

            Title = DictHelper.RetrieveWebTitle(appId);
            Footer = DictHelper.RetrieveWebFooter(appId);
            Theme = DictHelper.RetrieveActiveTheme();
            IsDemo = DictHelper.RetrieveSystemModel();
            ShowCardTitle = DictHelper.RetrieveCardTitleStatus();
            ShowSideBar = DictHelper.RetrieveSidebarStatus();
            AllowMobile = DictHelper.RetrieveMobileLogin();
            AllowOAuth = DictHelper.RetrieveOAuthLogin();
            ShowMobile = AllowMobile;
            ShowOAuth = AllowOAuth;
            LockScreenPeriod = DictHelper.RetrieveAutoLockScreenPeriod();
            EnableAutoLockScreen = DictHelper.RetrieveAutoLockScreen();
            FixedTableHeader = DictHelper.RetrieveFixedTableHeader();
        }

        /// <summary>
        /// 獲取 網站標題
        /// </summary>
        public string Title { get; protected set; }

        /// <summary>
        /// 獲取 網站頁腳
        /// </summary>
        public string Footer { get; protected set; }

        /// <summary>
        /// 網站樣式全局設置
        /// </summary>
        public string Theme { get; protected set; }

        /// <summary>
        /// 是否為Demo系統
        /// </summary>
        public bool IsDemo { get; protected set; }

        /// <summary>
        /// 是否顯示卡片標題
        /// </summary>
        public bool ShowCardTitle { get; protected set; }

        /// <summary>
        /// 是否收縮側邊欄
        /// </summary>
        public bool ShowSideBar { get; protected set; }

        /// <summary>
        /// 獲得 是否允許簡訊驗證碼登入
        /// </summary>
        public bool AllowMobile { get; }

        /// <summary>
        /// 獲得 是否允許第三方 OAuth 認證登入
        /// </summary>
        public bool AllowOAuth { get; }

        /// <summary>
        /// 獲得 是否允許簡訊驗證碼登入
        /// </summary>
        public bool ShowMobile { get; }

        /// <summary>
        /// 獲得 是否允許第三方 OAuth 認證登入
        /// </summary>
        public bool ShowOAuth { get; }

        /// <summary>
        /// 獲得 自動鎖屏時長 預設 1 分钟 字典表中配置
        /// </summary>
        public int LockScreenPeriod { get; }

        /// <summary>
        /// 獲得 自動鎖屏功能是否自動開啟 預設關閉
        /// </summary>
        public bool EnableAutoLockScreen { get; }

        /// <summary>
        /// 獲得 是否固定表頭
        /// </summary>
        public bool FixedTableHeader { get; }
    }
}
