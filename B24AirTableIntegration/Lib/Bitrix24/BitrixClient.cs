using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Configuration;
using B24AirTableIntegration.Lib.Helpers;


namespace B24AirTableIntegration.Lib.Bitrix24
{
    public class BitrixClient : ApiClient
    {
        private static object syncRoot = new object();
        private static BitrixClient client;

        public static BitrixClient Instance
        {
            get
            {
                if (client == null)
                {
                    lock (syncRoot)
                    {
                        if (client == null)
                        {
                            client = new BitrixClient();
                        }
                    }
                }
                return client;
            }
        }

        private BitrixClient() :
            base(ConfigurationManager.AppSettings["BitrixUrl"], null)
        {
        }

        public LeadResponse GetLead(string id)
        {
            LeadResponse leadResponse = GetEntity<LeadResponse>(BitrixMethods.GET_LEAD, id);
            leadResponse.Lead.Contact = GetEntity<Contact>(BitrixMethods.GET_CONTACT, leadResponse.Lead.CONTACT_ID);
            return leadResponse;
        }

        public DealResponse GetDeal(string id)
        {
            DealResponse dealResponse = GetEntity<DealResponse>(BitrixMethods.GET_DEAL, id);
            dealResponse.Deal.Lead = GetLead(dealResponse.Deal.LEAD_ID);
            return dealResponse;
        }

        private T GetEntity<T>(string method, string id)
        {
            return Get<T>(method, new Dictionary<string, string> { { "id", id } });
        }

        internal string GetEnumFieldValue(string FieldName, string ID)
        {
            EnumFieldItems items = Get<EnumFieldItems>(BitrixMethods.GET_ENUM_VALUES, new Dictionary<string, string>
            {
                { "entity_id", FieldName}
            });
            return items.Items.FirstOrDefault(x => x.STATUS_ID == ID)?.NAME;
        }
    }
}