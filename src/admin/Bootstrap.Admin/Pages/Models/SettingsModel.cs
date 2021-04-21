using Bootstrap.Admin.Models;
using Bootstrap.DataAccess;
using Bootstrap.Security;
using System.Collections.Generic;

namespace Bootstrap.Admin.Pages.Models
{
    /// <summary>
    /// 導航模型
    /// </summary>
    public class SettingsModel : NavigatorBarModel
    {
        /// <summary>
        /// 構造函數 Blazor 使用
        /// </summary>
        public SettingsModel(string? userName) : base(userName)
        {
            Themes = DictHelper.RetrieveThemes();
            AutoLockScreen = EnableAutoLockScreen;
            DefaultApp = DictHelper.RetrieveDefaultApp();
        }

        /// <summary>
        /// 獲得 系統配置的所有樣式表
        /// </summary>
        public IEnumerable<BootstrapDict> Themes { get; }

        /// <summary>
        /// 獲得 是否開啟自動鎖屏
        /// </summary>
        public bool AutoLockScreen { get; }

        /// <summary>
        /// 獲得 是否開啟自動鎖屏
        /// </summary>
        public bool DefaultApp { get; }

    }
}
