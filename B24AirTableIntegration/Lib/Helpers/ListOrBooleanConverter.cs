using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;

namespace B24AirTableIntegration.Lib.Helpers
{
    public class ListOrBooleanConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(List<int>) || objectType == typeof(Boolean) || objectType == null)
            {
                return true;
            }
            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            object retVal = new Object();
            if (reader.TokenType == JsonToken.Boolean)
            {
                retVal = null;
            }
            else if (reader.TokenType == JsonToken.StartArray)
            {
                retVal = serializer.Deserialize(reader, objectType);
            }
            return retVal;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
                serializer.Serialize(writer, false);
            else
                serializer.Serialize(writer, value);
        }
    }
}