using Bootstrap.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Bootstrap.Admin.Controllers.Api
{
    /// <summary>
    /// 資料字典分类
    /// </summary>
    [Route("api/[controller]/[action]")]
    [Authorize]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        /// <summary>
        /// 獲取字典表中所有 Category 資料
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<string> RetrieveDictCategorys()
        {
            return DictHelper.RetrieveCategories();
        }

        /// <summary>
        /// 獲取所有選單資料
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<string> RetrieveMenus()
        {
            return MenuHelper.RetrieveAllMenus(User.Identity!.Name).OrderBy(m => m.Name).Select(m => m.Name);
        }

        /// <summary>
        /// 獲取所有父级選單資料
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<string> RetrieveParentMenus()
        {
            return MenuHelper.RetrieveMenus(User.Identity!.Name).Where(m => m.Menus.Count() > 0).OrderBy(m => m.Name).Select(m => m.Name);
        }

        /// <summary>
        /// 通過指定選單檢查子選單是否有子選單
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public bool ValidateMenuBySubMenu(string id)
        {
            return !MenuHelper.RetrieveAllMenus(User.Identity!.Name).Where(m => m.ParentId == id).Any();
        }

        /// <summary>
        /// 通過指定選單檢查父级選單是否為選單类型 资源与按钮返回 false
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public bool ValidateParentMenuById(string id)
        {
            return MenuHelper.RetrieveAllMenus(User.Identity!.Name).FirstOrDefault(m => m.Id == id)?.IsResource == 0;
        }
    }
}
