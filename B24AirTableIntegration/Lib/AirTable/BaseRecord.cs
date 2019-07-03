using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B24AirTableIntegration.Lib.AirTable
{
    public class BaseRecords<T> where T : BaseRecord
    {
        public List<T> records { get; set; }
    }
    public class BaseRecord
    {
        [Newtonsoft.Json.JsonIgnore]
        public string TableName { get; }

        public string id { get; set; }
    }
    public class UpdatingRecord
    {
        public Dictionary<string, object> fields { get; set; }
    }
    public class CreationResponse
    {
        public string id { get; set; }
    }
}