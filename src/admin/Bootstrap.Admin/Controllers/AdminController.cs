using Bootstrap.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Bootstrap.Admin.Controllers
{
    /// <summary>
    /// 後台管理控制器
    /// </summary>
    [Authorize]
    public class AdminController : Controller
    {
        /// <summary>
        /// 後台管理首頁
        /// </summary>
        /// <returns></returns>
        public ActionResult Index() => View(new NavigatorBarModel(this));

        /// <summary>
        /// 用户維護
        /// </summary>
        /// <returns></returns>
        public ActionResult Users() => View(new NavigatorBarModel(this));

        /// <summary>
        /// 部門維護
        /// </summary>
        /// <returns></returns>
        public ActionResult Groups() => View(new NavigatorBarModel(this));

        /// <summary>
        /// 字典表維護
        /// </summary>
        /// <returns></returns>
        public ActionResult Dicts() => View(new NavigatorBarModel(this));

        /// <summary>
        /// 角色維護
        /// </summary>
        /// <returns></returns>
        public ActionResult Roles() => View(new NavigatorBarModel(this));

        /// <summary>
        /// 選單維護
        /// </summary>
        /// <returns></returns>
        public ActionResult Menus() => View(new NavigatorBarModel(this));

        /// <summary>
        /// 操作日誌
        /// </summary>
        /// <returns></returns>
        public ActionResult Logs() => View(new NavigatorBarModel(this));

        /// <summary>
        /// 脚本日誌
        /// </summary>
        /// <returns></returns>
        public ActionResult SQL() => View(new NavigatorBarModel(this));

        /// <summary>
        /// 訪問日誌
        /// </summary>
        /// <returns></returns>
        public ActionResult Traces() => View(new NavigatorBarModel(this));

        /// <summary>
        /// 登入日誌
        /// </summary>
        /// <returns></returns>
        public ActionResult Logins() => View(new NavigatorBarModel(this));

        /// <summary>
        /// FA 圖標頁面
        /// </summary>
        /// <returns></returns>
        public ActionResult FAIcon() => View(new NavigatorBarModel(this));

        /// <summary>
        /// 健康檢查
        /// </summary>
        /// <returns></returns>
        public ActionResult Healths() => View(new NavigatorBarModel(this));

        /// <summary>
        /// 圖標視圖
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [ResponseCache(Duration = 600)]
        public PartialViewResult IconView() => PartialView("IconView");

        /// <summary>
        /// 側邊欄局部視圖
        /// </summary>
        /// <returns></returns>
        /// <remark>選單維護頁面增刪選單時局部刷新時調用</remark>
        public PartialViewResult Sidebar() => PartialView("Sidebar", new NavigatorBarModel(this));

        /// <summary>
        /// 網站設置
        /// </summary>
        /// <returns></returns>
        public ActionResult Settings() => View(new SettingsModel(this));

        /// <summary>
        /// 通知管理
        /// </summary>
        /// <returns></returns>
        public ActionResult Notifications() => View(new NavigatorBarModel(this));

        /// <summary>
        /// 個人中心
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public ActionResult Profiles([FromServices]IWebHostEnvironment host) => View(new ProfilesModel(this, host));

        /// <summary>
        /// 程式异常
        /// </summary>
        /// <returns></returns>
        public ActionResult Exceptions() => View(new NavigatorBarModel(this));

        /// <summary>
        /// 訊息通知
        /// </summary>
        /// <returns></returns>
        public ActionResult Messages() => View(new NavigatorBarModel(this));

        /// <summary>
        /// 任務管理
        /// </summary>
        /// <returns></returns>
        public ActionResult Tasks() => View(new TaskModel(this));

        /// <summary>
        /// 客户端測試頁面
        /// </summary>
        /// <returns></returns>
        public ActionResult Mobile() => View(new NavigatorBarModel(this));

        /// <summary>
        /// 線上用户
        /// </summary>
        /// <returns></returns>
        public ActionResult Online() => View(new NavigatorBarModel(this));

        /// <summary>
        /// 網站分析統计
        /// </summary>
        /// <returns></returns>
        public ActionResult Analyse() => View(new NavigatorBarModel(this));

        /// <summary>
        /// 用於測試ExceptionFilter
        /// </summary>
        /// <returns></returns>
        public ActionResult Error() => throw new Exception("Customer Excetion UnitTest");
    }
}
