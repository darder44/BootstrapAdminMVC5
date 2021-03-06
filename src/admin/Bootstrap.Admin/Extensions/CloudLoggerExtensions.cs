using Longbow.Logging;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 郵件日誌擴展方法
    /// </summary>
    public static class CloudLoggerExtensions
    {
        /// <summary>
        /// 注册郵件日誌方法
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static ILoggingBuilder AddCloudLogger(this ILoggingBuilder builder)
        {
            builder.Services.AddSingleton<IConfigureOptions<CloudLoggerOption>, LoggerProviderConfigureOptions<CloudLoggerOption, CloudLoggerProvider>>();
            builder.Services.AddSingleton<IOptionsChangeTokenSource<CloudLoggerOption>, LoggerProviderOptionsChangeTokenSource<CloudLoggerOption, CloudLoggerProvider>>();
            builder.Services.AddSingleton<ILoggerProvider, CloudLoggerProvider>();
            return builder;
        }
    }

    /// <summary>
    /// 云日誌提供類
    /// </summary>
    [ProviderAlias("Cloud")]
    public class CloudLoggerProvider : LoggerProvider
    {
        private readonly HttpClient httpClient;
        private readonly IDisposable optionsReloadToken;
        private CloudLoggerOption option;

        /// <summary>
        /// 構造函数
        /// </summary>
        public CloudLoggerProvider(IOptionsMonitor<CloudLoggerOption> options) : base(null, new Func<string, LogLevel, bool>((name, logLevel) => logLevel >= LogLevel.Error))
        {
            optionsReloadToken = options.OnChange(op => option = op);
            option = options.CurrentValue;

            httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(10)
            };
            httpClient.DefaultRequestHeaders.Connection.Add("keep-alive");

            LogCallback = new Action<string>(async message =>
            {
                if (!string.IsNullOrEmpty(option.Url))
                {
                    try { await httpClient.PostAsJsonAsync(option.Url, message).ConfigureAwait(false); }
                    catch { }
                }
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                httpClient.Dispose();
                optionsReloadToken.Dispose();
            }
        }
    }

    /// <summary>
    /// 云日誌配置類
    /// </summary>
    public class CloudLoggerOption
    {
        /// <summary>
        /// 獲得/設置 云日誌地址
        /// </summary>
        public string Url { get; set; } = "";
    }
}
