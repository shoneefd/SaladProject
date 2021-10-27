using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;


namespace SaladProject.Util
{
    public static class HttpUtil
    {
        private static readonly HttpClient Client = new HttpClient();
        private static string _url = "https://api.rawg.io/api/";
        private static readonly string _key = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "api_key.txt"));
        static HttpUtil()
        {
            Client.BaseAddress = new Uri(_url);
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );
        }
        public async static Task<string> QueryGames(string query, string sort)
        {
            var queryString = HttpUtility.ParseQueryString("");
            queryString["key"] = _key;
            if (!String.IsNullOrEmpty(query))
            {
                queryString["search"] = query;
            }
            if (!String.IsNullOrEmpty(sort))
            {
                queryString["ordering"] = sort;
            }
            string queryFull = "games?" + queryString.ToString();
            await File.WriteAllTextAsync(Path.Combine(Environment.CurrentDirectory, "log.txt"), queryFull);
            return await Client.GetStringAsync(queryFull);
        }
        public async static Task<string> QueryGame(string gameId)
        {
            var queryString = HttpUtility.ParseQueryString("");
            queryString["key"] = _key;
            string queryFull = $"games/{gameId}?" + queryString.ToString();
            return await Client.GetStringAsync(queryFull);
        }
    }
}