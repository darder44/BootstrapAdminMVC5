using Bootstrap.Security.DataAccess;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bootstrap.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    public class App
    {
        /// <summary>
        /// 獲得/設置 應用程式主鍵ID
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// 獲得/設置 群組名稱
        /// </summary>
        public string AppName { get; set; } = "未設置";

        /// <summary>
        /// 獲取/設置 用户群組關联狀態 checked 標示已经關联 '' 標示未關联
        /// </summary>
        public string Checked { get; set; } = "";

        /// <summary>
        /// 根據角色ID指派部门
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public virtual IEnumerable<App> RetrievesByRoleId(string roleId)
        {
            using var db = DbManager.Create();
            var ret = db.Fetch<App>($"select d.Code as Id, d.Name as AppName, case ra.AppId when d.Code then 'checked' else '' end Checked from Dicts d left join RoleApp ra on d.Code = ra.AppId and ra.RoleId = @1 where d.Category = @0", "應用程式", roleId);

            // 判斷是否為Administrators
            var role = RoleHelper.Retrieves().FirstOrDefault(r => r.Id == roleId);
            if (role != null && role.RoleName.Equals("Administrators", StringComparison.OrdinalIgnoreCase))
            {
                ret.ForEach(r => r.Checked = "checked");
            }
            return ret;
        }

        /// <summary>
        /// 根據指定用户名獲得授權應用程式集合
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public virtual IEnumerable<string> RetrievesByUserName(string userName) => DbHelper.RetrieveAppsByUserName(userName);

        /// <summary>
        /// 根據角色ID以及選定的App ID，保存到角色應用表
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="appIds"></param>
        /// <returns></returns>
        public virtual bool SaveByRoleId(string roleId, IEnumerable<string> appIds)
        {
            if (string.IsNullOrEmpty(roleId)) throw new ArgumentNullException(nameof(roleId));

            bool ret = false;
            if (appIds == null) appIds = new string[0];
            var db = DbManager.Create();
            try
            {
                db.BeginTransaction();
                //刪除角色應用表该角色所有的應用
                db.Execute("delete from RoleApp where RoleID = @0", roleId);
                db.InsertBatch("RoleApp", appIds.Select(g => new { RoleID = roleId, AppID = g }));
                db.CompleteTransaction();
                ret = true;
            }
            catch (Exception)
            {
                db.AbortTransaction();
                throw;
            }
            return ret;
        }
    }
}
