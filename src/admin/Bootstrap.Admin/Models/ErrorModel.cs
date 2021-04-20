namespace Bootstrap.Admin.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ErrorModel : ModelBase
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        public string Content { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        public string Image { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>

        public string Detail { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        public string ReturnUrl { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ErrorModel CreateById(int id)
        {
            var model = new ErrorModel
            {
                Id = id,
                Title = "服務器内部錯誤",
                Content = "服務器内部錯誤",
                Detail = "相關錯誤信息已经紀錄到日誌中，請登入服務器或後台管理中查看",
                Image = "~/images/error_icon.png",
                ReturnUrl = "~/Admin/Index"
            };

            switch (id)
            {
                case 0:
                    model.Content = "未處理服務器内部錯誤";
                    break;
                case 404:
                    model.Title = "資源未找到";
                    model.Content = "請求資源未找到";
                    model.Image = "~/images/404_icon.png";
                    break;
                case 403:
                    model.Title = "未授權請求";
                    model.Content = "您的訪問受限！";
                    model.Detail = "服務器拒绝處理您的請求！您可能没有訪問此操作的權限";
                    break;
            }
            return model;
        }
    }
}
