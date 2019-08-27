using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B24AirTableIntegration.Lib.Bitrix24
{
    public class User
    {
        public string ID { get; set; }
        public bool ACTIVE { get; set; }
        public string EMAIL { get; set; }
        public string NAME { get; set; }
        public string LAST_NAME { get; set; }
        public string SECOND_NAME { get; set; }
        public string PERSONAL_GENDER { get; set; }
        public object PERSONAL_PROFESSION { get; set; }
        public string PERSONAL_WWW { get; set; }
        public string PERSONAL_BIRTHDAY { get; set; }
        public string PERSONAL_PHOTO { get; set; }
        public object PERSONAL_ICQ { get; set; }
        public object PERSONAL_PHONE { get; set; }
        public object PERSONAL_FAX { get; set; }
        public string PERSONAL_MOBILE { get; set; }
        public object PERSONAL_PAGER { get; set; }
        public object PERSONAL_STREET { get; set; }
        public string PERSONAL_CITY { get; set; }
        public object PERSONAL_STATE { get; set; }
        public object PERSONAL_ZIP { get; set; }
        public object PERSONAL_COUNTRY { get; set; }
        public object WORK_COMPANY { get; set; }
        public string WORK_POSITION { get; set; }
        public string WORK_PHONE { get; set; }
        public List<int> UF_DEPARTMENT { get; set; }
        public object UF_INTERESTS { get; set; }
        public object UF_SKILLS { get; set; }
        public object UF_WEB_SITES { get; set; }
        public object UF_XING { get; set; }
        public object UF_LINKEDIN { get; set; }
        public object UF_FACEBOOK { get; set; }
        public object UF_TWITTER { get; set; }
        public object UF_SKYPE { get; set; }
        public object UF_DISTRICT { get; set; }
        public string UF_PHONE_INNER { get; set; }
        public string USER_TYPE { get; set; }

        public string AirTableString { get => string.Join(" ", NAME, SECOND_NAME, LAST_NAME).Trim().Replace("  ", " "); }
    }
    

    public class UserResponse
    {
        [Newtonsoft.Json.JsonProperty("result")]
        public List<User> Users { get; set; }
        public int total { get; set; }
        public Time time { get; set; }
    }
}