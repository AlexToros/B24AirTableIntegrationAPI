using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B24AirTableIntegration.Lib.Bitrix24
{
    public class PHONE
    {
        public string ID { get; set; }
        public string VALUE_TYPE { get; set; }
        public string VALUE { get; set; }
        public string TYPE_ID { get; set; }
    }

    public class EMAIL
    {
        public string ID { get; set; }
        public string VALUE_TYPE { get; set; }
        public string VALUE { get; set; }
        public string TYPE_ID { get; set; }
    }

    public class WEB
    {
        public string ID { get; set; }
        public string VALUE_TYPE { get; set; }
        public string VALUE { get; set; }
        public string TYPE_ID { get; set; }
    }

    public class Contact
    {
        public string ID { get; set; }
        public string POST { get; set; }
        public string COMMENTS { get; set; }
        public object HONORIFIC { get; set; }
        public string NAME { get; set; }
        public string SECOND_NAME { get; set; }
        public string LAST_NAME { get; set; }
        public object PHOTO { get; set; }
        public string LEAD_ID { get; set; }
        public string TYPE_ID { get; set; }
        public string SOURCE_ID { get; set; }
        public string SOURCE_DESCRIPTION { get; set; }
        public string COMPANY_ID { get; set; }
        public string BIRTHDATE { get; set; }
        public string EXPORT { get; set; }
        public string HAS_PHONE { get; set; }
        public string HAS_EMAIL { get; set; }
        public string HAS_IMOL { get; set; }
        public DateTime DATE_CREATE { get; set; }
        public DateTime DATE_MODIFY { get; set; }
        public string ASSIGNED_BY_ID { get; set; }
        public string CREATED_BY_ID { get; set; }
        public string MODIFY_BY_ID { get; set; }
        public string OPENED { get; set; }
        public object ORIGINATOR_ID { get; set; }
        public object ORIGIN_ID { get; set; }
        public object ORIGIN_VERSION { get; set; }
        public object FACE_ID { get; set; }
        public object ADDRESS { get; set; }
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
        public List<int> UF_CRM_5BCF50BA7C6D7 { get; set; }
        public string UF_CRM_5C028F68D9256 { get; set; }
        public string UF_CRM_5D0B79DBCD9DD { get; set; }
        public string UF_CRM_5D0B79DBDCA4C { get; set; }
        public string UF_CRM_5D0CA44606508 { get; set; }
        public string UF_CRM_5D10DD6522CFB { get; set; }
        public string UF_CRM_5D10DD653B295 { get; set; }
        public string UF_CRM_5D10DD6545072 { get; set; }
        public string UF_CRM_5D10DD6550F42 { get; set; }
        public List<PHONE> PHONE { get; set; }
        public List<EMAIL> EMAIL { get; set; }
        public List<WEB> WEB { get; set; }


        [Newtonsoft.Json.JsonIgnore]
        public string Type
        {
            get
            {
                var client = BitrixClient.Instance;
                return client.GetEnumFieldValue(BitrixSettings.CONTACT_TYPE_LIST_ID, TYPE_ID);
            }
        }

        [Newtonsoft.Json.JsonIgnore]
        public string AirTableString
        {
            get
            {
                return $"{string.Join(" ", NAME, SECOND_NAME, LAST_NAME).Trim().Replace("  ", " ")} ({string.Join("; ", PHONE.Select(p => p.VALUE))})"; 
            }
        }
    }
    

    public class ContactResponse
    {
        [Newtonsoft.Json.JsonProperty("result")]
        public Contact Contact { get; set; }
        public Time time { get; set; }
    }
}