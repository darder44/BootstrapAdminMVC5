using Microsoft.AspNetCore.Components;

namespace Bootstrap.Admin.Pages.Components
{
    /// <summary>
    /// 弹窗組件基類
    /// </summary>
    public class AlertBase : ModalBase
    {
        /// <summary>
        ///
        /// </summary>
        [Parameter]
        public RenderFragment? AlertBody { get; set; }

        /// <summary>
        /// 獲得/設置 弹窗 Footer 程式碼块
        /// </summary>
        [Parameter]
        public RenderFragment? AlertFooter { get; set; }

        /// <summary>
        /// 獲得/設置 是否自動關閉 預設為 true
        /// </summary>
        [Parameter]
        public bool AutoClose { get; set; } = true;

        /// <summary>
        /// 獲得/設置 自動關閉時長 預設 1500 毫秒
        /// </summary>
        [Parameter]
        public int Interval { get; set; } = 1500;

        /// <summary>
        /// 控件渲染完畢後回調方法
        /// </summary>
        /// <param name="firstRender"></param>
        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (_show)
            {
                _show = false;
                Toggle();
            }
        }

        private bool _show;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        public void Show(string title)
        {
            Title = title;
            _show = true;
            StateHasChanged();
        }
    }
}
