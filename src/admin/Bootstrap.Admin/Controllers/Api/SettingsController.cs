using Bootstrap.DataAccess;
using Bootstrap.Security;
using Longbow.Cache;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;

namespace Bootstrap.Admin.Controllers.Api
{
    /// <summary>
    /// 網站設置控制器
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        /// <summary>
        /// 保存網站設置方法
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        [HttpPost]
        [ButtonAuthorize(Url = "~/Admin/Settings", Auth = "saveTitle,saveFooter,saveTheme,saveUISettings,loginSettings,lockScreen,defaultApp,blazor,iplocate,logSettings")]
        public bool Post([FromBody]IEnumerable<BootstrapDict> values) => DictHelper.SaveUISettings(values);

        /// <summary>
        /// 保存網站是否為Demo系統時調用
        /// </summary>
        /// <returns></returns>
        [HttpPost("{id}")]
        public bool Post(string id, [FromBody]BootstrapDict dict) => id switch
        {
            "Demo" => DictHelper.UpdateSystemModel(dict.Code == "1", dict.Name),
            _ => false
        };

        /// <summary>
        /// 保存前台應用時調用
        /// </summary>
        /// <returns></returns>
        [HttpPut()]
        public bool Put([FromBody]QueryAppOption option) => option.Save();

        /// <summary>
        /// 獲取網站缓存站點集合
        /// </summary>
        [HttpGet]
        public IEnumerable<ICacheCorsItem> Get() => CacheManager.CorsSites;

        /// <summary>
        /// 通過指定 AppKey 獲取前台應用配置信息
        /// </summary>
        /// <param name="key"></param>
        [HttpGet("{key}")]
        public QueryAppOption Get(string key) => QueryAppOption.RetrieveByKey(key);

        /// <summary>
        /// 刪除指定鍵值的前台應用配置信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dict"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public bool Delete(string id, [FromBody]BootstrapDict dict) => id switch
        {
            "AppPath" => DictHelper.DeleteApp(dict),
            _ => false
        };
    }
}
