using System.Collections.Generic;

namespace Bootstrap.Admin.Pages.Components
{
    /// <summary>
    /// ITableHeader 接口
    /// </summary>
    public interface ITableHeader
    {
        /// <summary>
        /// 獲取綁定字串顯示名稱方法
        /// </summary>
        IReadOnlyDictionary<string, object> AdditionalAttributes { get; set; }

        /// <summary>
        /// 獲取綁定字串顯示名稱方法
        /// </summary>
        string GetDisplayName();

        /// <summary>
        /// 獲取綁定字串信息方法
        /// </summary>
        string GetFieldName();

        /// <summary>
        /// 獲得/設置 是否允許排序 預設為 false
        /// </summary>
        bool Sort { get; set; }
    }
}
