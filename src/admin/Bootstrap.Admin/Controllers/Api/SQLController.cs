using Bootstrap.Admin.Query;
using Bootstrap.DataAccess;
using Longbow.Web.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bootstrap.Admin.Controllers.Api
{
    /// <summary>
    /// SQL 语句執行日誌 webapi
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class SQLController : ControllerBase
    {
        /// <summary>
        /// 獲取執行日誌資料
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpGet]
        public QueryData<DBLog> Get([FromQuery]QuerySQLOption value)
        {
            return value.RetrieveData();
        }
    }
}
