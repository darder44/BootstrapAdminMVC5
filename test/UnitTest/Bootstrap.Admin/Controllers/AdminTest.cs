using System.Net;
using Xunit;

namespace Bootstrap.Admin.Controllers
{
    public class AdminTest : ControllerTest
    {
        public AdminTest(BALoginWebHost factory) : base(factory, "Admin") { }

        [Theory]
        [InlineData("Index", "歡迎使用後台管理")]
        [InlineData("Users", "用户管理")]
        [InlineData("Groups", "部门管理")]
        [InlineData("Dicts", "字典表维护")]
        [InlineData("Roles", "角色管理")]
        [InlineData("Menus", "選單管理")]
        [InlineData("Logs", "操作日誌")]
        [InlineData("Traces", "訪問日誌")]
        [InlineData("Logins", "登入日誌")]
        [InlineData("FAIcon", "圖標集")]
        [InlineData("Sidebar", "後台管理")]
        [InlineData("IconView", "圖標分類")]
        [InlineData("Settings", "網站設置")]
        [InlineData("Notifications", "通知管理")]
        [InlineData("Profiles", "個人中心")]
        [InlineData("Exceptions", "程式異常")]
        [InlineData("Healths", "健康检查")]
        [InlineData("Messages", "站内訊息")]
        [InlineData("Online", "線上用户")]
        [InlineData("Tasks", "任務管理")]
        [InlineData("Mobile", "客户端測試")]
        [InlineData("Analyse", "網站分析")]
        [InlineData("SQL", "SQL日誌")]
        public async void View_Ok(string view, string text)
        {
            var r = await Client.GetAsync(view);
            Assert.True(r.IsSuccessStatusCode);
            var content = await r.Content.ReadAsStringAsync();
            Assert.Contains(text, content);
        }

        [Fact]
        public async void Admin_Error_Ok()
        {
            var r = await Client.GetAsync("Error");
            Assert.False(r.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.InternalServerError, r.StatusCode);
        }
    }
}
