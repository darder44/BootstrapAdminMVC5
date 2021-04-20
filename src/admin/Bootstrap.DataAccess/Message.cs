using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bootstrap.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    [TableName("Messages")]
    public class Message
    {
        /// <summary>
        /// 訊息主鍵 資料庫自增
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// 標題
        /// </summary>
        public string Title { get; set; } = "";

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; } = "";

        /// <summary>
        /// 發訊息人
        /// </summary>
        public string From { get; set; } = "";

        /// <summary>
        /// 收訊息人
        /// </summary>
        public string To { get; set; } = "";

        /// <summary>
        /// 訊息發送時間
        /// </summary>
        public DateTime SendTime { get; set; }

        /// <summary>
        /// 訊息狀態：0-未读，1-已读 和Dict表的通知訊息關联
        /// </summary>
        public string Status { get; set; } = "0";

        /// <summary>
        /// 標旗狀態：0-未標旗，1-已標旗
        /// </summary>
        public int Flag { get; set; }

        /// <summary>
        /// 刪除狀態：0-未刪除，1-已刪除
        /// </summary>
        public int IsDelete { get; set; }

        /// <summary>
        /// 訊息標簽：0-一般，1-紧要 和Dict表的訊息標簽關联
        /// </summary>
        public string Label { get; set; } = "0";

        /// <summary>
        /// 獲得/設置 標簽名稱
        /// </summary>
        [ResultColumn]
        public string LabelName { get; set; } = "";

        /// <summary>
        /// 獲得/設置 時間描述 2分鐘内為剛剛
        /// </summary>
        [ResultColumn]
        public string Period { get; set; } = "";

        /// <summary>
        /// 獲得/設置 發件人頭像
        /// </summary>
        [ResultColumn]
        public string FromIcon { get; set; } = "";

        /// <summary>
        /// 獲得/設置 發件人昵稱
        /// </summary>
        [ResultColumn]
        public string FromDisplayName { get; set; } = "";

        /// <summary>
        /// 所有有關userName所有訊息列表
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        protected virtual IEnumerable<Message> Retrieves(string userName)
        {
            using var db = DbManager.Create();
            var t = db.Provider.EscapeSqlIdentifier("To");
            var f = db.Provider.EscapeSqlIdentifier("From");
            return db.Fetch<Message>($"select m.*, d.Name, u.DisplayName from Messages m left join Dicts d on m.Label = d.Code and d.Category = @0 and d.Define = 0 inner join Users u on m.{f} = u.UserName where {t} = @1 or {f} = @1 order by SendTime desc", "訊息標簽", userName);
        }

        /// <summary>
        /// 收件箱
        /// </summary>
        /// <param name="userName"></param>
        public virtual IEnumerable<Message> Inbox(string userName)
        {
            var messageRet = Retrieves(userName);
            return messageRet.Where(n => n.To.Equals(userName, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// 發件箱
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public virtual IEnumerable<Message> SendMail(string userName)
        {
            var messageRet = Retrieves(userName);
            return messageRet.Where(n => n.From.Equals(userName, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// 垃圾箱
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public virtual IEnumerable<Message> Trash(string userName)
        {
            var messageRet = Retrieves(userName);
            return messageRet.Where(n => n.IsDelete == 1);
        }

        /// <summary>
        /// 標旗
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public virtual IEnumerable<Message> Mark(string userName)
        {
            var messageRet = Retrieves(userName);
            return messageRet.Where(n => n.Flag == 1);
        }

        /// <summary>
        /// 獲取Header處顯示的訊息列表
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public virtual IEnumerable<Message> RetrieveHeaders(string userName)
        {
            var messageRet = Inbox(userName);
            messageRet.AsParallel().ForAll(n =>
            {
                var ts = DateTime.Now - n.SendTime;
                if (ts.TotalMinutes < 5) n.Period = "剛剛";
                else if (ts.Days > 0) n.Period = string.Format("{0}天", ts.Days);
                else if (ts.Hours > 0) n.Period = string.Format("{0}小時", ts.Hours);
                else if (ts.Minutes > 0) n.Period = string.Format("{0}分鐘", ts.Minutes);
            });
            return messageRet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public virtual bool Save(Message msg)
        {
            using var db = DbManager.Create();
            db.Save(msg);
            return true;
        }
    }
}
