﻿using Bootstrap.Admin.Models;
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
        /// 构造函数 Blazor 使用
        /// </summary>
        public SettingsModel(string? userName) : base(userName)
        {
            Themes = DictHelper.RetrieveThemes();
            AutoLockScreen = EnableAutoLockScreen;
            DefaultApp = DictHelper.RetrieveDefaultApp();
        }

        /// <summary>
        /// 獲得 系统配置的所有样式表
        /// </summary>
        public IEnumerable<BootstrapDict> Themes { get; }

        /// <summary>
        /// 獲得 是否开启自動锁屏
        /// </summary>
        public bool AutoLockScreen { get; }

        /// <summary>
        /// 獲得 是否开启自動锁屏
        /// </summary>
        public bool DefaultApp { get; }

    }
}