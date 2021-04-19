using Bootstrap.DataAccess;
using Bootstrap.Security;
using Longbow.Tasks;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Threading;
using Task = System.Threading.Tasks.Task;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 後台任務扩展方法
    /// </summary>
    internal static class TasksExtensions
    {
        /// <summary>
        /// 添加示例後台任務
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddBootstrapAdminBackgroundTask(this IServiceCollection services)
        {
            services.AddTaskServices(builder => builder.AddFileStorage());
            services.AddHostedService<BootstrapAdminBackgroundServices>();
            return services;
        }
    }

    /// <summary>
    /// 後台任務服务类
    /// </summary>
    internal class BootstrapAdminBackgroundServices : BackgroundService
    {
        /// <summary>
        /// 运行任務
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override Task ExecuteAsync(CancellationToken stoppingToken) => Task.Run(() =>
        {
            TaskServicesManager.GetOrAdd("单次任務", token => Task.Delay(1000));
            TaskServicesManager.GetOrAdd("周期任務", token => Task.Delay(1000), TriggerBuilder.Default.WithInterval(10000).Build());
            TaskServicesManager.GetOrAdd("Cron 任務", token => Task.Delay(1000), TriggerBuilder.Build(Cron.Secondly(5)));
            TaskServicesManager.GetOrAdd("超时任務", token => Task.Delay(2000), TriggerBuilder.Default.WithTimeout(1000).WithInterval(1000).WithRepeatCount(2).Build());

            // 本机調试时此處会抛出异常，配置文件中預設开启了任務持久化到物理文件，此處异常只有首次加载时会抛出
            // 此處异常是示例自定义任務内部未进行捕獲异常时任務仍然能继续运行，不会導致整個进程崩溃退出
            // 此處程式碼可注释掉
            //TaskServicesManager.GetOrAdd("故障任務", token => throw new Exception("故障任務"));
            TaskServicesManager.GetOrAdd("取消任務", token => Task.Delay(1000)).Triggers.First().Enabled = false;

            // 創建任務并禁用
            TaskServicesManager.GetOrAdd("禁用任務", token => Task.Delay(1000)).Status = SchedulerStatus.Disabled;

            // 真實任務负责批次写入資料執行脚本到日志中
            TaskServicesManager.GetOrAdd<DBLogTask>("SQL日志", TriggerBuilder.Build(Cron.Minutely()));

            // 真實人物负责周期性設置健康檢查结果开關為开启
            TaskServicesManager.GetOrAdd("健康檢查", token => Task.FromResult(DictHelper.SaveSettings(new BootstrapDict[] {
                new BootstrapDict() {
                    Category = "網站設置",
                    Name = "健康檢查",
                    Code = "1",
                    Define = 0
                }
            })), TriggerBuilder.Build(Cron.Minutely(10)));
        });
    }
}
