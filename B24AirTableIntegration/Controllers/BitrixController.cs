﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using B24AirTableIntegration.Lib.Bitrix24;
using B24AirTableIntegration.Lib.AirTable;
using B24AirTableIntegration.Lib.Helpers;

namespace B24AirTableIntegration.Controllers
{
    public class BitrixController : ApiController
    {        
        [HttpPost]
        [Route("bitrix/onleadchanged")]
        public void OnLeadChanged([FromBody]EventResponse response)
        {
            Log.Debug("Изменение лида");
            Log.Debug(response.data.fields.ID);
            try
            {
                BitrixClient Bitrix = BitrixClient.Instance;
                AirTableClient AirTable = AirTableClient.Instance;

                var Lead = Bitrix.GetLead(response.data.fields.ID);
                if (Lead.Lead.DATE_CREATE > new DateTime(2019, 7, 2, 13, 11, 0))
                {

                    if (Lead.Lead.IsValid)
                        AirTable.UpdateOrCreate(Lead);
                    else
                        AirTable.DeleteIfExist(Lead);
                }
                else
                    Log.Debug("Старый лид");
            }
            catch (Exception ex)
            {
                Log.Debug($"{ex}\r\nВнутреннее исключение:{ex.InnerException}");
            }
        }

        [HttpPost]
        [Route("bitrix/ondealchanged")]
        public void OnDealChanged([FromBody]EventResponse response)
        {
            try
            {
                Log.Debug("Изменение сделки");
                Log.Debug(response.data.fields.ID);
                BitrixClient Bitrix = BitrixClient.Instance;
                AirTableClient AirTable = AirTableClient.Instance;

                var Deal = Bitrix.GetDeal(response.data.fields.ID);
                if (Deal.Deal.DATE_CREATE > new DateTime(2019, 7, 2, 13, 11, 0))
                {
                    if (Deal.Deal.IsValid)
                        AirTable.UpdateOrCreate(Deal);
                }
                else
                    Log.Debug("Старая сделка");
            }
            catch (Exception ex)
            {
                Log.Debug($"{ex}\r\nВнутреннее исключение:{ex.InnerException}");
            }
        }

        [HttpPost]
        [Route("bitrix/oncommentadd")]
        public void OnCommentAdd([FromBody]EventResponse response)
        {
            try
            {
                BitrixClient Bitrix = BitrixClient.Instance;
                AirTableClient AirTable = AirTableClient.Instance;

                var Comment = Bitrix.GetComment(response.data.fields.ID);

                AirTable.Update(Comment);
            }
            catch (Exception ex)
            {
                Log.Debug($"{ex}\r\nВнутреннее исключение:{ex.InnerException}");
            }
        }

        [HttpPost]
        [Route("bitrix/oncommentupdate")]
        public void OnCommentUpdate([FromBody]EventResponse response)
        {
            try
            {
                BitrixClient Bitrix = BitrixClient.Instance;
                AirTableClient AirTable = AirTableClient.Instance;

                var Comment = Bitrix.GetComment(response.data.fields.ID);
                var OtherComments = Bitrix.GetTimeLine(Comment.Comment.ENTITY_ID.ToString(), Comment.Comment.ENTITY_TYPE);

                if (Comment.Comment == OtherComments.Entries.OrderByDescending(c => c.CREATED).FirstOrDefault())
                    AirTable.Update(Comment);
            }
            catch (Exception ex)
            {
                Log.Debug($"{ex}\r\nВнутреннее исключение:{ex.InnerException}");
            }
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