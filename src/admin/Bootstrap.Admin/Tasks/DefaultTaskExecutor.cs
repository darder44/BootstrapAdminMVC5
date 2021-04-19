using System.Threading;
using System.Threading.Tasks;
using Longbow.Tasks;

namespace Bootstrap.Admin
{
    /// <summary>
    /// 預設任務執行體實體類
    /// </summary>
    public class DefaultTaskExecutor : ITask
    {
        /// <summary>
        /// 任務執行方法
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task Execute(CancellationToken cancellationToken) => Task.Delay(1000, cancellationToken);
    }
}
