using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using B24AirTableIntegration.App_Code.Bitrix24;
using B24AirTableIntegration.App_Code.AirTable;

namespace B24AirTableIntegration.Controllers
{
    public class BitrixController : ApiController
    {
        [HttpPost]
        [Route("Bitrix/OnLeadChanged")]
        public void OnLeadChanged([FromBody]EventResponse response)
        {
            BitrixClient Bitrix = new BitrixClient();
            AirTableClient AirTable = new AirTableClient();
                        
            var Lead = Bitrix.GetLead(response.data.fields.ID);
            AirTable.Update(Lead);
        }

        [HttpPost]
        [Route("Bitrix/OnDealChanged")]
        public void OnDealChanged([FromBody]EventResponse response)
        {
            BitrixClient Bitrix = new BitrixClient();
            AirTableClient AirTable = new AirTableClient();

            var Deal = Bitrix.GetDeal(response.data.fields.ID);
            AirTable.Update(Deal);
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
    }
}