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
        public List<int> UF_CRM_5BCF50BA94A18 { get; set; }
        public string UF_CRM_GA_CID { get; set; }
        public string UF_CRM_5D0B79DC12351 { get; set; }
        public string UF_CRM_5D0B79DC20D5F { get; set; }
        public string UF_CRM_5D0CEBB99A40C { get; set; }
        public string UF_CRM_5D10DD65A2518 { get; set; }
        [Newtonsoft.Json.JsonProperty("UF_CRM_5D10DD65B7069")]
        public string CountPeopleString { get; set; }
        [Newtonsoft.Json.JsonProperty("UF_CRM_5D10DD65C34C5")]
        public DateTime? CheckIn { get; set; }
        [Newtonsoft.Json.JsonProperty("UF_CRM_5D10DD65CDCBD")]
        public string LivingDaysString { get; set; }

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

        [Newtonsoft.Json.JsonIgnore]
        public BitrixObjectType Type
        {
            get
            {
                if (Lead.Lead != null)
                    return Lead.Lead.Type;
                return BitrixObjectType.None;
            }
        }

        [Newtonsoft.Json.JsonIgnore]
        public bool IsValid
        {
            get
            {
                if (Lead.Lead != null)
                    return Lead.Lead.IsValid;
                return false;
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
                    case BitrixObjectType.B2B:
                        list_ID = BitrixSettings.B2B_STATUS_LIST_ID;
                        break;
                    case BitrixObjectType.B2C:
                        list_ID = BitrixSettings.B2C_STATUS_LIST_ID;
                        break;
                    default:
                        return null;
                }

                return BitrixClient.Instance.GetEnumFieldValue(list_ID, STAGE_ID);
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
                record.fields.Add("Основная информация", Regex.Replace(Deal.COMMENTS, "<[^>]+>", string.Empty));

            if (Deal.Lead != null && Deal.Lead.Lead != null && !string.IsNullOrEmpty(Deal.Lead.Lead.AirTableClientString))
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
            if (Deal.Lead != null && Deal.Lead.Lead != null && Deal.Lead.Lead.City != null)
                record.fields.Add("Город", Deal.Lead.Lead.City);
            if (Deal.Lead != null && Deal.Lead.Lead != null && Deal.Lead.Lead.ClientType != null)
                record.fields.Add("Тип клиента", Deal.Lead.Lead.ClientType);
            if (Deal.Lead != null && Deal.Lead.Lead != null && Deal.Lead.Lead.TypeName != null)
                record.fields.Add("Тип Лида/Сделки", Deal.Lead.Lead.TypeName);

            return record;
        }
    }
}