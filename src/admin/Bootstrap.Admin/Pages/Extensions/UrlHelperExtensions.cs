﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Bootstrap.Admin.Pages.Extensions
{
    /// <summary>
    /// Url 地址輔助操作類
    /// </summary>
    public static class UrlHelperExtensions
    {
        /// <summary>
        /// 轉換為 Blazor 地址 ~/Admin/Index => Admin/Index
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string ToBlazorLink(this string url) => (url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || url.StartsWith("https://", StringComparison.OrdinalIgnoreCase)) ? url : url.Replace("~/", "");
         
        /// <summary>
        /// 轉化為 Blazor 選單地址 ~/Admin/Index => Pages/Admin/Index
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string ToBlazorMenuUrl(this string url) => url.Replace("~/", "Pages/");

        /// <summary>
        /// 轉化為 Mvc 選單地址 /Pages/Admin/Index => ~/Admin/Index
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string ToMvcMenuUrl(this string url)
        {
            var index = new List<string>() { "/Pages", "/Pages/Admin" };
            return index.Any(u => u.Contains(url, System.StringComparison.OrdinalIgnoreCase)) ? "~/Admin/Index" : url.Replace("/Pages/", "~/");
        }
    }
}
