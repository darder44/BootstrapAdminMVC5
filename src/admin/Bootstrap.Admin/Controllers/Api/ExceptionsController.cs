using Bootstrap.Admin.Query;
using Bootstrap.DataAccess;
using Longbow.Web.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bootstrap.Admin.Controllers.Api
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ExceptionsController : ControllerBase
    {
        /// <summary>
        /// 顯示所有異常
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpGet]
        public QueryData<object> Get([FromQuery]QueryExceptionOption value)
        {
            return value.Retrieves();
        }

        /// <summary>
        /// 異常程式頁面点击服務器日誌按鈕獲取所有物理日誌文件列表方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ButtonAuthorize(Url = "~/Admin/Exceptions", Auth = "log")]
        public IEnumerable<string> Post() => ExceptionsHelper.RetrieveLogFiles();

        /// <summary>
        /// 选中指定文件查看其内容方法
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ButtonAuthorize(Url = "~/Admin/Exceptions", Auth = "log")]
        public JsonResult Put([FromBody]ExceptionFileQuery exceptionFile)
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, "Error");
            var logName = $"{Path.Combine(filePath, exceptionFile.FileName)}.log";
            var sb = new StringBuilder();
            if (System.IO.File.Exists(logName))
            {
                using var reader = new StreamReader(logName);
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine()?.Replace("<", "&lt;").Replace(">", "&gt;");
                    if (!string.IsNullOrEmpty(line))
                    {
                        if (line == "General Information ") sb.AppendFormat("<h4><b>{0}</b></h4>", line);
                        else if (line.StartsWith("TimeStamp:")) sb.AppendFormat("<div class='logTs'>{0}</div>", line);
                        else if (line.EndsWith("Exception Information")) sb.AppendFormat("<div class='logExcep'>{0}</div>", line);
                        else if (line.StartsWith("Message:")) sb.AppendFormat("<div class='logMsg'>{0}</div>", line);
                        else if (line.StartsWith("ErrorSql:")) sb.AppendFormat("<div class='logSql'>{0}</div>", line);
                        else if (line.StartsWith("Exception Type: Longbow.Data.DBAccessException")) sb.AppendFormat("<div class='logDbExcep'>{0}</div>", line);
                        else if (line.StartsWith("StackTrace Information")) sb.AppendFormat("<b>{0}</b><br>", line);
                        else sb.AppendFormat("{0}<br>", line);
                    }
                }
            }
            return new JsonResult(sb.ToString());
        }

        /// <summary>
        /// 查詢服務器端日誌文件参数類
        /// </summary>
        public class ExceptionFileQuery
        {
            /// <summary>
            /// 獲取/設置 文件名稱
            /// </summary>
            public string FileName { get; set; } = "";
        }
    }
}
