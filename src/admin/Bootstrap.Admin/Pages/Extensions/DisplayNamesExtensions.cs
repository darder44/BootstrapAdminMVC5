using Bootstrap.DataAccess;
using Bootstrap.Security;
using Longbow.Cache;
using Longbow.Web;
using System;
using System.Collections.Concurrent;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 顯示名稱扩展方法類
    /// </summary>
    public static class DisplayNamesExtensions
    {

        private static readonly ConcurrentDictionary<(Type ModelType, string FieldName), string> _displayNameCache = new ConcurrentDictionary<(Type, string), string>();

        /// <summary>
        /// 向系統中加入實體類顯示名稱字典
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDisplayNames(this IServiceCollection services)
        {
            _displayNameCache.TryAdd((typeof(BootstrapDict), nameof(BootstrapDict.Category)), "字典標簽");
            _displayNameCache.TryAdd((typeof(BootstrapDict), nameof(BootstrapDict.Name)), "字典名稱");
            _displayNameCache.TryAdd((typeof(BootstrapDict), nameof(BootstrapDict.Code)), "字典程式碼");
            _displayNameCache.TryAdd((typeof(BootstrapDict), nameof(BootstrapDict.Define)), "字典類型");

            _displayNameCache.TryAdd((typeof(User), nameof(User.UserName)), "登入名稱");
            _displayNameCache.TryAdd((typeof(User), nameof(User.DisplayName)), "顯示名稱");

            _displayNameCache.TryAdd((typeof(Group), nameof(Group.GroupCode)), "部門編碼");
            _displayNameCache.TryAdd((typeof(Group), nameof(Group.GroupName)), "部門名稱");
            _displayNameCache.TryAdd((typeof(Group), nameof(Group.Description)), "部門描述");

            _displayNameCache.TryAdd((typeof(BootstrapMenu), nameof(BootstrapMenu.Name)), "選單名稱");
            _displayNameCache.TryAdd((typeof(BootstrapMenu), nameof(BootstrapMenu.ParentName)), "父級選單");
            _displayNameCache.TryAdd((typeof(BootstrapMenu), nameof(BootstrapMenu.Order)), "選單序号");
            _displayNameCache.TryAdd((typeof(BootstrapMenu), nameof(BootstrapMenu.Icon)), "選單圖標");
            _displayNameCache.TryAdd((typeof(BootstrapMenu), nameof(BootstrapMenu.Url)), "選單路徑");
            _displayNameCache.TryAdd((typeof(BootstrapMenu), nameof(BootstrapMenu.Category)), "選單類别");
            _displayNameCache.TryAdd((typeof(BootstrapMenu), nameof(BootstrapMenu.CategoryName)), "選單類别");
            _displayNameCache.TryAdd((typeof(BootstrapMenu), nameof(BootstrapMenu.Target)), "目標");
            _displayNameCache.TryAdd((typeof(BootstrapMenu), nameof(BootstrapMenu.IsResource)), "選單類型");
            _displayNameCache.TryAdd((typeof(BootstrapMenu), nameof(BootstrapMenu.Application)), "所属應用");

            // 缓存顯示名稱
            _displayNameCache.TryAdd((typeof(CacheItem), nameof(CacheItem.Key)), "缓存 Key");
            _displayNameCache.TryAdd((typeof(CacheItem), nameof(CacheItem.Value)), "缓存值");
            _displayNameCache.TryAdd((typeof(CacheItem), nameof(CacheItem.Interval)), "缓存時長（秒）");
            _displayNameCache.TryAdd((typeof(CacheItem), nameof(CacheItem.ElapsedSeconds)), "已過時長（秒）");
            _displayNameCache.TryAdd((typeof(CacheItem), nameof(CacheItem.Desc)), "缓存说明");

            _displayNameCache.TryAdd((typeof(OnlineUser), nameof(OnlineUser.ConnectionId)), "會话Id");
            _displayNameCache.TryAdd((typeof(OnlineUser), nameof(OnlineUser.UserName)), "登入名稱");
            _displayNameCache.TryAdd((typeof(OnlineUser), nameof(OnlineUser.DisplayName)), "顯示名稱");
            _displayNameCache.TryAdd((typeof(OnlineUser), nameof(OnlineUser.FirstAccessTime)), "登入時间");
            _displayNameCache.TryAdd((typeof(OnlineUser), nameof(OnlineUser.LastAccessTime)), "訪問時间");
            _displayNameCache.TryAdd((typeof(OnlineUser), nameof(OnlineUser.Method)), "請求方式");
            _displayNameCache.TryAdd((typeof(OnlineUser), nameof(OnlineUser.Ip)), "主机");
            _displayNameCache.TryAdd((typeof(OnlineUser), nameof(OnlineUser.Location)), "登入地点");
            _displayNameCache.TryAdd((typeof(OnlineUser), nameof(OnlineUser.Browser)), "浏览器");
            _displayNameCache.TryAdd((typeof(OnlineUser), nameof(OnlineUser.OS)), "操作系統");
            _displayNameCache.TryAdd((typeof(OnlineUser), nameof(OnlineUser.RequestUrl)), "訪問地址");

            return services;
        }

        /// <summary>
        /// 嘗試獲取指定 Model 指定属性值的顯示名稱
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="displayName"></param>
        /// <returns></returns>
        public static bool TryGetValue((Type ModelType, string FieldName) cacheKey, out string? displayName) => _displayNameCache.TryGetValue(cacheKey, out displayName);

        /// <summary>
        /// 獲得或者添加指定 Model 的指定属性值得顯示名稱
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="valueFactory"></param>
        /// <returns></returns>
        public static string GetOrAdd((Type ModelType, string FieldName) cacheKey, Func<(Type, string), string> valueFactory) => _displayNameCache.GetOrAdd(cacheKey, valueFactory);
    }
}
