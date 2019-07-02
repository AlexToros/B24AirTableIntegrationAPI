using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B24AirTableIntegration.Lib.Bitrix24
{
    public class SETTINGS
    {
        public string DISPLAY { get; set; }
        public int LIST_HEIGHT { get; set; }
        public string CAPTION_NO_VALUE { get; set; }
        public string SHOW_NO_VALUE { get; set; }
    }

    public class LIST
    {
        public string ID { get; set; }
        public string SORT { get; set; }
        public string VALUE { get; set; }
        public string DEF { get; set; }
    }

    public class UserEnumField
    {
        public string ID { get; set; }
        public string ENTITY_ID { get; set; }
        public string FIELD_NAME { get; set; }
        public string USER_TYPE_ID { get; set; }
        public object XML_ID { get; set; }
        public string SORT { get; set; }
        public string MULTIPLE { get; set; }
        public string MANDATORY { get; set; }
        public string SHOW_FILTER { get; set; }
        public string SHOW_IN_LIST { get; set; }
        public string EDIT_IN_LIST { get; set; }
        public string IS_SEARCHABLE { get; set; }
        public SETTINGS SETTINGS { get; set; }
        public List<LIST> LIST { get; set; }
    }
    

    public class UserEnumFieldResponse
    {
        public List<UserEnumField> result { get; set; }
        public int total { get; set; }
        public Time time { get; set; }
    }
}