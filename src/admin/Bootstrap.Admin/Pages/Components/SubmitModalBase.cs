using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Bootstrap.Admin.Pages.Components
{
    /// <summary>
    /// 資料綁定提交弹窗組件
    /// </summary>
    public class SubmitModalBase<TItem> : ModalBase
    {
#nullable disable
        /// <summary>
        /// 獲得/設置 弹窗綁定資料實體
        /// </summary>
        [Parameter]
        public TItem Model { get; set; }
#nullable restore

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public EventCallback<TItem> ModelChanged { get; set; }

        /// <summary>
        /// A callback that will be invoked when the form is submitted.
        /// If using this parameter, you are responsible for triggering any validation
        /// manually, e.g., by calling <see cref="EditContext.Validate"/>.
        /// </summary>
        [Parameter] public EventCallback<EditContext> OnSubmit { get; set; }

        /// <summary>
        /// A callback that will be invoked when the form is submitted and the
        /// <see cref="EditContext"/> is determined to be valid.
        /// </summary>
        [Parameter] public EventCallback<EditContext> OnValidSubmit { get; set; }

        /// <summary>
        /// A callback that will be invoked when the form is submitted and the
        /// <see cref="EditContext"/> is determined to be invalid.
        /// </summary>
        [Parameter] public EventCallback<EditContext> OnInvalidSubmit { get; set; }
    }
}
