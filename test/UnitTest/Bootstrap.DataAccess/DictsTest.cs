using Bootstrap.Security;
using Bootstrap.Security.Mvc;
using Longbow.Web;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using Xunit;
using System.Net.Http.Json;
using System.Text;

namespace Bootstrap.DataAccess
{
    [Collection("Login")]
    public class DictsTest
    {
        [Fact]
        public void SaveAndDelete_Ok()
        {
            var dict = new BootstrapDict()
            {
                Category = "UnitTest",
                Name = "SaveDict",
                Code = "1",
                Define = 1
            };
            Assert.True(DictHelper.Save(dict));
            dict.Code = "2";
            Assert.True(DictHelper.Save(dict));
            Assert.True(DictHelper.Delete(new string[] { dict.Id }));
        }

        [Fact]
        public void SaveSettings_Ok()
        {
            var dict = new BootstrapDict()
            {
                Category = "UnitTest",
                Name = "SaveSettings",
                Code = "1",
                Define = 1
            };

            // insert
            Assert.True(DictHelper.Save(dict));
            // update
            Assert.True(DictHelper.SaveSettings(new BootstrapDict[] { dict }));
            // delete
            Assert.True(DictHelper.Delete(new string[] { dict.Id }));
        }

        [Fact]
        public void RetrieveCategories_Ok()
        {
            Assert.NotEmpty(DictHelper.RetrieveCategories());
        }

        [Fact]
        public void RetrieveWebIcon_Ok()
        {
            var url = DictHelper.RetrieveWebIcon("Demo");
            Assert.Equal("http://localhost:49185/favicon.ico", url);
        }

        [Fact]
        public void RetrieveWebLogo_Ok()
        {
            var url = DictHelper.RetrieveWebLogo("Demo");
            Assert.Equal("http://localhost:49185/favicon.png", url);
        }

        [Fact]
        public void RetrieveWebTitle_Ok()
        {
            Assert.Equal("後台管理系統", DictHelper.RetrieveWebTitle(BootstrapAppContext.AppId));
        }

        [Fact]
        public void RetrieveWebFooter_Ok()
        {
            Assert.Equal("2016 © 通用後台管理系統", DictHelper.RetrieveWebFooter(BootstrapAppContext.AppId));
        }

        [Fact]
        public void RetrieveThemes_Ok()
        {
            Assert.NotEmpty(DictHelper.RetrieveThemes());
        }

        [Fact]
        public void RetrieveActiveTheme_Ok()
        {
            Assert.Equal("blue.css", DictHelper.RetrieveActiveTheme());
        }

        [Fact]
        public void RetrieveIconFolderPath_Ok()
        {
            Assert.Equal("~/images/uploader/", DictHelper.RetrieveIconFolderPath());
        }

        [Fact]
        public void RetrieveApps_Ok()
        {
            Assert.NotEmpty(DictHelper.RetrieveApps());
        }

        [Fact]
        public void RetrieveDicts_Ok()
        {
            Assert.NotEmpty(DictHelper.RetrieveDicts());
        }

        [Fact]
        public void RetrieveCookieExpiresPeriod_Ok()
        {
            Assert.Equal(7, DictHelper.RetrieveCookieExpiresPeriod());
        }

        [Fact]
        public void RetrieveExceptionsLogPeriod_Ok()
        {
            Assert.Equal(1, DictHelper.RetrieveExceptionsLogPeriod());
        }

        [Fact]
        public void RetrieveLoginLogsPeriod_Ok()
        {
            Assert.Equal(12, DictHelper.RetrieveLoginLogsPeriod());
        }

        [Fact]
        public void RetrieveLogsPeriod_Ok()
        {
            Assert.Equal(12, DictHelper.RetrieveLogsPeriod());
        }

        [Fact]
        public void RetrieveLocaleIP_Ok()
        {
            var ipSvr = DictHelper.RetrieveLocaleIPSvr();
            Assert.Equal("None", ipSvr);

            var ipUri = DictHelper.RetrieveLocaleIPSvrUrl("JuheIPSvr");
            Assert.NotNull(ipUri);
        }


        [Fact]
        public void Test()
        {
            var payload = "{\"status\":1,\"message\":\"Internal Service Error: ip[207.148.111.94] loc failed\"}";
            var options = new JsonSerializerOptions().AddDefaultConverters();

            var state = JsonSerializer.Deserialize<BaiDuIPLocator>(payload, options);
            Assert.Equal(1, state.Status);
        }

        [Fact]
        public async void BaiduIPSvr_Ok()
        {
            var ipUri = DictHelper.RetrieveLocaleIPSvrUrl("BaiDuIPSvr");

            using var client = new HttpClient();
            // 日本东京
            var locator = await client.GetAsJsonAsync<BaiDuIPLocator>($"{ipUri}207.148.111.94");
            Assert.NotEqual(0, locator.Status);

            // 四川成都
            locator = await client.GetAsJsonAsync<BaiDuIPLocator>($"{ipUri}182.148.123.196");
            Assert.Equal(0, locator.Status);
        }


        [Fact]
        public void RetrieveAccessLogPeriod_Ok()
        {
            Assert.Equal(1, DictHelper.RetrieveAccessLogPeriod());
        }

        [Fact]
        public void IPSvrCachePeriod_Ok()
        {
            Assert.Equal(10, DictHelper.RetrieveLocaleIPSvrCachePeriod());
        }

        [Fact]
        public void RetrieveEnableBlazor_Ok()
        {
            Assert.False(DictHelper.RetrieveEnableBlazor());
        }

        [Fact]
        public void RetrieveHomeUrl_Ok()
        {
            Assert.Equal("~/Home/Index", DictHelper.RetrieveHomeUrl("Admin", ""));
            Assert.Equal("http://localhost:49185", DictHelper.RetrieveHomeUrl("Admin", "Demo"));
            Assert.Equal("~/Home/Index", DictHelper.RetrieveHomeUrl("Admin", "BA"));

            // 开啟預設程式功能
            DictHelper.SaveSettings(new BootstrapDict[] {
                new BootstrapDict()
                {
                    Category = "網站設置",
                    Name = "預設應用程式",
                    Code = "1"
                }
            });

            var defaultApp = DictHelper.RetrieveDefaultApp();
            Assert.True(defaultApp);
            DictHelper.RetrieveHomeUrl("Admin", "BA");

            // 關閉預設程式功能
            DictHelper.SaveSettings(new BootstrapDict[] {
                new BootstrapDict()
                {
                    Category = "網站設置",
                    Name = "預設應用程式",
                    Code = "0"
                }
            });
            Assert.Equal("~/Home/Index", DictHelper.RetrieveHomeUrl("Admin", "BA"));
        }

        #region Private Class For Test
        /// <summary>
        ///
        /// </summary>
        private class BaiDuIPLocator
        {
            /// <summary>
            /// 詳詳地址信息
            /// </summary>
            public string Address { get; set; }

            /// <summary>
            /// 结果状態返回碼
            /// </summary>
            public int Status { get; set; }

            /// <summary>
            ///
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return Status == 0 ? string.Join(" ", Address.SpanSplit("|").Skip(1).Take(2)) : "XX XX";
            }
        }

        private class JuheIPLocator
        {
            /// <summary>
            ///
            /// </summary>
            public string ResultCode { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string Reason { get; set; }

            /// <summary>
            ///
            /// </summary>
            public JuheIPLocatorResult Result { get; set; }

            /// <summary>
            ///
            /// </summary>
            /// <value></value>
            public int Error_Code { get; set; }

            /// <summary>
            ///
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return Error_Code != 0 ? "XX XX" : Result.ToString();
            }
        }

        private class JuheIPLocatorResult
        {
            /// <summary>
            ///
            /// </summary>
            public string Country { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string Province { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string City { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string Isp { get; set; }

            /// <summary>
            ///
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return Country != "中国" ? $"{Country} {Province} {Isp}" : $"{Province} {City} {Isp}";
            }
        }

        private class BaiduIP138Locator
        {
            /// <summary>
            ///
            /// </summary>
            public string Status { get; set; } = "";

            /// <summary>
            /// 獲得/設置 地理位置结果
            /// </summary>
            public IEnumerable<BaiDuIp138LocatorResult> Data { get; set; } = Array.Empty<BaiDuIp138LocatorResult>();
        }

        /// <summary>
        /// Ip138 地理位置结果实体類
        /// </summary>
        private class BaiDuIp138LocatorResult
        {
            /// <summary>
            /// 獲得/設置 地理位置信息
            /// </summary>
            public string Location { get; set; } = "";

            /// <summary>
            /// ToString 方法
            /// </summary>
            /// <returns></returns>
            public override string ToString() => Location;
        }

        #endregion
    }
}
