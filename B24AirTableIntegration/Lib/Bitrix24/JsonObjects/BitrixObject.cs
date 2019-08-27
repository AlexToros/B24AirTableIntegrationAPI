using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B24AirTableIntegration.Lib.Bitrix24
{
    public class BitrixObject
    {
        public string ID { get; set; }
        public string TITLE { get; set; }
        public string SOURCE_ID { get; set; }
        public string SOURCE_DESCRIPTION { get; set; }
        public string CONTACT_ID { get; set; }
        public DateTime? DATE_CREATE { get; set; }
        public string COMMENTS { get; set; }
        public string ASSIGNED_BY_ID { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public string Source
        {
            get
            {
                var client = BitrixClient.Instance;
                return client.GetEnumFieldValue(BitrixSettings.SOURCE_LIST_ID, SOURCE_ID);
            }
        }
    }
}