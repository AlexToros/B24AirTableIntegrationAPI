using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B24AirTableIntegration.Lib.Bitrix24
{
    public class Company
    {
        public string ID { get; set; }
        public object COMPANY_TYPE { get; set; }
        public string TITLE { get; set; }
        public object LOGO { get; set; }
        public string LEAD_ID { get; set; }
        public string HAS_PHONE { get; set; }
        public string HAS_EMAIL { get; set; }
        public string HAS_IMOL { get; set; }
        public string ASSIGNED_BY_ID { get; set; }
        public string CREATED_BY_ID { get; set; }
        public string MODIFY_BY_ID { get; set; }
        public object BANKING_DETAILS { get; set; }
        public object INDUSTRY { get; set; }
        public object REVENUE { get; set; }
        public object CURRENCY_ID { get; set; }
        public object EMPLOYEES { get; set; }
        public string COMMENTS { get; set; }
        public DateTime DATE_CREATE { get; set; }
        public DateTime DATE_MODIFY { get; set; }
        public string OPENED { get; set; }
        public string IS_MY_COMPANY { get; set; }
        public object ORIGINATOR_ID { get; set; }
        public object ORIGIN_ID { get; set; }
        public object ORIGIN_VERSION { get; set; }
        public object ADDRESS { get; set; }
        public object ADDRESS_2 { get; set; }
        public object ADDRESS_CITY { get; set; }
        public object ADDRESS_POSTAL_CODE { get; set; }
        public object ADDRESS_REGION { get; set; }
        public object ADDRESS_PROVINCE { get; set; }
        public object ADDRESS_COUNTRY { get; set; }
        public object ADDRESS_COUNTRY_CODE { get; set; }
        public object ADDRESS_LEGAL { get; set; }
        public object REG_ADDRESS { get; set; }
        public object REG_ADDRESS_2 { get; set; }
        public object REG_ADDRESS_CITY { get; set; }
        public object REG_ADDRESS_POSTAL_CODE { get; set; }
        public object REG_ADDRESS_REGION { get; set; }
        public object REG_ADDRESS_PROVINCE { get; set; }
        public object REG_ADDRESS_COUNTRY { get; set; }
        public object REG_ADDRESS_COUNTRY_CODE { get; set; }
        public object UTM_SOURCE { get; set; }
        public object UTM_MEDIUM { get; set; }
        public object UTM_CAMPAIGN { get; set; }
        public object UTM_CONTENT { get; set; }
        public object UTM_TERM { get; set; }
        public List<int> UF_CRM_5BDAD1A7837D2 { get; set; }
        public string UF_CRM_5C028F68E4BB1 { get; set; }
        public string UF_CRM_5D0B79DBEBAB3 { get; set; }
        public string UF_CRM_5D0B79DC05430 { get; set; }
        public string UF_CRM_5D0CA4461AD19 { get; set; }
        public string UF_CRM_5D10DD656A314 { get; set; }
        public string UF_CRM_5D10DD657CE37 { get; set; }
        public DateTime UF_CRM_5D10DD6587F96 { get; set; }
        public string UF_CRM_5D10DD6594617 { get; set; }
        public string UF_CRM_5D1C56FC97995 { get; set; }
        public string UF_CRM_5D1C56FCBABEA { get; set; }
        public bool UF_CRM_5D5E9BD8A11FC { get; set; }
        public bool UF_CRM_5D5E9BD8A9FDB { get; set; }
        public string UF_CRM_5D5E9BD8B3C4F { get; set; }
        public string UF_CRM_5D5FFD3C0E4D4 { get; set; }
        public string UF_CRM_5D5FFD3C1A443 { get; set; }
        public List<PHONE> PHONE { get; set; }
    }

    public class CompanyResponse
    {
        public Company Company { get; set; }
        public Time time { get; set; }
    }
}