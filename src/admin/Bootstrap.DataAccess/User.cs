using Bootstrap.Security;
using Bootstrap.Security.DataAccess;
using Longbow.Security.Cryptography;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Bootstrap.DataAccess
{
    /// <summary>
    /// 用户表實體類別
    /// </summary>
    [TableName("Users")]
    public class User : BootstrapUser
    {
        /// <summary>
        /// 獲得/設置 用户主鍵ID
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// 獲取/設置 密碼
        /// </summary>
        [DisplayName("登錄密碼")]
        public string Password { get; set; } = "";

        /// <summary>
        /// 獲取/設置 密碼盐
        /// </summary>
        public string PassSalt { get; set; } = "";

        /// <summary>
        /// 獲取/設置 角色用户關联狀態 checked 標示已经關联 '' 標示未關联
        /// </summary>
        [ResultColumn]
        public string Checked { get; set; } = "";

        /// <summary>
        /// 獲得/設置 用户注册時間
        /// </summary>
        [DisplayName("注册時間")]
        public DateTime RegisterTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 獲得/設置 用户被批復時間
        /// </summary>
        [DisplayName("授權時間")]
        public DateTime? ApprovedTime { get; set; }

        /// <summary>
        /// 獲得/設置 用户批復人
        /// </summary>
        [DisplayName("授權人")]
        public string? ApprovedBy { get; set; }

        /// <summary>
        /// 獲得/設置 用户的申請理由
        /// </summary>
        [DisplayName("說明")]
        public string Description { get; set; } = "";

        /// <summary>
        /// 獲得/設置 用户當前狀態 0 表示管理员注册用户 1 表示用户注册 2 表示更改密碼 3 表示更改個人皮肤 4 表示更改顯示名稱 5 批復新用户注册操作
        /// </summary>
        [ResultColumn]
        public UserStates UserStatus { get; set; }

        /// <summary>
        /// 獲得/設置 通知描述 2分鐘内為剛剛
        /// </summary>
        [ResultColumn]
        public string? Period { get; set; }

        /// <summary>
        /// 獲得/設置 新密碼
        /// </summary>
        [ResultColumn]
        [DisplayName("确認密碼")]
        public string NewPassword { get; set; } = "";

        /// <summary>
        /// 獲得/設置 是否重置密碼
        /// </summary>
        [ResultColumn]
        public int IsReset { get; set; }

        /// <summary>
        /// 驗證用户登錄账号與密碼正确
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public virtual bool Authenticate(string userName, string password)
        {
            using var db = DbManager.Create();
            var user = db.SingleOrDefault<User>("select Password, PassSalt from Users where ApprovedTime is not null and UserName = @0", userName);

            return user != null && !string.IsNullOrEmpty(user.PassSalt) && user.Password == LgbCryptography.ComputeHash(password, user.PassSalt);
        }

        /// <summary>
        /// 保存預設應用方法
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="app"></param>
        /// <returns></returns>
        public virtual bool SaveApp(string userName, string app)
        {
            using var db = DbManager.Create();
            return db.Update<User>("set App = @1 where UserName = @0", userName, app) == 1;
        }

        /// <summary>
        /// 更改密碼方法
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="newPass"></param>
        /// <returns></returns>
        public virtual bool ChangePassword(string userName, string password, string newPass)
        {
            bool ret = false;
            if (Authenticate(userName, password))
            {
                string sql = "set Password = @0, PassSalt = @1 where UserName = @2";
                var passSalt = LgbCryptography.GenerateSalt();
                var newPassword = LgbCryptography.ComputeHash(newPass, passSalt);
                using var db = DbManager.Create();
                ret = db.Update<User>(sql, newPassword, passSalt, userName) == 1;
            }
            return ret;
        }

        /// <summary>
        /// 查詢所有用户
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<User> Retrieves()
        {
            using var db = DbManager.Create();
            return db.Fetch<User>("select u.ID, u.UserName, u.DisplayName, RegisterTime, ApprovedTime, ApprovedBy, Description, ru.IsReset from Users u left join (select 1 as IsReset, UserName from ResetUsers group by UserName) ru on u.UserName = ru.UserName Where ApprovedTime is not null");
        }

        /// <summary>
        /// 查詢所有的新注册用户
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<User> RetrieveNewUsers()
        {
            using var db = DbManager.Create();
            return db.Fetch<User>("select ID, UserName, DisplayName, RegisterTime, Description from Users Where ApprovedTime is null order by RegisterTime desc");
        }

        /// <summary>
        /// 刪除用户
        /// </summary>
        /// <param name="value"></param>
        public virtual bool Delete(IEnumerable<string> value)
        {
            if (!value.Any()) return true;
            using var db = DbManager.Create();
            bool ret;
            try
            {
                var ids = string.Join(",", value);
                db.BeginTransaction();
                db.Execute($"Delete from UserRole where UserID in ({ids})");
                db.Execute($"delete from UserGroup where UserID in ({ids})");
                db.Delete<User>($"where ID in ({ids})");
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
        /// 重置密碼方法
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public virtual bool ResetPassword(string userName, string password)
        {
            var ret = false;
            var resetUser = UserHelper.RetrieveResetUserByUserName(userName);
            if (resetUser == null) return ret;

            string sql = "set Password = @0, PassSalt = @1 where UserName = @2";
            var passSalt = LgbCryptography.GenerateSalt();
            var newPassword = LgbCryptography.ComputeHash(password, passSalt);
            using var db = DbManager.Create();
            try
            {
                db.BeginTransaction();
                ret = db.Update<User>(sql, newPassword, passSalt, userName) == 1;
                if (ret) db.Execute("delete from ResetUsers where UserName = @0", userName);
                db.CompleteTransaction();
            }
            catch (Exception)
            {
                db.AbortTransaction();
                throw;
            }
            return ret;
        }

        /// <summary>
        /// 忘記密碼方法
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual bool ForgotPassword(ResetUser user) => ResetUserHelper.Save(user);

        /// <summary>
        /// 新建前台User View調用/注册用户調用
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual bool Save(User user)
        {
            user.PassSalt = LgbCryptography.GenerateSalt();
            user.Password = LgbCryptography.ComputeHash(user.Password, user.PassSalt);
            user.RegisterTime = DateTime.Now;

            using var db = DbManager.Create();
            bool ret;
            try
            {
                db.BeginTransaction();
                if (!db.Exists<User>("UserName = @0", user.UserName))
                {
                    db.Insert(user);
                    db.Execute("insert into UserRole (UserID, RoleID) select ID, (select ID from Roles where RoleName = 'Default') RoleId from Users where UserName = @0", user.UserName);
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
        /// User List 視圖保存按鈕調用
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <param name="displayName"></param>
        /// <returns></returns>
        public virtual bool Update(string id, string password, string displayName)
        {
            var passSalt = LgbCryptography.GenerateSalt();
            var newPassword = LgbCryptography.ComputeHash(password, passSalt);
            using var db = DbManager.Create();
            return db.Update<User>("set Password = @1, PassSalt = @2, DisplayName = @3 where ID = @0", id, newPassword, passSalt, displayName) == 1;
        }

        /// <summary>
        /// 通过新用户方法
        /// </summary>
        /// <param name="id"></param>
        /// <param name="approvedBy"></param>
        /// <returns></returns>
        public virtual bool Approve(string id, string approvedBy)
        {
            using var db = DbManager.Create();
            return db.Update<User>("set ApprovedTime = @1, ApprovedBy = @2 where ID = @0", id, DateTime.Now, approvedBy) == 1;
        }

        /// <summary>
        /// 拒绝新用户方法
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rejectBy"></param>
        /// <returns></returns>
        public virtual bool Reject(string id, string rejectBy)
        {
            using var db = DbManager.Create();
            var ret = false;
            try
            {
                db.BeginTransaction();
                db.Execute("insert into RejectUsers (UserName, DisplayName, RegisterTime, RejectedBy, RejectedTime, RejectedReason) select UserName, DisplayName, Registertime, @1, @2, @3 from Users where ID = @0", id, rejectBy, DateTime.Now, "未填寫");
                db.Execute("delete from UserRole where UserId = @0", id);
                db.Execute("delete from UserGroup where UserId = @0", id);
                db.Delete<User>("where Id = @0", id);
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
        /// 通过roleId獲取所有用户
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public virtual IEnumerable<User> RetrievesByRoleId(string roleId)
        {
            using var db = DbManager.Create();
            return db.Fetch<User>("select u.ID, u.UserName, u.DisplayName, case ur.UserID when u.ID then 'checked' else '' end Checked from Users u left join UserRole ur on u.ID = ur.UserID and RoleID = @0 where u.ApprovedTime is not null", roleId);
        }

        /// <summary>
        /// 通过角色ID保存當前授權用户（插入）
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="userIds">用户ID数組</param>
        /// <returns></returns>
        public virtual bool SaveByRoleId(string roleId, IEnumerable<string> userIds)
        {
            var ret = false;
            var db = DbManager.Create();
            try
            {
                db.BeginTransaction();
                //刪除用户角色表該角色所有的用户
                db.Execute("delete from UserRole where RoleID = @0", roleId);
                db.InsertBatch("UserRole", userIds.Select(g => new { UserID = g, RoleID = roleId }));
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
        /// 通过groupId獲取所有用户
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public virtual IEnumerable<User> RetrievesByGroupId(string groupId)
        {
            using var db = DbManager.Create();
            return db.Fetch<User>("select u.ID, u.UserName, u.DisplayName, case ur.UserID when u.ID then 'checked' else '' end Checked from Users u left join UserGroup ur on u.ID = ur.UserID and GroupID = @0 where u.ApprovedTime is not null", groupId);
        }

        /// <summary>
        /// 通过部门ID保存當前授權用户（插入）
        /// </summary>
        /// <param name="groupId">GroupID</param>
        /// <param name="userIds">用户ID数組</param>
        /// <returns></returns>
        public virtual bool SaveByGroupId(string groupId, IEnumerable<string> userIds)
        {
            var ret = false;
            using var db = DbManager.Create();
            try
            {
                db.BeginTransaction();
                //刪除用户角色表該角色所有的用户
                db.Execute("delete from UserGroup where GroupID = @0", groupId);
                db.InsertBatch("UserGroup", userIds.Select(g => new { UserID = g, GroupID = groupId }));
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
        /// 根據用户名修改用户頭像
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="iconName"></param>
        /// <returns></returns>
        public virtual bool SaveUserIconByName(string userName, string iconName)
        {
            using var db = DbManager.Create();
            return db.Update<User>("set Icon = @1 where UserName = @0", userName, iconName) == 1;
        }

        /// <summary>
        /// 保存顯示名稱方法
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="displayName"></param>
        /// <returns></returns>
        public virtual bool SaveDisplayName(string userName, string displayName)
        {
            using var db = DbManager.Create();
            return db.Update<User>("set DisplayName = @1 where UserName = @0", userName, displayName) == 1;
        }

        /// <summary>
        /// 根據用户名更改用户皮肤
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="cssName"></param>
        /// <returns></returns>
        public virtual bool SaveUserCssByName(string userName, string cssName)
        {
            using var db = DbManager.Create();
            return db.Update<User>("set Css = @1 where UserName = @0", userName, cssName) == 1;
        }

        /// <summary>
        /// 獲得指定用户方法
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public virtual BootstrapUser? RetrieveUserByUserName(string userName) => DbHelper.RetrieveUserByUserName(userName);

        /// <summary>
        /// ToString 方法
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} ({1})", UserName, DisplayName);
        }
    }

    /// <summary>
    /// 用户狀態枚举類別型
    /// </summary>
    public enum UserStates
    {
        /// <summary>
        /// 更改密碼
        /// </summary>
        ChangePassword,
        /// <summary>
        /// 更改樣式
        /// </summary>
        ChangeTheme,
        /// <summary>
        /// 更改顯示名稱
        /// </summary>
        ChangeDisplayName,
        /// <summary>
        /// 审批用户
        /// </summary>
        ApproveUser,
        /// <summary>
        /// 拒绝用户
        /// </summary>
        RejectUser,
        /// <summary>
        /// 保存預設應用
        /// </summary>
        SaveApp
    }
}
