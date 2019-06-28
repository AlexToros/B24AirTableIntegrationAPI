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
        public string ASSIGNED_BY_ID { get; set; }
        public string CREATED_BY_ID { get; set; }
        public string MODIFY_BY_ID { get; set; }
        public DateTime DATE_CREATE { get; set; }
        public DateTime DATE_MODIFY { get; set; }
        public string OPENED { get; set; }
        public string CLOSED { get; set; }
        public string COMMENTS { get; set; }
        public object ADDITIONAL_INFO { get; set; }
        public object LOCATION_ID { get; set; }
        public string CATEGORY_ID { get; set; }
        public string STAGE_SEMANTIC_ID { get; set; }
        public string IS_NEW { get; set; }
        public string IS_RECURRING { get; set; }
        public string IS_RETURN_CUSTOMER { get; set; }
        public string IS_REPEATED_APPROACH { get; set; }
        public string SOURCE_DESCRIPTION { get; set; }
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
        public string UF_CRM_5D10DD65B7069 { get; set; }
        public DateTime UF_CRM_5D10DD65C34C5 { get; set; }
        public string UF_CRM_5D10DD65CDCBD { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public LeadResponse Lead { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public BitrixObjectType Type
        {
            get
            {
                int i;
                if (int.TryParse(UF_CRM_1539697408, out i))
                {
                    try
                    {
                        return (BitrixObjectType)i;
                    }
                    catch
                    {
                        return BitrixObjectType.None;
                    }
                }
                else
                    return BitrixObjectType.None;
            }
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
            //record.fields.Add("Тип клиента", Deal.DATE_CREATE); //Сопоставление
            record.fields.Add("Основная информация", Deal.COMMENTS);
            return record;
        }
    }
}