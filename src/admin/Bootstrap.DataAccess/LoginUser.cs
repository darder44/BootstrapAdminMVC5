using Longbow.Web.Mvc;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Bootstrap.DataAccess
{
    /// <summary>
    /// 登錄用户訊息實體類別
    /// </summary>
    [TableName("LoginLogs")]
    public class LoginUser
    {
        /// <summary>
        /// 獲得/設置 Id
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// 獲得/設置 用户名
        /// </summary>
        [DisplayName("登錄名稱")]
        public string UserName { get; set; } = "";

        /// <summary>
        /// 獲得/設置 登錄時間
        /// </summary>
        [DisplayName("登錄時間")]
        public DateTime LoginTime { get; set; }

        /// <summary>
        /// 獲得/設置 登錄IP地址
        /// </summary>
        [DisplayName("主機")]
        public string Ip { get; set; } = "";

        /// <summary>
        /// 獲得/設置 登錄瀏覽器
        /// </summary>
        [DisplayName("瀏覽器")]
        public string Browser { get; set; } = "";

        /// <summary>
        /// 獲得/設置 登錄操作系統
        /// </summary>
        [DisplayName("操作系統")]
        public string OS { get; set; } = "";

        /// <summary>
        /// 獲得/設置 登錄地點
        /// </summary>
        [DisplayName("登錄地點")]
        public string City { get; set; } = "";

        /// <summary>
        /// 獲得/設置 登錄是否成功
        /// </summary>
        [DisplayName("登錄结果")]
        public string Result { get; set; } = "";

        /// <summary>
        /// 獲得/設置 用户 UserAgent
        /// </summary>
        [DisplayName("登錄名稱")]
        public string UserAgent { get; set; } = "";

        /// <summary>
        /// 保存登錄用户資料
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual bool Log(LoginUser user)
        {
            using (var db = DbManager.Create())
            {
                db.Save(user);
            }
            return true;
        }

        /// <summary>
        /// 獲得登錄用户的分頁資料
        /// </summary>
        /// <param name="po"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        public virtual Page<LoginUser> RetrieveByPages(PaginationOption po, DateTime? startTime, DateTime? endTime, string? ip)
        {
            if (string.IsNullOrEmpty(po.Sort)) po.Sort = "LoginTime";
            if (string.IsNullOrEmpty(po.Order)) po.Order = "desc";
            var sql = new Sql("select * from LoginLogs");
            if (startTime.HasValue) sql.Where("LoginTime >= @0", startTime.Value);
            if (endTime.HasValue) sql.Where("LoginTime < @0", endTime.Value.AddDays(1));
            if (!string.IsNullOrEmpty(ip)) sql.Where("ip = @0", ip);
            sql.OrderBy($"{po.Sort} {po.Order}");
            using var db = DbManager.Create();
            return db.Page<LoginUser>(po.PageIndex, po.Limit, sql);
        }

        /// <summary>
        /// 獲取所有登錄資料
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<LoginUser> RetrieveAll(DateTime? startTime, DateTime? endTime, string? ip)
        {
            var sql = new Sql("select UserName, LoginTime, Ip, Browser, OS, City, Result from LoginLogs");
            if (startTime.HasValue) sql.Where("LoginTime >= @0", startTime.Value);
            if (endTime.HasValue) sql.Where("LoginTime < @0", endTime.Value.AddDays(1));
            if (!string.IsNullOrEmpty(ip)) sql.Where("ip = @0", ip);
            sql.OrderBy($"LoginTime");
            using var db = DbManager.Create();
            return db.Fetch<LoginUser>(sql);
        }
    }
}
