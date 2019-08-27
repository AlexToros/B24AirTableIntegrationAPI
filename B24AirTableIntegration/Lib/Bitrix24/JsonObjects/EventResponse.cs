using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B24AirTableIntegration.Lib.Bitrix24
{
    public class EventResponse
    {
        public string @event { get; set; }
        public Auth auth { get; set; }
        public string ts { get; set; }
        public Data data { get; set; }
    }
    public class Auth
    {
        public string domain { get; set; }
        public string client_endpoint { get; set; }
        public string server_endpoint { get; set; }
        public string member_id { get; set; }
        public string application_token { get; set; }
    }
    public class Data
    {
        public Fields fields { get; set; }
    }
    public class Fields
    {
        public string ID { get; set; }
    }
}