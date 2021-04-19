using PetaPoco;
using System;
using System.Collections.Generic;

namespace Bootstrap.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    [TableName("Tasks")]
    public class Task
    {
        /// <summary>
        /// 獲取/設置  任務ID
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// 獲取/設置  任務名稱
        /// </summary>
        public string TaskName { get; set; } = "";

        /// <summary>
        /// 獲取/設置  分配人
        /// </summary>
        public string AssignName { get; set; } = "";

        /// <summary>
        /// 獲得/設置 分配人昵稱
        /// </summary>
        [ResultColumn]
        public string AssignDisplayName { get; set; } = "";

        /// <summary>
        /// 獲取/設置  完成任務人
        /// </summary>
        public string UserName { get; set; } = "";

        /// <summary>
        /// 獲取/設置  任務所需時間（天）
        /// </summary>
        public int TaskTime { get; set; }

        /// <summary>
        /// 獲取/設置  任務进度
        /// </summary>
        public double TaskProgress { get; set; }

        /// <summary>
        /// 獲取/設置  分配時間
        /// </summary>
        public DateTime AssignTime { get; set; }

        /// <summary>
        /// 查詢所有任務
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<Task> Retrieves()
        {
            using var db = DbManager.Create();
            return db.SkipTake<Task>(0, 1000, "select t.*, u.DisplayName AssignDisplayName from Tasks t inner join Users u on t.UserName = u.UserName order by AssignTime desc");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public virtual bool Save(Task task)
        {
            using var db = DbManager.Create();
            db.Save(task);
            return true;
        }
    }
}
