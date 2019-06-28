using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B24AirTableIntegration.Lib.Bitrix24
{
    public class Item
    {
        public string NAME { get; set; }
        public int SORT { get; set; }
        public string STATUS_ID { get; set; }
    }
    
    public class EnumFieldItems
    {
        [Newtonsoft.Json.JsonProperty("result")]
        public List<Item> Items { get; set; }
        public Time time { get; set; }
    }
}