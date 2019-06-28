using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B24AirTableIntegration.App_Code.AirTable
{
    public abstract class AbstractRecords<T> where T : AbstractRecord
    {
        public List<T> records { get; set; }
    }
    public abstract class AbstractRecord
    {
        [Newtonsoft.Json.JsonIgnore]
        public abstract string TableName { get; }

        public string id { get; set; }
    }
}