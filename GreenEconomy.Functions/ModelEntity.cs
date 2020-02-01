using System;
using System.Collections.Generic;
using System.Linq;
using GreenEconomy.Core.Models;
using Microsoft.Azure.Cosmos.Table;
using Xamarin.Essentials;

namespace GreenEconomy.Functions
{
    public class ModelEntity : ITableEntity  
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public string ETag { get; set; }

        public BaseModel Model { get; set; } = new BaseModel();

        public void ReadEntity(IDictionary<string, EntityProperty> properties, OperationContext operationContext)
        {
            if(properties.TryGetValue("TableName", out EntityProperty tableName))
            {
                var fullName = $"GreenEconomy.Core.Models.{tableName}, GreenEconomy.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
                var type = Type.GetType(fullName);

                Model = Activator.CreateInstance(type) as BaseModel;
            }
            else
            {
                return;
            }

            var typeProperties = Model.GetType().GetProperties();
            foreach (var prop in properties)
            {
                var typeProp = typeProperties.FirstOrDefault(x => x.Name == prop.Key);
                if (typeProp != null)
                {

                    if (typeProp.PropertyType == typeof(string))
                    {
                        typeProp.SetValue(Model, prop.Value.StringValue);
                    }
                    else if (typeProp.PropertyType == typeof(double))
                    {
                        typeProp.SetValue(Model, prop.Value.DoubleValue);
                    }
                    else if (typeProp.PropertyType == typeof(int) || typeProp.PropertyType.IsEnum)
                        typeProp.SetValue(Model, prop.Value.Int32Value);
                   
                }
            }
        }

        public IDictionary<string, EntityProperty> WriteEntity(OperationContext operationContext)
        {
            var properties = new Dictionary<string, EntityProperty>();
            var typeProperties = Model.GetType().GetProperties();
            foreach (var typeProp in typeProperties)
            {
                if (typeProp.PropertyType == typeof(string))
                {
                    properties.Add(typeProp.Name, new EntityProperty(typeProp.GetValue(Model) as string));
                }
                else if (typeProp.PropertyType == typeof(double))
                {
                    properties.Add(typeProp.Name, new EntityProperty((double)typeProp.GetValue(Model)));

                }
                else if (typeProp.PropertyType == typeof(int) || typeProp.PropertyType.IsEnum)
                {
                    properties.Add(typeProp.Name, new EntityProperty((int)typeProp.GetValue(Model)));
                }
            }

            return properties;
        }
    }
}
