using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GreenEconomy.Core.Models;
using Newtonsoft.Json;

namespace GreenEconomy.Core.Services
{
    public interface IWebClient
    {
        Task<List<T>> GetAsync<T>();
    }

    public class WebClient : IWebClient
    {
        private readonly HttpClient HttpClient;

        public WebClient(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }
        private const string BaseUrl = "https://greeneconomy.azurewebsites.net/api";
        public async Task<List<T>> GetAsync<T>()
        {
            var url = $"{BaseUrl}/businesses";
            var res = await HttpClient.GetStringAsync(url);
            var items = JsonConvert.DeserializeObject<List<T>>(res);
            return items;
        }
    }
}
