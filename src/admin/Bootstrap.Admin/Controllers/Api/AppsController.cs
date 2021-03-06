using Bootstrap.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Bootstrap.Admin.Controllers.Api
{
    /// <summary>
    /// 應用程式控制器
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AppsController : ControllerBase
    {
        /// <summary>
        /// 通過角色ID獲取其授權的所有應用程式集合
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IEnumerable<DataAccess.App> Get(string id) => AppHelper.RetrievesByRoleId(id);
    }
}
