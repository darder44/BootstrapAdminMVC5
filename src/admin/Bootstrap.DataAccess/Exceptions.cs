using Longbow.Web.Mvc;
using PetaPoco;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Common;

namespace Bootstrap.DataAccess
{
    /// <summary>
    /// 異常實體類別
    /// </summary>
    [PrimaryKey("Id", AutoIncrement = true)]
    public class Exceptions
    {
        /// <summary>
        /// 獲得/設置 主鍵
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// 獲得/設置 主鍵
        /// </summary>
        public string AppDomainName { get; set; } = "";

        /// <summary>
        /// 獲得/設置 用户請求頁面地址
        /// </summary>
        [DisplayName("請求網址")]
        public string ErrorPage { get; set; } = "";

        /// <summary>
        /// 獲得/設置 用户 ID
        /// </summary>
        [DisplayName("用户名")]
        public string? UserId { get; set; }

        /// <summary>
        /// 獲得/設置 用户 IP
        /// </summary>
        [DisplayName("登錄主机")]
        public string? UserIp { get; set; }

        /// <summary>
        /// 獲得/設置 異常類別型
        /// </summary>
        [DisplayName("異常類別型")]
        public string? ExceptionType { get; set; }

        /// <summary>
        /// 獲得/設置 異常错误描述訊息
        /// </summary>
        [DisplayName("異常描述")]
        public string Message { get; set; } = "";

        /// <summary>
        /// 獲得/設置 異常Stack訊息
        /// </summary>
        public string? StackTrace { get; set; }

        /// <summary>
        /// 獲得/設置 日誌時間戳
        /// </summary>
        [DisplayName("記錄時間")]
        public DateTime LogTime { get; set; }

        /// <summary>
        /// 獲得/設置 時間描述 2分鐘内為剛剛
        /// </summary>
        [ResultColumn]
        public string Period { get; set; } = "";

        /// <summary>
        /// 獲得/設置 分類別訊息
        /// </summary>
        public string Category { get; set; } = "";

        private static void ClearExceptions() => System.Threading.Tasks.Task.Run(() =>
        {
            DbManager.Create().Execute("delete from Exceptions where LogTime < @0", DateTime.Now.AddMonths(0 - DictHelper.RetrieveExceptionsLogPeriod()));
        });

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="additionalInfo"></param>
        /// <returns></returns>
        public virtual bool Log(Exception ex, NameValueCollection additionalInfo)
        {
            if (ex == null) return true;

            var errorPage = additionalInfo?["ErrorPage"] ?? (ex.GetType().Name.Length > 50 ? ex.GetType().Name.Substring(0, 50) : ex.GetType().Name);
            var loopEx = ex;
            var category = "App";
            while (loopEx != null)
            {
                if (typeof(DbException).IsAssignableFrom(loopEx.GetType()))
                {
                    category = "DB";
                    break;
                }
                loopEx = loopEx.InnerException;
            }
            try
            {
                // 防止資料庫寫入操作失敗後陷入死循環
                // fix https://gitee.com/LongbowEnterprise/dashboard/issues?id=I136OP
                using (var db = Longbow.Data.DbManager.Create())
                {
                    db.Insert(new Exceptions
                    {
                        AppDomainName = AppDomain.CurrentDomain.FriendlyName,
                        ErrorPage = errorPage,
                        UserId = additionalInfo?["UserId"],
                        UserIp = additionalInfo?["UserIp"],
                        ExceptionType = ex.GetType().FullName,
                        Message = ex.Message,
                        StackTrace = ex.StackTrace,
                        LogTime = DateTime.Now,
                        Category = category
                    });
                }
                ClearExceptions();
            }
            catch { }
            return true;
        }

        /// <summary>
        /// 查詢一周内所有異常
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<Exceptions> Retrieves()
        {
            using var db = DbManager.Create();
            return db.Fetch<Exceptions>("select * from Exceptions where LogTime > @0 order by LogTime desc", DateTime.Now.AddMonths(0 - DictHelper.RetrieveExceptionsLogPeriod()));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="po"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public virtual Page<Exceptions> RetrievePages(PaginationOption po, DateTime? startTime, DateTime? endTime)
        {
            if (string.IsNullOrEmpty(po.Sort)) po.Sort = "LogTime";
            if (string.IsNullOrEmpty(po.Order)) po.Order = "desc";

            var sql = new Sql("select * from Exceptions");
            if (startTime.HasValue) sql.Append("where LogTime > @0", startTime.Value);
            if (endTime.HasValue) sql.Append("where LogTime < @0", endTime.Value.AddDays(1).AddSeconds(-1));
            if (startTime == null && endTime == null) sql.Append("where LogTime > @0", DateTime.Today.AddMonths(0 - DictHelper.RetrieveExceptionsLogPeriod()));
            sql.Append($"order by {po.Sort} {po.Order}");

            using var db = DbManager.Create();
            return db.Page<Exceptions>(po.PageIndex, po.Limit, sql);
        }
    }
}
