using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace Bootstrap.Admin.Pages.Components
{
    /// <summary>
    /// 模態框組件類
    /// </summary>
    public class ModalBase : ComponentBase
    {
        /// <summary>
        /// 獲得/設置 IJSRuntime 實例
        /// </summary>
        [Inject]
        protected IJSRuntime? JSRuntime { get; set; }

        /// <summary>
        /// 獲得/設置 ModalBody 程式碼塊
        /// </summary>
        [Parameter]
        public RenderFragment? ModalBody { get; set; }

        /// <summary>
        /// 獲得/設置 彈窗 Footer 程式碼塊
        /// </summary>
        [Parameter]
        public RenderFragment? ModalFooter { get; set; }

        /// <summary>
        /// 獲得/設置 Id
        /// </summary>
        [Parameter]
        public string Id { get; set; } = "";

        /// <summary>
        /// 獲得/設置 彈窗標題
        /// </summary>
        [Parameter]
        public string Title { get; set; } = "未設置";

        /// <summary>
        /// 獲得/設置 是否允許点击後台關閉彈窗 預設為 false
        /// </summary>
        [Parameter]
        public bool Backdrop { get; set; }

        /// <summary>
        /// 獲得/設置 彈窗大小
        /// </summary>
        [Parameter]
        public ModalSize Size { get; set; }

        /// <summary>
        /// 獲得/設置 是否垂直居中 預設為 true
        /// </summary>
        [Parameter]
        public bool IsCentered { get; set; } = true;

        /// <summary>
        /// 獲得/設置 是否顯示 Footer 預設為 true
        /// </summary>
        [Parameter]
        public bool ShowFooter { get; set; } = true;

        /// <summary>
        /// SetParametersAsync 方法
        /// </summary>
        public override Task SetParametersAsync(ParameterView parameters)
        {
            parameters.SetParameterProperties(this);
            if (string.IsNullOrEmpty(Id)) throw new InvalidOperationException("Modal Component Id property must be set");
            return base.SetParametersAsync(ParameterView.Empty);
        }

        /// <summary>
        /// 輸出窗口大小樣式
        /// </summary>
        /// <returns></returns>
        protected string RenderModalSize()
        {
            var ret = "";
            switch (Size)
            {
                case ModalSize.Default:
                    break;
                case ModalSize.Small:
                    ret = "modal-sm";
                    break;
                case ModalSize.Large:
                    ret = "modal-lg";
                    break;
                case ModalSize.ExtraLarge:
                    ret = "modal-xl";
                    break;
            }
            return ret;
        }

        /// <summary>
        /// Toggle 彈窗方法
        /// </summary>
        public void Toggle()
        {
            JSRuntime.ToggleModal($"#{Id}");
        }
    }

    /// <summary>
    /// 彈窗大小
    /// </summary>
    public enum ModalSize
    {
        /// <summary>
        /// 預設大小
        /// </summary>
        Default,
        /// <summary>
        /// 小窗口
        /// </summary>
        Small,
        /// <summary>
        /// 大窗口
        /// </summary>
        Large,
        /// <summary>
        /// 超大窗口
        /// </summary>
        ExtraLarge,
    }
}
