using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Bootstrap.Admin.Controllers
{
    public class HealthCheckTest : ControllerTest
    {
        public HealthCheckTest(BALoginWebHost factory) : base(factory, "") { }

        [Fact]
        public async void View_Ok()
        {
            var content = await Client.GetStringAsync("/Healths");
            Assert.Contains("TotalDuration", content);
        }

        [Fact]
        public async void UI_Ok()
        {
            var content = await Client.GetStringAsync("/Healths-ui");
            Assert.Contains("健康检查", content);
        }
    }

    [Collection("BA-Logout")]
    public class HealthCheckError
    {
        protected HttpClient Client { get; set; }

        protected IServiceProvider ServiceProvider { get; set; }

        public HealthCheckError(BAWebHost factory)
        {
            Client = factory.CreateClient("/Account/Logout");
            ServiceProvider = factory.Services;
        }

    }
}
