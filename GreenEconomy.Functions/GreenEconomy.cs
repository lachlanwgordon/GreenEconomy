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
        public static Task<List<string>> B(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            return Task.FromResult(new List<string> { "Hello", "Goodbye" }); 
        }

        [FunctionName("get")]
        public static async Task<List<BaseModel>> GetModelsAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req, ExecutionContext context)
        {
            var tableName = req.Query["table"];
            var business = new Business();

            CloudTable table = await DatabaseHelper.CreateTableAsync(tableName, context);

            var result = await table.ExecuteQuerySegmentedAsync<ModelEntity>(new TableQuery<ModelEntity>(), null);



            var businesses = result.Select(x => x.Model).ToList().ToList<BaseModel>();
            return businesses;
        }

        [FunctionName("post")]
        public static async Task<BaseModel> SaveModel([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestMessage req, ExecutionContext context)
        {
            var szModel = await req.Content.ReadAsStringAsync();
            //var baseModel = await req.Content.ReadAsAsync<BaseModel>();//This crashes due to unsupported content type but I don't know how to fix it. It would be good to get it working.
            var baseModel = JsonConvert.DeserializeObject<BaseModel>(szModel);

            var tableName = baseModel.TableName;
            var fullName = $"GreenEconomy.Core.Models.{tableName}, GreenEconomy.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
            var type = Type.GetType(fullName);
            var model = JsonConvert.DeserializeObject(szModel, type) as BaseModel;

            CloudTable table = await DatabaseHelper.CreateTableAsync(tableName, context);

            var ent = await table.InsertOrMergeEntityAsync(new ModelEntity { Model = model, PartitionKey = tableName, RowKey = model.Id});

            return ent.Model;
        }

        [FunctionName("seed")]
        public static async Task<List<BaseModel>> SeedBusinessesAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req, ExecutionContext context)
        {
            var tableName = req.Query["table"];

            CloudTable table = await DatabaseHelper.CreateTableAsync(tableName, context);

            var store = new BusinessStore(new WebClient(new HttpClient()));
            var businesses = await store.SeedItemsAsync();
            var businessesEnitites = businesses.Select(x => new ModelEntity { Model = x, PartitionKey = tableName, RowKey = x.Id });

            foreach (var business in businessesEnitites)
            {
                await table.InsertOrMergeEntityAsync(business);
            }

            return businesses.ToList<BaseModel>();
        }
    }
}
