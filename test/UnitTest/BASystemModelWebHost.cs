using System.Threading.Tasks;
using Xunit;

namespace Bootstrap.Admin
{
    /// <summary>
    /// 演示系統
    /// </summary>
    [CollectionDefinition("SystemModel")]
    public class BootstrapAdminDemoContext : ICollectionFixture<BASystemModelWebHost>
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public class BASystemModelWebHost : BALoginWebHost
    {
        public BASystemModelWebHost() : base()
        {
            // 設置系統為演示模式
            using var db = Longbow.Data.DbManager.Create();
            db.Execute("Update Dicts Set Code = @2 where Category = @0 and Name = @1", "網站設置", "演示系統", "1");

            do
            {
                var task = Task.Delay(500);
                task.Wait();
                var dict = DataAccess.DictHelper.RetrieveSystemModel();
                if (dict) break;
            }
            while (true);
        }

        protected override void Dispose(bool disposing)
        {
            using var db = Longbow.Data.DbManager.Create();
            db.Execute("Update Dicts Set Code = @2 where Category = @0 and Name = @1", "網站設置", "演示系統", "0");

            do
            {
                var task = Task.Delay(500);
                task.Wait();
                var dict = DataAccess.DictHelper.RetrieveSystemModel();
                if (!dict) break;
            }
            while (true);
        }
    }
}
