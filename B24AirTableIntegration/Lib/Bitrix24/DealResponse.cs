using B24AirTableIntegration.Lib.AirTable;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public DateTime BEGINDATE { get; set; }
        public string CLOSEDATE { get; set; }
        public string CREATED_BY_ID { get; set; }
        public string MODIFY_BY_ID { get; set; }
        public DateTime DATE_MODIFY { get; set; }
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
        public DateTime CheckIn { get; set; }
        [Newtonsoft.Json.JsonProperty("UF_CRM_5D10DD65CDCBD")]
        public string LivingDaysString { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public LeadResponse Lead { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public BitrixObjectType Type
        {
            get => Lead.Lead.Type;
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
            record.fields.Add("Источник", Deal.Source);
            record.fields.Add("Точка контакта", Deal.SOURCE_DESCRIPTION);
            record.fields.Add("Дата обращения", Deal.DATE_CREATE);
            //record.fields.Add("Тип клиента", ); //Сопоставление
            record.fields.Add("Основная информация", Deal.COMMENTS);
            record.fields.Add("Клиент", Deal.Lead.Lead.Contact.AirTableString);
            record.fields.Add("Клиент - Bitrix24", Deal.URL);
            record.fields.Add("Кол-во человек", Deal.PeopleCount);
            record.fields.Add("Заезд", Deal.CheckIn);
            record.fields.Add("Срок заселения", Deal.LivingDaysCount);
            record.fields.Add("Город", Deal.Lead.Lead.City);

            return record;
        }
    }
}