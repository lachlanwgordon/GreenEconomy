using System;
using System.Diagnostics;
using Newtonsoft.Json;

namespace GreenEconomy.Core.Models
{
    public class BaseModel 
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string TableName { get; set; } 

        public BaseModel()
        {
            TableName = this.GetType().Name;
        }
    }
}
