using System;
using System.Collections.Generic;
using System.Linq;
using GreenEconomy.Core.Models;
using Microsoft.Azure.Cosmos.Table;
using Xamarin.Essentials;

namespace GreenEconomy.Functions
{
    public class ModelWrapper<T> where T : BaseModel 
    {
        T Model { get; set; }
        DynamicTableEntity Entity { get; set; } = new DynamicTableEntity();




        public void PopuplateEntity()
        {
            var props = typeof(T).GetProperties();
            foreach (var prop in props)
            {


                var val = prop.GetValue(Model);

                if(prop.PropertyType == typeof(string))
                {
                    Entity.Properties.Add(prop.Name, new EntityProperty(val as string));
                }





            }
        }
    }



    public class BusinessEntity : Business, ITableEntity
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public string ETag { get; set; }

        public void ReadEntity(IDictionary<string, EntityProperty> properties, OperationContext operationContext)
        {
            PartitionKey = properties[nameof(PartitionKey)].StringValue;
            RowKey = properties[nameof(RowKey)].StringValue;
            Timestamp = properties[nameof(Timestamp)].DateTimeOffsetValue.Value;
            Id = properties[nameof(Id)].StringValue;
            Name = properties[nameof(Name)].StringValue;
            Location = new Location(properties["Latitude"].DoubleValue.Value, properties["Longitude"].DoubleValue.Value);
        }


        public IDictionary<string, EntityProperty> WriteEntity(OperationContext operationContext)
        {
            var properties = new Dictionary<string, EntityProperty>();





            return properties;
        }
    }


    public class ModelEntity<T> : ITableEntity where T : BaseModel, new()
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public string ETag { get; set; }

        public T Model { get; set; } = new T();

        public void ReadEntity(IDictionary<string, EntityProperty> properties, OperationContext operationContext)
        {

            var typeProperties = typeof(T).GetProperties();
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
                    else if (typeProp.PropertyType == typeof(int))
                        typeProp.SetValue(Model, prop.Value.Int32Value);
                }
            }
        }

        public IDictionary<string, EntityProperty> WriteEntity(OperationContext operationContext)
        {
            var properties = new Dictionary<string, EntityProperty>();
            var typeProperties = typeof(T).GetProperties();
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
                else if (typeProp.PropertyType == typeof(int))
                {
                    properties.Add(typeProp.Name, new EntityProperty((int)typeProp.GetValue(Model)));
                }
            }

            return properties;
        }
    }
}
