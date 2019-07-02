using B24AirTableIntegration.Lib.AirTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B24AirTableIntegration.Lib.Bitrix24
{
    public class Lead : BitrixObject
    {
        public object HONORIFIC { get; set; }
        public object NAME { get; set; }
        public object SECOND_NAME { get; set; }
        public object LAST_NAME { get; set; }
        public object COMPANY_TITLE { get; set; }
        public string COMPANY_ID { get; set; }
        public string IS_RETURN_CUSTOMER { get; set; }
        public string BIRTHDATE { get; set; }
        public string STATUS_ID { get; set; }
        public object STATUS_DESCRIPTION { get; set; }
        public object POST { get; set; }
        public string CURRENCY_ID { get; set; }
        public string OPPORTUNITY { get; set; }
        public string HAS_PHONE { get; set; }
        public string HAS_EMAIL { get; set; }
        public string HAS_IMOL { get; set; }
        public string CREATED_BY_ID { get; set; }
        public string MODIFY_BY_ID { get; set; }
        public DateTime? DATE_MODIFY { get; set; }
        public DateTime? DATE_CLOSED { get; set; }
        public string STATUS_SEMANTIC_ID { get; set; }
        public string OPENED { get; set; }
        public object ORIGINATOR_ID { get; set; }
        public object ORIGIN_ID { get; set; }
        public string ADDRESS { get; set; }
        public object ADDRESS_2 { get; set; }
        public object ADDRESS_CITY { get; set; }
        public object ADDRESS_POSTAL_CODE { get; set; }
        public object ADDRESS_REGION { get; set; }
        public object ADDRESS_PROVINCE { get; set; }
        public object ADDRESS_COUNTRY { get; set; }
        public object ADDRESS_COUNTRY_CODE { get; set; }
        public object UTM_SOURCE { get; set; }
        public object UTM_MEDIUM { get; set; }
        public object UTM_CAMPAIGN { get; set; }
        public object UTM_CONTENT { get; set; }
        public object UTM_TERM { get; set; }
        [Newtonsoft.Json.JsonProperty("UF_CRM_1540290799")]
        public List<int> Type_IDs { get; set; }
        public string UF_CRM_GA_CID { get; set; }
        public string UF_CRM_TRANID { get; set; }
        public string UF_CRM_COOKIES { get; set; }
        public string UF_CRM_FORMNAME { get; set; }
        [Newtonsoft.Json.JsonProperty("UF_CRM_1561384018")]
        public string City_ID { get; set; }
        [Newtonsoft.Json.JsonProperty("UF_CRM_1561385937")]
        public string CountPeopleString { get; set; }
        [Newtonsoft.Json.JsonProperty("UF_CRM_1561385960")]
        public DateTime? CheckIn { get; set; }
        [Newtonsoft.Json.JsonProperty("UF_CRM_1561385988")]
        public string LivingDaysString { get; set; }
        public List<PHONE> PHONE { get; set; }
        public List<WEB> WEB { get; set; }
        public List<EMAIL> EMAIL { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public Contact Contact { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public UserResponse AssignUser { get; set; }

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
        public string URL
        {
            get => $@"https://kvikroom.bitrix24.ru/crm/lead/details/{ID}/";
        }

        [Newtonsoft.Json.JsonIgnore]
        public string City
        {
            get
            {
                return BitrixClient.Instance.GetLeadEnumUserFieldValue("UF_CRM_1561384018", City_ID);
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
                return BitrixClient.Instance.GetEnumFieldValue(BitrixSettings.LEAD_STATUS_LIST_ID, STATUS_ID);
            }
        }
    }

    public class Time
    {
        public double start { get; set; }
        public double finish { get; set; }
        public double duration { get; set; }
        public double processing { get; set; }
        public DateTime date_start { get; set; }
        public DateTime date_finish { get; set; }
    }

    public class LeadResponse
    {
        [Newtonsoft.Json.JsonProperty("result")]
        public Lead Lead { get; set; }
        public Time time { get; set; }

        internal UpdatingRecord GetUpdatingRecord()
        {
            UpdatingRecord record = new UpdatingRecord()
            {
                fields = new Dictionary<string, object>()
            };
            if (Lead.Source != null)
                record.fields.Add("Источник", Lead.Source);
            if (Lead.SOURCE_DESCRIPTION != null)
                record.fields.Add("Точка контакта", Lead.SOURCE_DESCRIPTION);
            if (Lead.DATE_CREATE.HasValue && Lead.DATE_CREATE.Value != DateTime.MinValue)
                record.fields.Add("Дата обращения", Lead.DATE_CREATE.Value.ToString("yyyy-MM-dd"));
            //record.fields.Add("Тип клиента", ); //Сопоставление
            if (Lead.COMMENTS != null)
                record.fields.Add("Основная информация", Lead.COMMENTS);
            if (Lead.Contact != null && !string.IsNullOrWhiteSpace(Lead.Contact.AirTableString))
                record.fields.Add("Клиент", Lead.Contact.AirTableString);
            if (Lead.URL != null)
                record.fields.Add("Клиент - Bitrix24", Lead.URL);
            if (Lead.PeopleCount != 0)
                record.fields.Add("Кол-во человек", Lead.PeopleCount);
            if (Lead.CheckIn.HasValue && Lead.CheckIn.Value != DateTime.MinValue)
                record.fields.Add("Заезд", Lead.CheckIn.Value.ToString("yyyy-MM-dd"));
            if (Lead.LivingDaysCount != 0)
                record.fields.Add("Срок заселения", Lead.LivingDaysCount);
            if (Lead.City != null)
                record.fields.Add("Город", Lead.City);

            return record;
        }
    }
}