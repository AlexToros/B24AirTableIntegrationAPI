using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B24AirTableIntegration.Lib.Bitrix24
{
    public static class BitrixSettings
    {
        public const string GET_LEAD = "crm.lead.get";
        public const string GET_DEAL = "crm.deal.get";
        public const string GET_CONTACT = "crm.contact.get";
        public const string GET_ENUM_VALUES = "crm.status.entity.items";
        public const string GET_USER = "user.get";
        public const string GET_LEAD_USER_ENUM_VALUE = "crm.lead.userfield.list";
        public const string GET_DEAL_USER_ENUM_VALUE = "crm.deal.userfield.list";

        public const string SOURCE_LIST_ID = "SOURCE";
        public const string CONTACT_TYPE_LIST_ID = "CONTACT_TYPE";
        public const string B2B_STATUS_LIST_ID = "DEAL_STAGE_3";
        public const string B2C_STATUS_LIST_ID = "DEAL_STAGE";
        public const string LEAD_STATUS_LIST_ID = "STATUS";
    }
}