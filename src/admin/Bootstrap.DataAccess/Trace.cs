using Longbow.Web.Mvc;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Bootstrap.DataAccess
{
    /// <summary>
    /// 用户訪問資料實體類別
    /// </summary>
    [TableName("Traces")]
    public class Trace
    {
        /// <summary>
        /// 獲得/設置 操作日誌主鍵ID
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// 獲得/設置 用户名稱
        /// </summary>
        [DisplayName("用户名稱")]
        public string UserName { get; set; } = "";

        /// <summary>
        /// 獲得/設置 操作時間
        /// </summary>
        [DisplayName("操作時間")]
        public DateTime LogTime { get; set; }

        /// <summary>
        /// 獲得/設置 客户端IP
        /// </summary>
        [DisplayName("登錄主机")]
        public string Ip { get; set; } = "";

        /// <summary>
        /// 獲得/設置 客户端地点
        /// </summary>
        [DisplayName("操作地点")]
        public string City { get; set; } = "";

        /// <summary>
        /// 獲得/設置 客户端浏览器
        /// </summary>
        [DisplayName("浏览器")]
        public string Browser { get; set; } = "";

        /// <summary>
        /// 獲得/設置 客户端操作系統
        /// </summary>
        [DisplayName("操作系統")]
        public string OS { get; set; } = "";

        /// <summary>
        /// 獲取/設置 請求網址
        /// </summary>
        [DisplayName("操作頁面")]
        public string RequestUrl { get; set; } = "";

        /// <summary>
        /// 獲得/設置 客户端 UserAgent
        /// </summary>
        [DisplayName("UserAgent")]
        public string UserAgent { get; set; } = "";

        /// <summary>
        /// 獲得/設置 客户端 Referer
        /// </summary>
        [DisplayName("Referer")]
        public string Referer { get; set; } = "";

        /// <summary>
        /// 保存用户訪問資料历史記錄
        /// </summary>
        /// <param name="p"></param>
        public virtual bool Save(Trace p)
        {
            using var db = DbManager.Create();
            db.Save(p);
            ClearTraces();
            return true;
        }

        /// <summary>
        /// 查詢用户訪問分頁資料
        /// </summary>
        /// <param name="po"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        public virtual Page<Trace> RetrievePages(PaginationOption po, DateTime? startTime, DateTime? endTime, string? ip)
        {
            if (string.IsNullOrEmpty(po.Order)) po.Order = "desc";
            if (string.IsNullOrEmpty(po.Sort)) po.Sort = "LogTime";
            var sql = new Sql("select * from Traces");
            if (startTime.HasValue) sql.Where("LogTime > @0", startTime.Value);
            if (endTime.HasValue) sql.Where("LogTime < @0", endTime.Value.AddDays(1).AddSeconds(-1));
            if (startTime == null && endTime == null) sql.Where("LogTime > @0", DateTime.Today.AddMonths(0 - DictHelper.RetrieveAccessLogPeriod()));
            if (!string.IsNullOrEmpty(ip)) sql.Where("IP = @0", ip);
            sql.OrderBy($"{po.Sort} {po.Order}");

            using var db = DbManager.Create();
            return db.Page<Trace>(po.PageIndex, po.Limit, sql);
        }

        /// <summary>
        /// 查詢所有用户資料
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        public virtual IEnumerable<Trace> RetrieveAll(DateTime? startTime, DateTime? endTime, string? ip)
        {
            var sql = new Sql("select UserName, LogTime, IP, Browser, OS, City, RequestUrl from Traces");
            if (startTime.HasValue) sql.Where("LogTime > @0", startTime.Value);
            if (endTime.HasValue) sql.Where("LogTime < @0", endTime.Value.AddDays(1).AddSeconds(-1));
            if (!string.IsNullOrEmpty(ip)) sql.Where("IP = @0", ip);
            sql.OrderBy("LogTime");

            using var db = DbManager.Create();
            return db.Fetch<Trace>(sql);
        }

        private static void ClearTraces() => System.Threading.Tasks.Task.Run(() =>
        {
            using var db = DbManager.Create();
            return db.Execute("delete from Traces where LogTime < @0", DateTime.Now.AddMonths(0 - DictHelper.RetrieveAccessLogPeriod()));
        });
    }
}
