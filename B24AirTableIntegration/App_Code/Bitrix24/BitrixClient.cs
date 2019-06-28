using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Configuration;
using B24AirTableIntegration.App_Code.Helpers;


namespace B24AirTableIntegration.App_Code.Bitrix24
{
    public class BitrixClient : ApiClient
    {
        public BitrixClient() :
            base(ConfigurationManager.AppSettings["BitrixUrl"], null)
        {
        }

        public LeadResponse GetLead(string id)
        {
            return Get<LeadResponse>(BitrixMethods.GET_LEAD, 
                new Dictionary<string, string> { { "id", id } });
        }

        public DealResponse GetDeal(string id)
        {
            DealResponse dealResponse = Get<DealResponse>(BitrixMethods.GET_DEAL,
                new Dictionary<string, string> { { "id", id } });

            dealResponse.Deal.Lead = GetLead(dealResponse.Deal.LEAD_ID);

            return dealResponse;
        }
    }
}