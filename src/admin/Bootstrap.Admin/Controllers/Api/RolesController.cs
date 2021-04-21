﻿using Bootstrap.Admin.Query;
using Bootstrap.DataAccess;
using Longbow.Web.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;


namespace Bootstrap.Admin.Controllers.Api
{
    /// <summary>
    /// 角色維護控制器
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class RolesController : ControllerBase
    {
        /// <summary>
        /// 獲取所有角色資料
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpGet]
        public QueryData<object> Get([FromQuery]QueryRoleOption value)
        {
            return value.RetrieveData();
        }
        /// <summary>
        /// 通過指定用户ID/部門ID/選單ID獲得所有角色集合，已经授權的有checked標記
        /// </summary>
        /// <param name="id">用户ID/部門ID/選單ID</param>
        /// <param name="type">類型</param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public IEnumerable<object> Post(string id, [FromQuery]string type)
        {
            var ret = type switch
            {
                "user" => RoleHelper.RetrievesByUserId(id),
                "group" => RoleHelper.RetrievesByGroupId(id),
                "menu" => RoleHelper.RetrievesByMenuId(id),
                _ => new Role[0]
            };
            return ret.Select(m => new { m.Id, m.Checked, m.RoleName, m.Description });
        }
        /// <summary>
        /// 保存角色授權方法
        /// </summary>
        /// <param name="id">角色ID</param>
        /// <param name="values">選中的ID集合</param>
        /// <param name="type">type=menu時，選單維護頁面对角色授權彈框保存按鈕調用</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ButtonAuthorize(Url = "~/Admin/Roles", Auth = "assignUser,assignGroup,assignMenu,assignApp")]
        public bool Put(string id, [FromBody]IEnumerable<string> values, [FromQuery]string type) => type switch
        {
            "user" => UserHelper.SaveByRoleId(id, values),
            "group" => GroupHelper.SaveByRoleId(id, values),
            "menu" => MenuHelper.SaveMenusByRoleId(id, values),
            "app" => AppHelper.SaveByRoleId(id, values),
            _ => false
        };
        /// <summary>
        /// 保存角色方法
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        [ButtonAuthorize(Url = "~/Admin/Roles", Auth = "add,edit")]
        public bool Post([FromBody]Role value)
        {
            return RoleHelper.Save(value);
        }
        /// <summary>
        /// 刪除角色方法
        /// </summary>
        /// <param name="value"></param>
        [HttpDelete]
        [ButtonAuthorize(Url = "~/Admin/Roles", Auth = "del")]
        public bool Delete([FromBody]IEnumerable<string> value)
        {
            return RoleHelper.Delete(value);
        }
    }
}
