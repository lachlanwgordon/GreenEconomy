using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GreenEconomy.Core.Models;
using Newtonsoft.Json;

namespace GreenEconomy.Core.Services
{
    public interface IWebClient
    {
        Task<List<T>> GetAsync<T>() where T : BaseModel;
        Task<T> PostAsync<T>(T model) where T : BaseModel;
    }

    public class WebClient : IWebClient
    {
        private readonly HttpClient HttpClient;

        public WebClient(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }
        private const string BaseUrl = "https://greeneconomy.azurewebsites.net/api";
        //private const string BaseUrl = "http://localhost:7071/api";
        public async Task<List<T>> GetAsync<T>() where T : BaseModel
        {
            var items = new List<T>();
            try
            {

                var tableName = typeof(T).Name;
                var url = $"{BaseUrl}/get?table={tableName}";
                var res = await HttpClient.GetStringAsync(url);
                items = JsonConvert.DeserializeObject<List<T>>(res);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{ex}{ex.StackTrace}");

            }
            return items;

        }

        public async Task<T> PostAsync<T>(T model) where T : BaseModel
        {
            try
            {

                var tableName = typeof(T).Name;
                var url = $"{BaseUrl}/post?table={tableName}";
                var serialized = JsonConvert.SerializeObject(model);
                var content = new StringContent(serialized, Encoding.UTF8, "appication/json");
                var res = await HttpClient.PostAsync(url, content);
                var resultString = await res.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<T>(resultString);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{ex}{ex.StackTrace}");
            }
            return model;
        }
    }
}
