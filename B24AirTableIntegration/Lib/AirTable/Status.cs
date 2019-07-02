using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B24AirTableIntegration.Lib.AirTable
{
    public class Fields
    {
        public string Name { get; set; }
        public string StatusType { get; set; }
        public string AirTableStatus { get; set; }
    }

    public class Status
    {
        public string id { get; set; }
        public Fields fields { get; set; }
        public DateTime createdTime { get; set; }
    }

    public class Statuses
    {
        public List<Status> records { get; set; }
        public string offset { get; set; }
    }
}