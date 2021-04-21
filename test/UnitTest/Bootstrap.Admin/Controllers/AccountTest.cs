using Bootstrap.DataAccess;
using System.Linq;
using System.Net;
using Xunit;

namespace Bootstrap.Admin.Controllers
{
    public class AccountTest : ControllerTest
    {
        public AccountTest(BALoginWebHost factory) : base(factory, "Account") { }

        [Fact]
        public async void SystemMode_Test()
        {
            var dict = DictHelper.RetrieveDicts().FirstOrDefault(d => d.Category == "網站設置" && d.Name == "Demo系統");
            dict.Code = "1";
            DictHelper.Save(dict);

            var r = await Client.GetAsync("Login");

            // 恢復保护模式
            var db = DbManager.Create();
            db.Execute("Update Dicts Set Code = @0 Where Id = @1", "0", dict.Id);
            Assert.Equal(HttpStatusCode.OK, r.StatusCode);
            var source = await r.Content.ReadAsStringAsync();
            Assert.Contains("Demo系統", source);
        }

        [Fact]
        public async void AccessDenied_Ok()
        {
            // logout
            var r = await Client.GetAsync("AccessDenied");
            Assert.True(r.IsSuccessStatusCode);
            var content = await r.Content.ReadAsStringAsync();
            Assert.Contains("伺服器拒绝處理您的請求", content);
        }
    }
}
