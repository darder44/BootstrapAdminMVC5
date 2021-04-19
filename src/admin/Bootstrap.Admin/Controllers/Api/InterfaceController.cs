using Bootstrap.DataAccess;
using Bootstrap.Security;
using Bootstrap.Security.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Bootstrap.Admin.Controllers
{
    /// <summary>
    /// 接口控制器
    /// </summary>
    [Route("api/[controller]/[action]")]
    [AllowAnonymous]
    [ApiController]
    public class InterfaceController : ControllerBase
    {
        /// <summary>
        /// 獲取所有字典表資料
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IEnumerable<BootstrapDict> RetrieveDicts()
        {
            return DictHelper.RetrieveDicts();
        }

        /// <summary>
        /// 通過請求地址獲取相对應角色集合
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IEnumerable<string> RetrieveRolesByUrl([FromBody]string url)
        {
            return RoleHelper.RetrievesByUrl(url, BootstrapAppContext.AppId);
        }

        /// <summary>
        /// 通過用户名獲得分配所有角色
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IEnumerable<string> RetrieveRolesByUserName([FromBody]string userName)
        {
            return RoleHelper.RetrievesByUserName(userName);
        }

        /// <summary>
        /// 通過用户名獲得 User 實例
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public BootstrapUser? RetrieveUserByUserName([FromBody]string userName)
        {
            return UserHelper.RetrieveUserByUserName(userName);
        }

        /// <summary>
        /// 通過指定条件獲得應用程序選單
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [HttpPost]
        public IEnumerable<BootstrapMenu> RetrieveAppMenus([FromBody]AppMenuOption args) => MenuHelper.RetrieveAppMenus(args.AppId, args.UserName, args.Url);
    }
}
