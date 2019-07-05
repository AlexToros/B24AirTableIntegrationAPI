using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B24AirTableIntegration.Lib.Bitrix24
{
    public enum BitrixObjectType
    {
        None = 0,
        Lead_B2B = 55,
        Lead_B2C = 45,
        Deal_B2B = 69, 
        Deal_B2B2 = 75,
        Deal_B2C = 73
    }
}