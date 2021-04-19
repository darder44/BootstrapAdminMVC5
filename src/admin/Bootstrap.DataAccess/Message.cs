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
        /// 訊息主键 資料庫自增
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; } = "";

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; } = "";

        /// <summary>
        /// 发訊息人
        /// </summary>
        public string From { get; set; } = "";

        /// <summary>
        /// 收訊息人
        /// </summary>
        public string To { get; set; } = "";

        /// <summary>
        /// 訊息发送時間
        /// </summary>
        public DateTime SendTime { get; set; }

        /// <summary>
        /// 訊息状態：0-未读，1-已读 和Dict表的通知訊息关联
        /// </summary>
        public string Status { get; set; } = "0";

        /// <summary>
        /// 标旗状態：0-未标旗，1-已标旗
        /// </summary>
        public int Flag { get; set; }

        /// <summary>
        /// 删除状態：0-未删除，1-已删除
        /// </summary>
        public int IsDelete { get; set; }

        /// <summary>
        /// 訊息标签：0-一般，1-紧要 和Dict表的訊息标签关联
        /// </summary>
        public string Label { get; set; } = "0";

        /// <summary>
        /// 獲得/設置 标签名稱
        /// </summary>
        [ResultColumn]
        public string LabelName { get; set; } = "";

        /// <summary>
        /// 獲得/設置 時間描述 2分鐘内為剛剛
        /// </summary>
        [ResultColumn]
        public string Period { get; set; } = "";

        /// <summary>
        /// 獲得/設置 发件人头像
        /// </summary>
        [ResultColumn]
        public string FromIcon { get; set; } = "";

        /// <summary>
        /// 獲得/設置 发件人昵稱
        /// </summary>
        [ResultColumn]
        public string FromDisplayName { get; set; } = "";

        /// <summary>
        /// 所有有关userName所有訊息列表
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        protected virtual IEnumerable<Message> Retrieves(string userName)
        {
            using var db = DbManager.Create();
            var t = db.Provider.EscapeSqlIdentifier("To");
            var f = db.Provider.EscapeSqlIdentifier("From");
            return db.Fetch<Message>($"select m.*, d.Name, u.DisplayName from Messages m left join Dicts d on m.Label = d.Code and d.Category = @0 and d.Define = 0 inner join Users u on m.{f} = u.UserName where {t} = @1 or {f} = @1 order by SendTime desc", "訊息标签", userName);
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
        /// 发件箱
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
        /// 标旗
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public virtual IEnumerable<Message> Mark(string userName)
        {
            var messageRet = Retrieves(userName);
            return messageRet.Where(n => n.Flag == 1);
        }

        /// <summary>
        /// 获取Header處显示的訊息列表
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
