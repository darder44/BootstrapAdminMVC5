namespace Bootstrap.Admin.Pages.Components
{
    /// <summary>
    /// CheckBox 組件狀態枚举值
    /// </summary>
    public enum CheckBoxState
    {
        /// <summary>
        /// 未選中
        /// </summary>
        UnChecked,
        /// <summary>
        /// 選中
        /// </summary>
        Checked,
        /// <summary>
        /// 混合模式
        /// </summary>
        Mixed
    }

    /// <summary>
    /// 
    /// </summary>
    public static class CheckBoxStateExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static string ToCss(this CheckBoxState state)
        {
            var ret = "false";
            switch (state)
            {
                case CheckBoxState.Checked:
                    ret = "true";
                    break;
                case CheckBoxState.Mixed:
                    ret = "mixed";
                    break;
                case CheckBoxState.UnChecked:
                    break;
            }
            return ret;
        }
    }
}
