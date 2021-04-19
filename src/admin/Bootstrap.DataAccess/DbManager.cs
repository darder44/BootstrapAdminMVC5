﻿using Longbow.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetaPoco;
using System;
using System.Collections.Specialized;

namespace Bootstrap.DataAccess
{
    /// <summary>
    /// 資料庫物件管理類別
    /// </summary>
    public static class DbManager
    {
        /// <summary>
        /// 創建 IDatabase 實例方法
        /// </summary>
        /// <param name="connectionName"></param>
        /// <param name="keepAlive"></param>
        /// <param name="enableLog">是否記錄日誌</param>
        /// <returns></returns>
        public static IDatabase Create(string? connectionName = null, bool keepAlive = false, bool enableLog = true)
        {
            if (Mappers.GetMapper(typeof(Exceptions), null) == null) Mappers.Register(typeof(Exceptions).Assembly, new BootstrapDataAccessConventionMapper());
            var db = Longbow.Data.DbManager.Create(connectionName, keepAlive);
            db.ExceptionThrown += (sender, args) => args.Exception.Log(new NameValueCollection() { ["LastCmd"] = db.LastCommand });
            if (enableLog)
            {
                db.OnCommandExecuted(async provider =>
                {
                    var context = provider.GetRequiredService<IHttpContextAccessor>();
                    var userName = context.HttpContext?.User.Identity?.Name;
                    var log = new DBLog()
                    {
                        LogTime = DateTime.Now,
                        SQL = db.LastCommand,
                        UserName = userName
                    };
                    await DBLogTask.AddDBLog(log).ConfigureAwait(false);
                });
            }
            return db;
        }
    }
}
