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
            LeadResponse leadResponse = GetEntity<LeadResponse>(BitrixSettings.GET_LEAD, id);
            if (leadResponse.Lead != null && leadResponse.Lead.CONTACT_ID != null)
                leadResponse.Lead.Contact = GetEntity<Contact>(BitrixSettings.GET_CONTACT, leadResponse.Lead.CONTACT_ID);
            if (leadResponse.Lead != null && leadResponse.Lead.ASSIGNED_BY_ID != null)
                leadResponse.Lead.AssignUser = GetEntity<UserResponse>(BitrixSettings.GET_USER, leadResponse.Lead.ASSIGNED_BY_ID);
            return leadResponse;
        }

        public DealResponse GetDeal(string id)
        {
            DealResponse dealResponse = GetEntity<DealResponse>(BitrixSettings.GET_DEAL, id);
            if (dealResponse.Deal != null && dealResponse.Deal.ASSIGNED_BY_ID != null)
                dealResponse.Deal.Lead = GetLead(dealResponse.Deal.LEAD_ID);
            return dealResponse;
        }

        private T GetEntity<T>(string method, string id)
        {
            return Get<T>(method, new Dictionary<string, string> { { "id", id } });
        }

        internal string GetEnumFieldValue(string FieldName, string ID)
        {
            EnumFieldItems items = Get<EnumFieldItems>(BitrixSettings.GET_ENUM_VALUES, new Dictionary<string, string>
            {
                { "entity_id", FieldName}
            });
            return items.Items.FirstOrDefault(x => x.STATUS_ID == ID)?.NAME;
        }

        internal string GetLeadEnumUserFieldValue(string FieldName, string Enum_ID)
        {
            var ef = Get<UserEnumFieldResponse>(BitrixSettings.GET_USER_ENUM_VALUE,
                new Dictionary<string, string> {
                    { "filter[FIELD_NAME]", FieldName}
                });
            if (ef != null && ef.result != null && ef.result.Count > 0 && ef.result[0].LIST != null)
                return ef.result[0].LIST.FirstOrDefault(x => x.ID == Enum_ID)?.VALUE;
            return null;
        }
    }
}