using Bootstrap.Admin.Pages.Components;
using Bootstrap.Admin.Pages.Models;
using Bootstrap.Admin.Pages.Shared;
using Bootstrap.DataAccess;
using Bootstrap.Security;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Bootstrap.Admin.Pages.Views.Admin.Components
{
    /// <summary>
    /// 個人中心組件類
    /// </summary>
    public class ProfilesBase : PageBase
    {
        /// <summary>
        /// 獲得/設置 RootLayout 實例
        /// </summary>
        [CascadingParameter(Name = "Default")]
        public DefaultLayout? RootLayout { get; protected set; }

        /// <summary>
        /// 獲得/設置 ProfilesModel 實例
        /// </summary>
        protected ProfilesModel? Model { get; set; }

        /// <summary>
        /// 獲得/設置 是否為演示系統
        /// </summary>
        protected bool IsDemo { get; set; } = false;

        /// <summary>
        /// 獲得/設置 BootstrapUser 實例
        /// </summary>
        protected BootstrapUser User { get; set; } = new BootstrapUser();

        /// <summary>
        /// 獲得/設置  PasswModel 實例
        /// </summary>
        protected PasswordModel Password { get; set; } = new PasswordModel();

        /// <summary>
        /// 獲得/設置  當前用户顯示名稱
        /// </summary>
        [DisplayName("顯示名稱")]
        public string DisplayName { get; set; } = "";

        /// <summary>
        /// IJSRuntime 接口實例
        /// </summary>
        [Inject]
        protected IJSRuntime? JSRuntime { get; set; }

        /// <summary>
        /// 獲得/設置 选中的樣式項
        /// </summary>
        protected SelectedItem SelectedTheme { get; set; } = new SelectedItem();

        /// <summary>
        /// 獲得/設置 选中的應用程式
        /// </summary>
        protected SelectedItem SelectedApp { get; set; } = new SelectedItem();

        /// <summary>
        /// 獲得/設置 應用程式集合
        /// </summary>
        protected IEnumerable<SelectedItem> Apps { get; set; } = new SelectedItem[0];

        /// <summary>
        /// 獲得/設置 網站樣式集合
        /// </summary>
        protected IEnumerable<SelectedItem> Themes { get; set; } = new SelectedItem[0];

        /// <summary>
        /// 顯示提示信息
        /// </summary>
        /// <param name="text"></param>
        /// <param name="ret"></param>
        protected void ShowMessage(string text, bool ret = true) => JSRuntime?.ShowToast("個人中心", text, ret ? ToastCategory.Success : ToastCategory.Error);

        /// <summary>
        /// 組件初始化方法
        /// </summary>
        protected override void OnInitialized()
        {
            Model = new ProfilesModel(RootLayout?.UserName);
            Themes = new SelectedItem[] { new SelectedItem() { Text = "預設樣式" } }.Union(Model.Themes.Select(t => new SelectedItem() { Text = t.Name, Value = t.Code }));
            Apps = Model.Applications.Select(t => new SelectedItem() { Text = t.Value, Value = t.Key });

            SelectedTheme = Themes.First();
            SelectedApp = Apps.First();

            var user = UserHelper.RetrieveUserByUserName(Model?.UserName);
            if (user != null) User = user;

            // 直接绑定 User.DisplayName 導致未保存時 UI 的顯示名稱也會變化
            DisplayName = User.DisplayName;
        }

        /// <summary>
        /// 保存顯示名稱方法
        /// </summary>
        protected void SaveDisplayName(EditContext context)
        {
            if (!string.IsNullOrEmpty(User.UserName))
            {
                var ret = UserHelper.SaveDisplayName(User.UserName, DisplayName);
                if (ret)
                {
                    User.DisplayName = DisplayName;
                    RootLayout?.OnDisplayNameChanged(DisplayName);
                }

                // 彈窗提示是否保存成功
                var result = ret ? "成功" : "失败";
                ShowMessage($"保存顯示名稱{result}", ret);
            }
        }

        /// <summary>
        /// 保存密碼方法
        /// </summary>
        protected void SavePassword(EditContext context)
        {
            var ret = UserHelper.ChangePassword(User.UserName, Password.Password, Password.NewPassword);

            // 彈窗提示是否保存成功
            var result = ret ? "成功" : "失败";
            ShowMessage($"更新密碼{result}", ret);
        }

        /// <summary>
        /// 保存預設應用方法
        /// </summary>
        protected void SaveApp()
        {
            var ret = UserHelper.SaveApp(User.UserName, SelectedApp.Value);

            // 彈窗提示是否保存成功
            var result = ret ? "成功" : "失败";
            ShowMessage($"保存預設應用{result}", ret);
        }

        /// <summary>
        /// 保存網站樣式方法
        /// </summary>
        protected void SaveTheme()
        {
            var ret = UserHelper.SaveUserCssByName(User.UserName, SelectedTheme.Value);

            // 彈窗提示是否保存成功
            var result = ret ? "成功" : "失败";
            ShowMessage($"保存網站樣式{result}", ret);
        }

        /// <summary>
        /// 密碼保存實體類
        /// </summary>
        protected class PasswordModel
        {
            /// <summary>
            /// 獲得/設置 原密碼
            /// </summary>
            [DisplayName("原密碼")]
            public string Password { get; set; } = "";

            /// <summary>
            /// 獲得/設置 新密碼
            /// </summary>
            [DisplayName("新密碼")]
            public string NewPassword { get; set; } = "";

            /// <summary>
            /// 獲得/設置 確認密碼
            /// </summary>
            [DisplayName("確認密碼")]
            public string ConfirmPassword { get; set; } = "";
        }
    }
}
