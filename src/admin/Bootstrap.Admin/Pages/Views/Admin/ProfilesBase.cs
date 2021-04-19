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
    /// 個人中心組件类
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
        /// 獲得/設置 是否為演示系统
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
        /// 獲得/設置  當前用户显示名稱
        /// </summary>
        [DisplayName("显示名稱")]
        public string DisplayName { get; set; } = "";

        /// <summary>
        /// IJSRuntime 接口實例
        /// </summary>
        [Inject]
        protected IJSRuntime? JSRuntime { get; set; }

        /// <summary>
        /// 獲得/設置 选中的样式项
        /// </summary>
        protected SelectedItem SelectedTheme { get; set; } = new SelectedItem();

        /// <summary>
        /// 獲得/設置 选中的應用程序
        /// </summary>
        protected SelectedItem SelectedApp { get; set; } = new SelectedItem();

        /// <summary>
        /// 獲得/設置 應用程序集合
        /// </summary>
        protected IEnumerable<SelectedItem> Apps { get; set; } = new SelectedItem[0];

        /// <summary>
        /// 獲得/設置 網站样式集合
        /// </summary>
        protected IEnumerable<SelectedItem> Themes { get; set; } = new SelectedItem[0];

        /// <summary>
        /// 显示提示信息
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
            Themes = new SelectedItem[] { new SelectedItem() { Text = "預設样式" } }.Union(Model.Themes.Select(t => new SelectedItem() { Text = t.Name, Value = t.Code }));
            Apps = Model.Applications.Select(t => new SelectedItem() { Text = t.Value, Value = t.Key });

            SelectedTheme = Themes.First();
            SelectedApp = Apps.First();

            var user = UserHelper.RetrieveUserByUserName(Model?.UserName);
            if (user != null) User = user;

            // 直接绑定 User.DisplayName 導致未保存时 UI 的显示名稱也会变化
            DisplayName = User.DisplayName;
        }

        /// <summary>
        /// 保存显示名稱方法
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

                // 弹窗提示是否保存成功
                var result = ret ? "成功" : "失败";
                ShowMessage($"保存显示名稱{result}", ret);
            }
        }

        /// <summary>
        /// 保存密码方法
        /// </summary>
        protected void SavePassword(EditContext context)
        {
            var ret = UserHelper.ChangePassword(User.UserName, Password.Password, Password.NewPassword);

            // 弹窗提示是否保存成功
            var result = ret ? "成功" : "失败";
            ShowMessage($"更新密码{result}", ret);
        }

        /// <summary>
        /// 保存預設應用方法
        /// </summary>
        protected void SaveApp()
        {
            var ret = UserHelper.SaveApp(User.UserName, SelectedApp.Value);

            // 弹窗提示是否保存成功
            var result = ret ? "成功" : "失败";
            ShowMessage($"保存預設應用{result}", ret);
        }

        /// <summary>
        /// 保存網站样式方法
        /// </summary>
        protected void SaveTheme()
        {
            var ret = UserHelper.SaveUserCssByName(User.UserName, SelectedTheme.Value);

            // 弹窗提示是否保存成功
            var result = ret ? "成功" : "失败";
            ShowMessage($"保存網站样式{result}", ret);
        }

        /// <summary>
        /// 密码保存實體类
        /// </summary>
        protected class PasswordModel
        {
            /// <summary>
            /// 獲得/設置 原密码
            /// </summary>
            [DisplayName("原密码")]
            public string Password { get; set; } = "";

            /// <summary>
            /// 獲得/設置 新密码
            /// </summary>
            [DisplayName("新密码")]
            public string NewPassword { get; set; } = "";

            /// <summary>
            /// 獲得/設置 確认密码
            /// </summary>
            [DisplayName("確认密码")]
            public string ConfirmPassword { get; set; } = "";
        }
    }
}
