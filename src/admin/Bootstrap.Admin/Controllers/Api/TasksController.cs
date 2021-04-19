using Bootstrap.DataAccess;
using Longbow;
using Longbow.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bootstrap.Admin.Controllers.Api
{
    /// <summary>
    /// 任務管理控制器
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TasksController : ControllerBase
    {
        /// <summary>
        /// 獲取所有任務資料
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<object> Get()
        {
            return TaskServicesManager.ToList().Select(s => new { s.Name, Status = s.Status.ToString(), s.LastRuntime, s.CreatedTime, s.NextRuntime, LastRunResult = s.Triggers.First().LastResult.ToString(), TriggerExpression = s.Triggers.First().ToString() }).OrderBy(s => s.Name);
        }

        /// <summary>
        /// 任務相關操作
        /// </summary>
        /// <param name="scheName">調度名稱</param>
        /// <param name="operType">操作方式 pause run</param>
        [HttpPut("{scheName}")]
        public bool Put(string scheName, [FromQuery]string operType)
        {
            var sche = TaskServicesManager.Get(scheName);
            if (sche != null) sche.Status = operType == "pause" ? SchedulerStatus.Disabled : SchedulerStatus.Running;

            // SQL 日志任務特殊處理
            if (scheName == "SQL日志")
            {
                if (operType == "pause") DBLogTask.Pause();
                else DBLogTask.Run();
            }
            return true;
        }

        /// <summary>
        /// 保存任務方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public bool Post([FromBody]TaskWidget widget)
        {
            // 判斷 Cron 表达式
            if (string.IsNullOrEmpty(widget.CronExpression)) return false;

            // 系统内置任務禁止更改
            // 演示模式下禁止删除内置任務
            if (DictHelper.RetrieveSystemModel() && _tasks.Any(t => t.Equals(widget.Name, StringComparison.OrdinalIgnoreCase))) return false;

            // 加载任務執行體
            // 此處可以扩展為任意 DLL 中的任意继承 ITask 接口的實體類
            var taskExecutor = LgbActivator.CreateInstance<ITask>("Bootstrap.Admin", widget.TaskExecutorName);
            if (taskExecutor == null) return false;

            // 此處未存储到資料庫中，直接送入任務中心
            TaskServicesManager.Remove(widget.Name);
            TaskServicesManager.GetOrAdd(widget.Name, token => taskExecutor.Execute(token), TriggerBuilder.Build(widget.CronExpression));
            return true;
        }

        private static IEnumerable<string> _tasks = new string[] {
            "单次任務",
            "周期任務",
            "Cron 任務",
            "超时任務",
            "取消任務",
            "禁用任務",
            "SQL日志",
            "健康檢查"
        };
        /// <summary>
        /// 删除任務方法
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpDelete]
        public bool Delete([FromBody]IEnumerable<string> ids)
        {
            // 演示模式下禁止删除内置任務
            if (DictHelper.RetrieveSystemModel() && _tasks.Any(t => ids.Any(id => id.Equals(t, StringComparison.OrdinalIgnoreCase)))) return false;

            // 循环删除任務
            ids.ToList().ForEach(id => TaskServicesManager.Remove(id));
            return true;
        }
    }
}
