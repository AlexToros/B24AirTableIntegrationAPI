using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using B24AirTableIntegration.Lib.Bitrix24;
using B24AirTableIntegration.Lib.AirTable;
using System.IO;
using System.Text;

namespace B24AirTableIntegration.Controllers
{
    public class BitrixController : ApiController
    {
        [HttpPost]
        [Route("bitrix/test")]
        public void Test([FromBody]EventResponse response)
        {
            Log(response.data.fields.ID);
        }

        [HttpPost]
        [Route("bitrix/onleadchanged")]
        public void OnLeadChanged([FromBody]EventResponse response)
        {
            Log("Изменение лида");
            Log(response.data.fields.ID);
            try
            {
                BitrixClient Bitrix = BitrixClient.Instance;
                AirTableClient AirTable = AirTableClient.Instance;

                var Lead = Bitrix.GetLead(response.data.fields.ID);
                if (Lead.Lead.DATE_CREATE > new DateTime(2019, 7, 2, 13, 11, 0))
                    AirTable.UpdateOrCreate(Lead);
            }
            catch (Exception ex)
            {
                Log(ex.ToString());
            }
        }

        [HttpPost]
        [Route("bitrix/ondealchanged")]
        public void OnDealChanged([FromBody]EventResponse response)
        {
            BitrixClient Bitrix = BitrixClient.Instance;
            AirTableClient AirTable = AirTableClient.Instance;

            var Deal = Bitrix.GetDeal(response.data.fields.ID);
            if (Deal.Deal.DATE_CREATE > new DateTime(2019, 7, 2, 13, 11, 0))
                AirTable.UpdateOrCreate(Deal);
        }
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

        private void Log(string message)
        {
            using (StreamWriter sw = new StreamWriter(@"C:\Test\1.txt", true, Encoding.Default))
                sw.WriteLine(message);
        }
    }
}