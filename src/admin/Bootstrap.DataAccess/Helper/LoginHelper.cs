using Longbow.Web;
using Longbow.Web.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Net;

namespace Bootstrap.DataAccess
{
    /// <summary>
    /// 用户登錄操作類別
    /// </summary>
    public static class LoginHelper
    {
        /// <summary>
        /// 記錄登錄日誌方法
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userName">登錄用户名</param>
        /// <param name="auth">是否登錄成功</param>
        /// <returns></returns>
        public static bool Log(this HttpContext context, string userName, bool auth)
        {
            var ipLocator = context.RequestServices.GetRequiredService<IIPLocatorProvider>();
            var ip = context.Connection.RemoteIpAddress?.ToIPv4String() ?? "";
            var userAgent = context.Request.Headers["User-Agent"];
            var agent = new UserAgent(userAgent);

            if (string.IsNullOrEmpty(userName)) userName = ip;
            var loginUser = new LoginUser
            {
                UserName = userName,
                LoginTime = DateTime.Now,
                UserAgent = userAgent,
                Ip = ip,
                City = ipLocator.Locate(ip),
                Browser = $"{agent.Browser?.Name} {agent.Browser?.Version}",
                OS = $"{agent.OS?.Name} {agent.OS?.Version}",
                Result = auth ? "登錄成功" : "登錄失敗"
            };
            return DbContextManager.Create<LoginUser>()?.Log(loginUser) ?? false;
        }

        /// <summary>
        /// 查詢指定頁码登錄日誌
        /// </summary>
        /// <param name="po"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="ip"></param>
        public static Page<LoginUser> RetrievePages(PaginationOption po, DateTime? startTime, DateTime? endTime, string? ip) => DbContextManager.Create<LoginUser>()?.RetrieveByPages(po, startTime, endTime, ip) ?? new Page<LoginUser>() { Items = new List<LoginUser>() };

        /// <summary>
        /// 查詢所有登錄日誌
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static IEnumerable<LoginUser> RetrieveAll(DateTime? startTime, DateTime? endTime, string? ip)
        {
            return DbContextManager.Create<LoginUser>()?.RetrieveAll(startTime, endTime, ip) ?? new LoginUser[0];
        }
    }
}
