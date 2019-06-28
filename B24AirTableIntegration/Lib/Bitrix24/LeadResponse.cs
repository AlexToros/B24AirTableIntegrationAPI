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
        public object SOURCE_DESCRIPTION { get; set; }
        public string STATUS_ID { get; set; }
        public object STATUS_DESCRIPTION { get; set; }
        public object POST { get; set; }
        public object COMMENTS { get; set; }
        public string CURRENCY_ID { get; set; }
        public string OPPORTUNITY { get; set; }
        public string HAS_PHONE { get; set; }
        public string HAS_EMAIL { get; set; }
        public string HAS_IMOL { get; set; }
        public string ASSIGNED_BY_ID { get; set; }
        public string CREATED_BY_ID { get; set; }
        public string MODIFY_BY_ID { get; set; }
        public DateTime DATE_CREATE { get; set; }
        public DateTime DATE_MODIFY { get; set; }
        public DateTime DATE_CLOSED { get; set; }
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
        public bool UF_CRM_1540290799 { get; set; }
        public bool UF_CRM_GA_CID { get; set; }
        public bool UF_CRM_TRANID { get; set; }
        public bool UF_CRM_COOKIES { get; set; }
        public bool UF_CRM_FORMNAME { get; set; }
        public bool UF_CRM_1561384018 { get; set; }
        public bool UF_CRM_1561385937 { get; set; }
        public string UF_CRM_1561385960 { get; set; }
        public bool UF_CRM_1561385988 { get; set; }
        public List<PHONE> PHONE { get; set; }
        public List<WEB> WEB { get; set; }
        public List<EMAIL> EMAIL { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public Contact Contact { get; set; }
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
    }
}