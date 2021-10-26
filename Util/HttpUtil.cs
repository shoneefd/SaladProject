using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace SaladProject.Util
{
    public static class HttpUtil
    {
        private static readonly HttpClient Client = new HttpClient();
        private static string _url = "https://api.rawg.io/api/games/";
        private static readonly string _key = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "api_key.txt"));
        static HttpUtil()
        {
            Client.BaseAddress = new Uri(_url);
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );
            Client.DefaultRequestHeaders.Add("key", _key);
        }
        public async static Task<string> QueryGames(string query, string sort)
        {
            HttpRequestMessage request = new HttpRequestMessage() {
                Method = HttpMethod.Get
            };
            if (!String.IsNullOrEmpty(query))
            {
                request.Headers.Add("search", query);
            }
            if (!String.IsNullOrEmpty(sort))
            {
                request.Headers.Add("ordering", sort);
            }
            HttpResponseMessage response = await Client.SendAsync(request);
            return await response.Content.ReadAsStringAsync();
        }
        public async static Task<string> QueryGame(string gameId)
        {
            return await Client.GetStringAsync(gameId);
        }
    }
}