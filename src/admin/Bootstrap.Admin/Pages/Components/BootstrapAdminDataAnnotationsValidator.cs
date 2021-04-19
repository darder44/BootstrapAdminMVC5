using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;

namespace Bootstrap.Admin.Pages.Components
{
    /// <summary>
    /// BootstrapAdminDataAnnotationsValidator 验证組件
    /// </summary>
    public class BootstrapAdminDataAnnotationsValidator : ComponentBase
    {
        /// <summary>
        /// 獲得/設置 當前编辑資料上下文
        /// </summary>
        [CascadingParameter]
        EditContext? CurrentEditContext { get; set; }

        /// <summary>
        /// 獲得/設置 當前编辑窗體上下文
        /// </summary>
        [CascadingParameter]
        public LgbEditFormBase? EditForm { get; set; }

        /// <summary>
        /// 初始化方法
        /// </summary>
        protected override void OnInitialized()
        {
            if (CurrentEditContext == null)
            {
                throw new InvalidOperationException($"{nameof(DataAnnotationsValidator)} requires a cascading parameter of type {nameof(EditContext)}. For example, you can use {nameof(DataAnnotationsValidator)} inside an EditForm.");
            }

            if (EditForm == null)
            {
                throw new InvalidOperationException($"{nameof(DataAnnotationsValidator)} requires a cascading parameter of type {nameof(LgbEditFormBase)}. For example, you can use {nameof(BootstrapAdminDataAnnotationsValidator)} inside an EditForm.");
            }

            CurrentEditContext.AddBootstrapAdminDataAnnotationsValidation(EditForm);
        }
    }
}
