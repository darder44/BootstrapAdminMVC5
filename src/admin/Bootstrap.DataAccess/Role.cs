using Bootstrap.Security.DataAccess;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Bootstrap.DataAccess
{
    /// <summary>
    /// 角色實體類別
    /// </summary>
    [TableName("Roles")]
    public class Role
    {
        /// <summary>
        /// 獲得/設置 角色主鍵ID
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// 獲得/設置 角色名稱
        /// </summary>
        [DisplayName("角色名稱")]
        public string RoleName { get; set; } = "";

        /// <summary>
        /// 獲得/設置 角色描述
        /// </summary>
        [DisplayName("角色描述")]
        public string Description { get; set; } = "";

        /// <summary>
        /// 獲取/設置 用户角色關联狀態 checked 標示已经關联 '' 標示未關联
        /// </summary>
        [ResultColumn]
        public string Checked { get; set; } = "";

        /// <summary>
        /// 查詢所有角色
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<Role> Retrieves()
        {
            using var db = DbManager.Create();
            return db.Fetch<Role>();
        }

        /// <summary>
        /// 保存用户角色關係
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        public virtual bool SaveByUserId(string userId, IEnumerable<string> roleIds)
        {
            var ret = false;
            using var db = DbManager.Create();
            try
            {
                db.BeginTransaction();
                // delete user from config table
                db.Execute("delete from UserRole where UserID = @0", userId);
                db.InsertBatch("UserRole", roleIds.Select(g => new { UserID = userId, RoleID = g }));
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

        /// <summary>
        /// 查詢某個用户所拥有的角色
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<Role> RetrievesByUserId(string userId)
        {
            using var db = DbManager.Create();
            return db.Fetch<Role>("select r.ID, r.RoleName, r.Description, case ur.RoleID when r.ID then 'checked' else '' end Checked from Roles r left join UserRole ur on r.ID = ur.RoleID and UserID = @0", userId);
        }

        /// <summary>
        /// 刪除角色表
        /// </summary>
        /// <param name="value"></param>
        public virtual bool Delete(IEnumerable<string> value)
        {
            if (!value.Any()) return true;
            var ret = false;
            var ids = string.Join(",", value);
            using var db = DbManager.Create();
            try
            {
                db.BeginTransaction();
                db.Execute($"delete from UserRole where RoleID in ({ids})");
                db.Execute($"delete from RoleGroup where RoleID in ({ids})");
                db.Execute($"delete from NavigationRole where RoleID in ({ids})");
                db.Delete<Role>($"where ID in ({ids})");
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

        /// <summary>
        /// 保存新建/更新的角色訊息
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public virtual bool Save(Role p)
        {
            if (!string.IsNullOrEmpty(p.RoleName) && p.RoleName.Length > 50) p.RoleName = p.RoleName.Substring(0, 50);
            if (!string.IsNullOrEmpty(p.Description) && p.Description.Length > 50) p.Description = p.Description.Substring(0, 500);

            using var db = DbManager.Create();
            db.Save(p);
            return true;
        }

        /// <summary>
        /// 查詢某個選單所拥有的角色
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public virtual IEnumerable<Role> RetrievesByMenuId(string menuId)
        {
            using var db = DbManager.Create();
            return db.Fetch<Role>("select r.ID, r.RoleName, r.Description, case ur.RoleID when r.ID then 'checked' else '' end Checked from Roles r left join NavigationRole ur on r.ID = ur.RoleID and NavigationID = @0", menuId);
        }

        /// <summary>
        /// 通过指定選單 ID 保存角色集合資料
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        public virtual bool SavaByMenuId(string menuId, IEnumerable<string> roleIds)
        {
            // 参数 id 可能是子選單
            // https://gitee.com/LongbowEnterprise/dashboard/issues?id=IQW93

            var ret = false;
            using var db = DbManager.Create();
            db.BeginTransaction();
            try
            {
                string? parentId = menuId;
                if (!string.IsNullOrEmpty(parentId))
                {
                    do
                    {
                        // delete role from config table
                        db.Execute("delete from NavigationRole where NavigationID = @0", parentId);
                        db.InsertBatch("NavigationRole", roleIds.Select(g => new { NavigationID = parentId, RoleID = g }));

                        // find parent Menu Id
                        parentId = db.ExecuteScalar<string?>("select ParentId from Navigations Where Id = @0", parentId);
                    }
                    while (!string.IsNullOrEmpty(parentId) && parentId != "0");
                }
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

        /// <summary>
        /// 根據GroupId查詢和該Group有關的所有Roles
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public virtual IEnumerable<Role> RetrievesByGroupId(string groupId)
        {
            using var db = DbManager.Create();
            return db.Fetch<Role>("select r.ID, r.RoleName, r.Description, case ur.RoleID when r.ID then 'checked' else '' end Checked from Roles r left join RoleGroup ur on r.ID = ur.RoleID and GroupID = @0", groupId);
        }

        /// <summary>
        /// 根據GroupId更新Roles訊息，刪除旧的Roles訊息，插入新的Roles訊息
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        public virtual bool SaveByGroupId(string groupId, IEnumerable<string> roleIds)
        {
            var ret = false;
            var db = DbManager.Create();
            try
            {
                // delete user from config table
                db.Execute("delete from RoleGroup where GroupID = @0", groupId);
                db.InsertBatch("RoleGroup", roleIds.Select(g => new { GroupID = groupId, RoleID = g }));
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

        /// <summary>
        /// 通过指定登錄用户名獲得角色列表
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public virtual IEnumerable<string> RetrievesByUserName(string userName) => DbHelper.RetrieveRolesByUserName(userName);

        /// <summary>
        /// 根據選單url查詢某個所拥有的角色
        /// 从NavigatorRole表查
        /// 从Navigators -> GroupNavigatorRole -> Role查查詢某個用户所拥有的角色
        /// </summary>
        /// <param name="url"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        public virtual IEnumerable<string> RetrievesByUrl(string url, string appId) => DbHelper.RetrieveRolesByUrl(url, appId);
    }
}
