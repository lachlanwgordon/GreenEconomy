using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using GreenEconomy.Core.Models;

using GreenEconomy.Core.Services;
using System.Linq;
using Microsoft.Azure.Cosmos.Table;
using System.Net.Http;

namespace GreenEconomy.Functions
{
    public static class GreenEconomy
    {
        [FunctionName("GreenEconomy")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            return name != null
                ? (ActionResult)new OkObjectResult($"Hello, {name}")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }

        [FunctionName("b")]
        public static async Task<List<string>> B(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            return new List<string> { "Hello", "Goodbye" }; 
        }


        [FunctionName("businesses")]
        public static async Task<List<Business>> GetBusinessesAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req, ExecutionContext context)
        {
            CloudTable table = await DatabaseHelper.CreateTableAsync(nameof(Business), context);
            var result = await table.ExecuteQuerySegmentedAsync<ModelEntity<Business>>(new TableQuery<ModelEntity<Business>>(), null);

            var businesses = result.Select(x => x.Model).ToList();
            return businesses;
        }

        [FunctionName("seed")]
        public static async Task<List<Business>> SeedBusinessesAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req, ExecutionContext context)
        {
            CloudTable table = await DatabaseHelper.CreateTableAsync(nameof(Business), context);

            var store = new BusinessStore(new WebClient(new HttpClient()));
            var businesses = await store.SeedItemsAsync();
            var businessesEnitites = businesses.Select(x => new ModelEntity<Business> { Model = x, PartitionKey = "business", RowKey = x.Id });

            foreach (var business in businessesEnitites)
            {
                await table.InsertOrMergeEntityAsync<Business>(business);
            }

            return businesses.ToList();
        }
    }
}
