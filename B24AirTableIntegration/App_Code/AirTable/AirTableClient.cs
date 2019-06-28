using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Configuration;
using B24AirTableIntegration.App_Code.Helpers;
using Newtonsoft.Json;
using B24AirTableIntegration.App_Code.Bitrix24;

namespace B24AirTableIntegration.App_Code.AirTable
{
    public class AirTableClient : ApiClient
    {
        public AirTableClient() : 
            base(ConfigurationManager.AppSettings["AirTableURL"], new Dictionary<string, string> { { "Authorization", $"Bearer {ConfigurationManager.AppSettings["AirTableKey"]}" } })
        { 
        }

        private IEnumerable<string> GetRecords(string TableName, string filter, int PageSize, int maxCount, params string[] fieldNames)
        {
            var Params = new Dictionary<string, string>();
            if (maxCount > 0)
                Params.Add("maxRecords", maxCount.ToString());
            if (PageSize > 0)
                Params.Add("pageSize", PageSize.ToString());
            if (filter != null)
                Params.Add("filterByFormula", filter);
            for (int i = 0; i < fieldNames.Length; i++)
            {
                Params.Add($"fields[{i}]", fieldNames[i]);
            }
            string offset = null;
            do
            {
                string response = Get(TableName, Params);
                var match = Regex.Match(response, "(\"offset\".+)}");
                if (match.Success)
                {
                    offset = match.Groups[1].Value;
                    var offSetParam = offset.Split(':').Select(x => x.Trim('"')).ToArray();
                    Params[offSetParam[0]] = offSetParam[1];
                }
                else
                    offset = null;
                yield return response;
            } while (offset != null);
        }

        internal void Update(LeadResponse lead)
        {
            throw new NotImplementedException();
        }

        internal void Update(DealResponse deal)
        {
            throw new NotImplementedException();
        }

        private void UpdateRecord<T>(string tableName, string updatingID, T updatingRecord)
        {
            Patch($@"{tableName}/{updatingID}", updatingRecord);
        }

        private string GetFirstRecordID(string tableName, string filter)
        {
            var data = JsonConvert.DeserializeObject<AbstractRecords<AbstractRecord>>(GetRecords(tableName, filter, 1, 1).First());
            if (data != null && data.records.Count > 0)
                return data.records[0].id;
            return "";
        }
    }
}