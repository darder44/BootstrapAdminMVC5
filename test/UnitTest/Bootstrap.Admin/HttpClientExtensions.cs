using System.Net.Http;
using System.Threading.Tasks;

namespace Bootstrap.Admin
{
    /// <summary>
    /// HttpClient 擴展操作類
    /// </summary>
    public static class HttpClientExtensions
    {
        /// <summary>
        /// LoginAsync 異步方法
        /// </summary>
        /// <param name="client"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static async Task LoginAsync(this HttpClient client, string userName = "Admin", string password = "123789")
        {
            var r = await client.GetAsync("/Account/Login");
            var view = await r.Content.ReadAsStringAsync();
            var tokenTag = "<input name=\"__RequestVerificationToken\" type=\"hidden\" value=\"";
            var index = view.IndexOf(tokenTag);
            view = view.Substring(index + tokenTag.Length);
            index = view.IndexOf("\" /></form>");
            var antiToken = view.Substring(0, index);

            var content = new MultipartFormDataContent
            {
                { new StringContent(userName), "userName" },
                { new StringContent(password), "password" },
                { new StringContent("true"), "remember" },
                { new StringContent(antiToken), "__RequestVerificationToken" }
            };
            await client.PostAsync("/Account/Login", content);
        }
    }
}
