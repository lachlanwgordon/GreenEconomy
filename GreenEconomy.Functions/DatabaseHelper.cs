using System;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Threading.Tasks;
using GreenEconomy.Core.Models;

namespace GreenEconomy.Functions
{
    public static class DatabaseHelper
    {
        public static string StorageConnectionString { get; } = Environment.GetEnvironmentVariable("StorageConnectionString", EnvironmentVariableTarget.Process);

        public static CloudStorageAccount CreateStorageAccountFromConnectionString(string storageConnectionString)
        {
            CloudStorageAccount storageAccount;
            try
            {
                storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            }
            catch (FormatException)
            {
                Debug.WriteLine("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the application.");
                throw;
            }
            catch (ArgumentException)
            {
                Debug.WriteLine("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the sample.");
                throw;
            }

            return storageAccount;
        }

        public static async Task<CloudTable> CreateTableAsync(string tableName, Microsoft.Azure.WebJobs.ExecutionContext context)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(context.FunctionAppDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            string storageConnectionString = config.GetConnectionString("StorageConnectionString");
            CloudStorageAccount storageAccount = CreateStorageAccountFromConnectionString(storageConnectionString);

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient(new TableClientConfiguration());


            CloudTable table = tableClient.GetTableReference(tableName);
            await table.CreateIfNotExistsAsync();


            return table;
        }

        public static async Task<ModelEntity> InsertOrMergeEntityAsync(this CloudTable table, ModelEntity entity) 
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            try
            {
                // Create the InsertOrReplace table operation
                TableOperation insertOrMergeOperation = TableOperation.InsertOrMerge(entity);

                // Execute the operation.
                TableResult result = await table.ExecuteAsync(insertOrMergeOperation);
                ModelEntity insertedModel = result.Result as ModelEntity;

                // Get the request units consumed by the current operation. RequestCharge of a TableResult is only applied to Azure Cosmos DB
                if (result.RequestCharge.HasValue)
                {
                    Console.WriteLine("Request Charge of InsertOrMerge Operation: " + result.RequestCharge);
                }

                return insertedModel;
            }
            catch (StorageException ex)
            {
                Debug.WriteLine($"Insert or merge error {ex} \n {ex.StackTrace}");
                throw;
            }
        }
    }
}
