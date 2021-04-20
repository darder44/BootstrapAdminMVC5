using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Bootstrap.Admin.Models
{
    /// <summary>
    /// 任務管理頁面 Model 類
    /// </summary>
    public class TaskModel : NavigatorBarModel
    {
        /// <summary>
        /// 構造函数
        /// </summary>
        /// <param name="controller"></param>
        public TaskModel(ControllerBase controller) : base(controller)
        {
            // 此處為演示程式碼，具體生产环境可以从資料庫配置獲得
            // Key 為任務名稱 Value 為任務執行體 FullName
            TaskExecutors = new Dictionary<string, string>
            {
                { "測試任務", "Bootstrap.Admin.DefaultTaskExecutor" }
            };

            TaskTriggers = new Dictionary<string, string>
            {
                { "每 5 秒钟執行一次", Longbow.Tasks.Cron.Secondly(5) },
                { "每 1 分钟執行一次", Longbow.Tasks.Cron.Minutely(1) },
                { "每 5 分钟執行一次", Longbow.Tasks.Cron.Minutely(5) }
            };
        }

        /// <summary>
        /// 獲得 系統内置的所有任務
        /// </summary>
        public IDictionary<string, string> TaskExecutors { get; }

        /// <summary>
        /// 獲得 系統内置触發器集合
        /// </summary>
        /// <value></value>
        public IDictionary<string, string> TaskTriggers { get; }
    }
}
