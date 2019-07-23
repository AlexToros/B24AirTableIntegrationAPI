using B24AirTableIntegration.Lib.AirTable;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace B24AirTableIntegration.Lib.Bitrix24
{
    public class Deal : BitrixObject
    {
        public string TYPE_ID { get; set; }
        public string STAGE_ID { get; set; }
        public object PROBABILITY { get; set; }
        public string CURRENCY_ID { get; set; }
        public string OPPORTUNITY { get; set; }
        public object TAX_VALUE { get; set; }
        public string LEAD_ID { get; set; }
        public string COMPANY_ID { get; set; }
        public object QUOTE_ID { get; set; }
        public DateTime? BEGINDATE { get; set; }
        public string CLOSEDATE { get; set; }
        public string CREATED_BY_ID { get; set; }
        public string MODIFY_BY_ID { get; set; }
        public DateTime? DATE_MODIFY { get; set; }
        public string OPENED { get; set; }
        public string CLOSED { get; set; }
        public object ADDITIONAL_INFO { get; set; }
        public object LOCATION_ID { get; set; }
        public string CATEGORY_ID { get; set; }
        public string STAGE_SEMANTIC_ID { get; set; }
        public string IS_NEW { get; set; }
        public string IS_RECURRING { get; set; }
        public string IS_RETURN_CUSTOMER { get; set; }
        public string IS_REPEATED_APPROACH { get; set; }
        public object ORIGINATOR_ID { get; set; }
        public object ORIGIN_ID { get; set; }
        public object UTM_SOURCE { get; set; }
        public object UTM_MEDIUM { get; set; }
        public object UTM_CAMPAIGN { get; set; }
        public object UTM_CONTENT { get; set; }
        public object UTM_TERM { get; set; }
        public string UF_CRM_1539697408 { get; set; }
        [Newtonsoft.Json.JsonProperty("UF_CRM_5BCF50BA94A18")]
        public List<int> Type_IDs { get; set; }
        public string UF_CRM_GA_CID { get; set; }
        public string UF_CRM_5D0B79DC12351 { get; set; }
        public string UF_CRM_5D0B79DC20D5F { get; set; }
        public string UF_CRM_5D0CEBB99A40C { get; set; }
        [Newtonsoft.Json.JsonProperty("UF_CRM_5D10DD65A2518")]
        public string City_ID { get; set; }
        [Newtonsoft.Json.JsonProperty("UF_CRM_5D10DD65B7069")]
        public string CountPeopleString { get; set; }
        [Newtonsoft.Json.JsonProperty("UF_CRM_5D10DD65C34C5")]
        public DateTime? CheckIn { get; set; }
        [Newtonsoft.Json.JsonProperty("UF_CRM_5D10DD65CDCBD")]
        public string LivingDaysString { get; set; }
        [Newtonsoft.Json.JsonProperty("UF_CRM_5D1C56FCCE1D7")]
        public string ClientType_ID { get; set; }

        private ContactResponse contact = null;
        [Newtonsoft.Json.JsonIgnore]
        public ContactResponse Contact
        {
            get
            {
                if (contact == null)
                {
                    if (string.IsNullOrWhiteSpace(CONTACT_ID))
                        return null;
                    contact = BitrixClient.Instance.GetContact(CONTACT_ID);
                }
                return contact;
            }
            set
            {
                contact = value;
            }
        }

        private UserResponse assignUser = null;
        [Newtonsoft.Json.JsonIgnore]
        public UserResponse AssignUser
        {
            get
            {
                if (assignUser == null)
                {
                    if (string.IsNullOrWhiteSpace(ASSIGNED_BY_ID))
                        return null;
                    assignUser = BitrixClient.Instance.GetUser(ASSIGNED_BY_ID);
                }
                return assignUser;
            }
            set
            {
                assignUser = value;
            }
        }

        private LeadResponse lead = null;
        [Newtonsoft.Json.JsonIgnore]
        public LeadResponse Lead {
            get
            {
                if (lead == null)
                {
                    if (string.IsNullOrWhiteSpace(LEAD_ID))
                        return null;
                    lead = BitrixClient.Instance.GetLead(LEAD_ID);
                }
                return lead;
            }
            set
            {
                lead = value;
            }
        }

        public string typeName = null;
        [Newtonsoft.Json.JsonIgnore]
        public string TypeName
        {
            get
            {
                switch (Type)
                {
                    default:
                        return null;
                    case BitrixObjectType.Deal_B2B:
                    case BitrixObjectType.Deal_B2B2:
                    case BitrixObjectType.Deal_B2C:
                        if (string.IsNullOrWhiteSpace(typeName))
                        {
                            typeName = BitrixClient.Instance.GetDealEnumUserFieldValue("UF_CRM_5BCF50BA94A18", Type_IDs[0].ToString());
                        }
                        return typeName;
                }
            }
        }

        [Newtonsoft.Json.JsonIgnore]
        public BitrixObjectType Type
        {
            get
            {
                if (Type_IDs != null && Type_IDs.Count > 0)
                {
                    try
                    {
                        return (BitrixObjectType)Type_IDs[0];
                    }
                    catch
                    {
                        return BitrixObjectType.None;
                    }
                }
                return BitrixObjectType.None;
            }
        }

        [Newtonsoft.Json.JsonIgnore]
        public bool IsValid
        {
            get
            {
                return (Type == BitrixObjectType.Deal_B2C || Type == BitrixObjectType.Deal_B2B || Type == BitrixObjectType.Deal_B2B2);
            }
        }

        [Newtonsoft.Json.JsonIgnore]
        public int PeopleCount
        {
            get
            {
                int res = 0;
                int.TryParse(CountPeopleString, out res);
                return res;
            }
        }

        [Newtonsoft.Json.JsonIgnore]
        public int LivingDaysCount
        {
            get
            {
                int res = 0;
                int.TryParse(LivingDaysString, out res);
                return res;
            }
        }

        [Newtonsoft.Json.JsonIgnore]
        public string Status
        {
            get
            {
                string list_ID;
                switch (Type)
                {
                    case BitrixObjectType.Deal_B2B2:
                    case BitrixObjectType.Deal_B2B:
                        list_ID = BitrixSettings.B2B_STATUS_LIST_ID;
                        break;
                    case BitrixObjectType.Deal_B2C:
                        list_ID = BitrixSettings.B2C_STATUS_LIST_ID;
                        break;
                    default:
                        return null;
                }

                return BitrixClient.Instance.GetEnumFieldValue(list_ID, STAGE_ID);
            }
        }

        private string city = null;
        [Newtonsoft.Json.JsonIgnore]
        public string City
        {
            get
            {
                if (string.IsNullOrWhiteSpace(city))
                {
                    city = BitrixClient.Instance.GetDealEnumUserFieldValue("UF_CRM_5D10DD65A2518", City_ID);
                }
                return city;
            }
        }

        private string clientType = null;
        [Newtonsoft.Json.JsonIgnore]
        public string ClientType
        {
            get
            {
                if (string.IsNullOrWhiteSpace(clientType))
                {
                    clientType = BitrixClient.Instance.GetLeadEnumUserFieldValue("UF_CRM_5D1C56FCCE1D7", ClientType_ID);
                }
                return clientType;
            }
        }

        [Newtonsoft.Json.JsonIgnore]
        public string URL
        {
            get => $@"https://kvikroom.bitrix24.ru/crm/deal/details/{ID}/";
        }
    }

    public class DealResponse
    {
        [Newtonsoft.Json.JsonProperty("result")]
        public Deal Deal { get; set; }
        public Time time { get; set; }

        internal UpdatingRecord GetUpdatingRecord()
        {
            UpdatingRecord record = new UpdatingRecord()
            {
                fields = new Dictionary<string, object>()
            };
            if(Deal.Source != null)
            record.fields.Add("Источник", Deal.Source);
            if (Deal.SOURCE_DESCRIPTION != null)
                record.fields.Add("Точка контакта", Deal.SOURCE_DESCRIPTION);
            if (Deal.DATE_CREATE.HasValue && Deal.DATE_CREATE.Value != DateTime.MinValue)
                record.fields.Add("Дата обращения", Deal.DATE_CREATE.Value.ToString("yyyy-MM-dd"));
            if (Deal.COMMENTS != null)
                record.fields.Add("Основная информация", Regex.Replace(Deal.COMMENTS, "<[^>]+>", " "));

            if(Deal.Contact != null && !string.IsNullOrWhiteSpace(Deal.Contact.Contact.AirTableString))
                record.fields.Add("Клиент", Deal.Contact.Contact.AirTableString);
            else if (Deal.Lead != null && Deal.Lead.Lead != null && !string.IsNullOrEmpty(Deal.Lead.Lead.AirTableClientString))
                record.fields.Add("Клиент", Deal.Lead.Lead.AirTableClientString);
            else if (Deal.Lead != null && Deal.Lead.Lead != null && Deal.Lead.Lead.Contact != null && Deal.Lead.Lead.Contact.Contact != null && !string.IsNullOrWhiteSpace(Deal.Lead.Lead.Contact.Contact.AirTableString))
                record.fields.Add("Клиент", Deal.Lead.Lead.Contact.Contact.AirTableString);

            if (Deal.URL != null)
                record.fields.Add("Клиент - Bitrix24", Deal.URL);
            if (Deal.PeopleCount != 0)
                record.fields.Add("Кол-во человек", Deal.PeopleCount);
            if (Deal.CheckIn.HasValue && Deal.CheckIn.Value != DateTime.MinValue)
                record.fields.Add("Заезд", Deal.CheckIn.Value.ToString("yyyy-MM-dd"));
            if (Deal.LivingDaysCount != 0)
                record.fields.Add("Срок заселения / дней", Deal.LivingDaysCount);
            if (Deal.City != null)
                record.fields.Add("Город", Deal.City);
            if (Deal.ClientType != null)
                record.fields.Add("Тип клиента", Deal.ClientType);
            if (Deal.TypeName != null)
                record.fields.Add("Тип Лида/Сделки", Deal.TypeName);

            return record;
        }
    }
}