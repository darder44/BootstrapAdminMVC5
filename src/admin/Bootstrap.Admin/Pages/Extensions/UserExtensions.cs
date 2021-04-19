using Bootstrap.DataAccess;

namespace Bootstrap.Admin.Pages.Extensions
{
    /// <summary>
    /// 獲得 用户顯示名稱
    /// </summary>
    public static class UserExtensions
    {
        /// <summary>
        /// 獲得 用户顯示名稱
        /// </summary>
        public static string FormatDisplayName(this User user)
        {
            var displayName = user.DisplayName;
            if (string.IsNullOrEmpty(displayName)) displayName = user.UserName;
            return displayName;
        }
    }
}
