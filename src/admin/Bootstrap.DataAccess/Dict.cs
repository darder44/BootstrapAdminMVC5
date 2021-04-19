using Bootstrap.Security;
using Bootstrap.Security.DataAccess;
using Longbow;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bootstrap.DataAccess
{
    /// <summary>
    /// 字典表實體類別
    /// </summary>
    [TableName("Dicts")]
    public class Dict : BootstrapDict
    {
        /// <summary>
        /// 删除字典中的資料
        /// </summary>
        /// <param name="value">需要删除的IDs</param>
        /// <returns></returns>
        public virtual bool Delete(IEnumerable<string> value)
        {
            if (!value.Any()) return true;
            var ids = string.Join(",", value);
            string sql = $"where ID in ({ids})";
            using var db = DbManager.Create();
            db.Delete<BootstrapDict>(sql);
            return true;
        }

        /// <summary>
        /// 保存新建/更新的字典訊息
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public virtual bool Save(BootstrapDict dict)
        {
            if (dict.Category.Length > 50) dict.Category = dict.Category.Substring(0, 50);
            if (dict.Name.Length > 50) dict.Name = dict.Name.Substring(0, 50);
            if (dict.Code.Length > 2000) dict.Code = dict.Code.Substring(0, 2000);

            using var db = DbManager.Create();
            db.Save(dict);
            return true;
        }

        /// <summary>
        /// 保存網站個性化設置
        /// </summary>
        /// <param name="dicts"></param>
        /// <returns></returns>
        public virtual bool SaveSettings(IEnumerable<BootstrapDict> dicts)
        {
            using var db = DbManager.Create();
            dicts.ToList().ForEach(dict => db.Update<Dict>("set Code = @Code where Category = @Category and Name = @Name", dict));
            return true;
        }

        /// <summary>
        /// 获取字典分類別名稱
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<string> RetrieveCategories() => DictHelper.RetrieveDicts().OrderBy(d => d.Category).Select(d => d.Category).Distinct();

        /// <summary>
        /// 获取系统網站标题
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public virtual string RetrieveWebTitle(string appId)
        {
            // 优先查找配置的應用程序網站标题
            var code = DbHelper.RetrieveTitle(appId);
            if (code == "網站标题未設置") code = DictHelper.RetrieveDicts().FirstOrDefault(d => d.Name == "網站标题" && d.Category == "網站設置" && d.Define == 0)?.Code ?? "後台管理系统";
            return code;
        }

        /// <summary>
        /// 获取系统網站頁脚
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public virtual string RetrieveWebFooter(string appId)
        {
            // 优先查找配置的應用程序網站标题
            var code = DbHelper.RetrieveFooter(appId);
            if (code == "網站頁脚未設置") code = DictHelper.RetrieveDicts().FirstOrDefault(d => d.Name == "網站頁脚" && d.Category == "網站設置" && d.Define == 0)?.Code ?? "2016 © 通用後台管理系统";
            return code;
        }

        /// <summary>
        /// 獲得系统中配置的可以使用的網站样式
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<BootstrapDict> RetrieveThemes() => DictHelper.RetrieveDicts().Where(d => d.Category == "網站样式");

        /// <summary>
        /// 獲得網站設置中的当前样式
        /// </summary>
        /// <returns></returns>
        public virtual string RetrieveActiveTheme()
        {
            var theme = DictHelper.RetrieveDicts().FirstOrDefault(d => d.Name == "使用样式" && d.Category == "網站設置" && d.Define == 0);
            return theme == null ? string.Empty : (theme.Code.Equals("site.css", StringComparison.OrdinalIgnoreCase) ? string.Empty : theme.Code);
        }

        /// <summary>
        /// 获取头像路径
        /// </summary>
        /// <returns></returns>
        public virtual string? RetrieveIconFolderPath() => DictHelper.RetrieveDicts().FirstOrDefault(d => d.Name == "头像路径" && d.Category == "头像地址" && d.Define == 0)?.Code;

        /// <summary>
        /// 獲得默认的前台首頁地址，默认為~/Home/Index
        /// </summary>
        /// <param name="userName">登錄用户名</param>
        /// <param name="appId">默认應用程序编码</param>
        /// <returns></returns>
        public virtual string RetrieveHomeUrl(string? userName, string appId)
        {
            // https://gitee.com/LongbowEnterprise/dashboard/issues?id=IS0WK
            // https://gitee.com/LongbowEnterprise/dashboard/issues?id=I17SD0
            var url = "~/Admin/Index";
            var dicts = DictHelper.RetrieveDicts();

            if (appId.IsNullOrEmpty())
            {
                var defaultUrl = dicts.FirstOrDefault(d => d.Name == "前台首頁" && d.Category == "網站設置" && d.Define == 0)?.Code;
                if (!string.IsNullOrEmpty(defaultUrl)) url = defaultUrl;
            }
            else if (appId.Equals("BA", StringComparison.OrdinalIgnoreCase))
            {
                // 使用配置项設置是否启用默认第一個應用是默认應用
                var defaultApp = (dicts.FirstOrDefault(d => d.Name == "默认應用程序" && d.Category == "網站設置" && d.Define == 0)?.Code ?? "0") == "1";
                if (defaultApp)
                {
                    var app = AppHelper.RetrievesByUserName(userName).FirstOrDefault(key => !key.Equals("BA", StringComparison.OrdinalIgnoreCase)) ?? "";
                    if (!string.IsNullOrEmpty(app))
                    {
                        // 指定應用程序的首頁
                        var appUrl = RetrieveDefaultHomeUrlByApp(dicts, app);
                        if (!string.IsNullOrEmpty(appUrl)) url = appUrl;
                    }
                }
            }
            else
            {
                // 指定應用程序的首頁
                var appUrl = RetrieveDefaultHomeUrlByApp(dicts, appId);
                if (!string.IsNullOrEmpty(appUrl)) url = appUrl;
            }
            return url;
        }

        /// <summary>
        /// 通过 appId 获取應用首頁配置值
        /// </summary>
        /// <param name="dicts"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        protected virtual string RetrieveDefaultHomeUrlByApp(IEnumerable<BootstrapDict> dicts, string appId)
        {
            return dicts.FirstOrDefault(d => d.Name.Equals(appId, StringComparison.OrdinalIgnoreCase) && d.Category == "應用首頁" && d.Define == 0)?.Code ?? "";
        }

        /// <summary>
        /// 獲得字典表中配置的所有應用程序
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<KeyValuePair<string, string>> RetrieveApps() => DictHelper.RetrieveDicts().Where(d => d.Category == "應用程序" && d.Define == 0).Select(d => new KeyValuePair<string, string>(d.Code, d.Name)).OrderBy(d => d.Key);

        /// <summary>
        /// 通过資料庫獲得所有字典表配置訊息，快取Key=DictHelper-RetrieveDicts
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<BootstrapDict> RetrieveDicts() => DbHelper.RetrieveDicts();

        /// <summary>
        /// 程序異常時長 默认1月
        /// </summary>
        /// <returns></returns>
        public int RetrieveExceptionsLogPeriod() => LgbConvert.ReadValue(DictHelper.RetrieveDicts().FirstOrDefault(d => d.Category == "網站設置" && d.Name == "程序異常保留時長" && d.Define == 0)?.Code, 1);

        /// <summary>
        /// 操作日誌時長 默认12月
        /// </summary>
        /// <returns></returns>
        public int RetrieveLogsPeriod() => LgbConvert.ReadValue(DictHelper.RetrieveDicts().FirstOrDefault(d => d.Category == "網站設置" && d.Name == "操作日誌保留時長" && d.Define == 0)?.Code, 12);

        /// <summary>
        /// 登錄日誌時長 默认12月
        /// </summary>
        /// <returns></returns>
        public int RetrieveLoginLogsPeriod() => LgbConvert.ReadValue(DictHelper.RetrieveDicts().FirstOrDefault(d => d.Category == "網站設置" && d.Name == "登錄日誌保留時長" && d.Define == 0)?.Code, 12);

        /// <summary>
        /// Cookie保存時長 默认7天
        /// </summary>
        /// <returns></returns>
        public int RetrieveCookieExpiresPeriod() => LgbConvert.ReadValue(DictHelper.RetrieveDicts().FirstOrDefault(d => d.Category == "網站設置" && d.Name == "Cookie保留時長" && d.Define == 0)?.Code, 7);

        /// <summary>
        /// 獲得 IP地理位置
        /// </summary>
        /// <returns></returns>
        public string RetrieveLocaleIPSvr() => DictHelper.RetrieveDicts().FirstOrDefault(d => d.Category == "網站設置" && d.Name == "IP地理位置接口" && d.Define == 0)?.Code ?? string.Empty;

        /// <summary>
        /// 獲得 IP請求快取時長配置值
        /// </summary>
        /// <returns></returns>
        public int RetrieveLocaleIPSvrCachePeriod()
        {
            var period = DictHelper.RetrieveDicts().FirstOrDefault(d => d.Category == "網站設置" && d.Name == "IP請求快取時長" && d.Define == 0)?.Code;
            var ret = 10;
            if (!string.IsNullOrEmpty(period) && int.TryParse(period, out var svrPeriod)) ret = svrPeriod;
            return ret;
        }

        /// <summary>
        /// 獲得 项目是否获取登錄地点 默认為false
        /// </summary>
        /// <param name="ipSvr">服务提供名稱</param>
        /// <returns></returns>
        public string? RetrieveLocaleIPSvrUrl(string ipSvr) => DictHelper.RetrieveDicts().FirstOrDefault(d => d.Category == "地理位置" && d.Name == ipSvr && d.Define == 0)?.Code;

        /// <summary>
        /// 獲得 访问日誌保留時長 默认為1個月
        /// </summary>
        /// <returns></returns>
        public int RetrieveAccessLogPeriod() => LgbConvert.ReadValue(DictHelper.RetrieveDicts().FirstOrDefault(d => d.Category == "網站設置" && d.Name == "访问日誌保留時長" && d.Define == 0)?.Code, 1);

        /// <summary>
        /// 獲得 是否為演示系统 默认為 false 不是演示系统
        /// </summary>
        /// <returns></returns>
        public bool RetrieveSystemModel() => LgbConvert.ReadValue(DictHelper.RetrieveDicts().FirstOrDefault(d => d.Category == "網站設置" && d.Name == "演示系统" && d.Define == 0)?.Code, "0") == "1";

        /// <summary>
        /// 獲得 是否為演示系统 默认為 false 不是演示系统
        /// </summary>
        /// <returns></returns>
        public bool UpdateSystemModel(bool isDemo)
        {
            var dict = DictHelper.RetrieveDicts().FirstOrDefault(d => d.Category == "網站設置" && d.Name == "演示系统" && d.Define == 0);
            dict!.Code = isDemo ? "1" : "0";
            return Save(dict);
        }

        /// <summary>
        /// 獲得 验证码图床地址
        /// </summary>
        /// <returns></returns>
        public string RetrieveImagesLibUrl() => DictHelper.RetrieveDicts().FirstOrDefault(d => d.Category == "網站設置" && d.Name == "验证码图床" && d.Define == 0)?.Code ?? "http://images.sdgxgz.com/";

        /// <summary>
        /// 獲得 資料庫标题是否显示
        /// </summary>
        /// <returns></returns>
        public bool RetrieveCardTitleStatus() => (DictHelper.RetrieveDicts().FirstOrDefault(d => d.Category == "網站設置" && d.Name == "卡片标题状態" && d.Define == 0)?.Code ?? "1") == "1";

        /// <summary>
        /// 獲得 是否显示侧边栏 為真時显示
        /// </summary>
        /// <returns></returns>
        public bool RetrieveSidebarStatus() => (DictHelper.RetrieveDicts().FirstOrDefault(d => d.Category == "網站設置" && d.Name == "侧边栏状態" && d.Define == 0)?.Code ?? "1") == "1";

        /// <summary>
        /// 獲得是否允许短信验证码登錄
        /// </summary>
        /// <returns></returns>
        public bool RetrieveMobileLogin() => (DictHelper.RetrieveDicts().FirstOrDefault(d => d.Category == "網站設置" && d.Name == "短信验证码登錄" && d.Define == 0)?.Code ?? "1") == "1";

        /// <summary>
        /// 獲得是否允许 OAuth 认证登錄
        /// </summary>
        /// <returns></returns>
        public bool RetrieveOAuthLogin() => (DictHelper.RetrieveDicts().FirstOrDefault(d => d.Category == "網站設置" && d.Name == "OAuth 认证登錄" && d.Define == 0)?.Code ?? "1") == "1";

        /// <summary>
        /// 獲得自動锁屏時長 默认 30 秒
        /// </summary>
        /// <returns></returns>
        public int RetrieveAutoLockScreenPeriod() => LgbConvert.ReadValue(DictHelper.RetrieveDicts().FirstOrDefault(d => d.Category == "網站設置" && d.Name == "自動锁屏時長" && d.Define == 0)?.Code, 30);

        /// <summary>
        /// 獲得自動锁屏是否开启 默认关閉
        /// </summary>
        /// <returns></returns>
        public bool RetrieveAutoLockScreen() => (DictHelper.RetrieveDicts().FirstOrDefault(d => d.Category == "網站設置" && d.Name == "自動锁屏" && d.Define == 0)?.Code ?? "0") == "1";

        /// <summary>
        /// 獲得默认應用是否开启 默认关閉
        /// </summary>
        /// <returns></returns>
        public bool RetrieveDefaultApp() => (DictHelper.RetrieveDicts().FirstOrDefault(d => d.Category == "網站設置" && d.Name == "默认應用程序" && d.Define == 0)?.Code ?? "0") == "1";

        /// <summary>
        /// 獲得是否开启 Blazor 功能 默认关閉
        /// </summary>
        /// <returns></returns>
        public bool RetrieveEnableBlazor() => (DictHelper.RetrieveDicts().FirstOrDefault(d => d.Category == "網站設置" && d.Name == "Blazor" && d.Define == 0)?.Code ?? "0") == "1";

        /// <summary>
        /// 獲得是否开启 固定表头 功能 默认开启
        /// </summary>
        /// <returns></returns>
        public bool RetrieveFixedTableHeader() => (DictHelper.RetrieveDicts().FirstOrDefault(d => d.Category == "網站設置" && d.Name == "固定表头" && d.Define == 0)?.Code ?? "1") == "1";

        /// <summary>
        /// 獲得字典表地理位置配置訊息集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BootstrapDict> RetireveLocators() => DictHelper.RetrieveDicts().Where(d => d.Category == "地理位置服务");

        /// <summary>
        /// 獲得個人中心地址
        /// </summary>
        /// <returns></returns>
        public string RetrievePathBase() => DictHelper.RetrieveDicts().FirstOrDefault(d => d.Category == "網站設置" && d.Name == "後台地址" && d.Define == 0)?.Code ?? "";

        /// <summary>
        /// 獲得字典表健康檢查是否开启
        /// </summary>
        /// <returns></returns>
        public bool RetrieveHealth() => (DictHelper.RetrieveDicts().FirstOrDefault(d => d.Category == "網站設置" && d.Name == "健康檢查" && d.Define == 0)?.Code ?? "0") == "1";

        /// <summary>
        /// 獲得字典表登錄界面資料
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BootstrapDict> RetrieveLogins() => DictHelper.RetrieveDicts().Where(d => d.Category == "系统首頁" && d.Define == 1);

        /// <summary>
        /// 獲得使用中的登錄视图名稱
        /// </summary>
        /// <returns></returns>
        public string? RetrieveLoginView() => DictHelper.RetrieveDicts().FirstOrDefault(d => d.Category == "網站設置" && d.Name == "登錄界面" && d.Define == 1)?.Code;
    }
}
