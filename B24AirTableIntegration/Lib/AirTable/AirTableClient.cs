﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Configuration;
using B24AirTableIntegration.Lib.Helpers;
using Newtonsoft.Json;
using B24AirTableIntegration.Lib.Bitrix24;
using System.IO;
using System.Text;

namespace B24AirTableIntegration.Lib.AirTable
{
    public class AirTableClient : ApiClient
    {
        private static object syncRoot = new object();
        private static AirTableClient client;

        public static AirTableClient Instance
        {
            get
            {
                if (client == null)
                {
                    lock (syncRoot)
                    {
                        if (client == null)
                        {
                            client = new AirTableClient();
                        }
                    }
                }
                return client;
            }
        }

        private AirTableClient() : 
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
                string response = Get(Uri.EscapeUriString(TableName), Params);
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

        public void UpdateOrCreate(LeadResponse lead)
        {
            if (lead.Lead != null)
            {
                Log.Debug("Тип лида - " + lead.Lead.Type.ToString());
                if (lead.Lead.IsValid) return;

                var tableName = "Заявки";
                string AtRecord_ID;
                if (IsDealExist(lead.Lead.ID, out AtRecord_ID))
                    return;

                var updating = lead.GetUpdatingRecord();

                string newStatus;
                if (TryGetNewLeadStatus(lead.Lead.Type, lead.Lead.Status, out newStatus))
                {
                    updating.fields.Add("Статус заявки", newStatus);
                }

                if (lead.Lead.ASSIGNED_BY_ID != null)
                {
                    var assignUserID = GetFirstRecordID("Пользователи B24", "{ID B24}='" + lead.Lead.ASSIGNED_BY_ID + "'");
                    if (string.IsNullOrWhiteSpace(assignUserID))
                    {
                        Log.Debug("Обновление справочника пользователей");
                        RefreshUsersDictionary(lead.Lead.AssignUser, out assignUserID);
                        Log.Debug("Новый ID: " + assignUserID);
                    }
                    if (!string.IsNullOrWhiteSpace(assignUserID))
                        updating.fields.Add("Ответственный New", new string[] { assignUserID });
                }

                if (AtRecord_ID != null)
                {
                    UpdateRecord(tableName, AtRecord_ID, updating);
                }
                else
                {
                    updating.fields.Add("Lead_ID", lead.Lead.ID);
                    CreateRecord(tableName, updating);
                }
            }
            else
            {
                Log.Debug("Старый лид");
            }
        }

        private void RefreshUsersDictionary(UserResponse assignUser, out string id)
        {
            if (assignUser.Users != null && assignUser.Users.Count > 0)
            {
                CreationResponse resp = CreateRecord<CreationResponse, UpdatingRecord>("Пользователи B24", new UpdatingRecord
                {
                    fields = new Dictionary<string, object>
                    {
                        { "Name", assignUser.Users[0].NAME },
                        { "ID B24", assignUser.Users[0].ID }
                    }
                });
                id = resp.id;
            }
            else
                id = null;
        }

        public void UpdateOrCreate(DealResponse deal)
        {
            if (deal.Deal != null)
            {
                Log.Debug("Тип сделки - " + deal.Deal.Type.ToString());
                if (deal.Deal.IsValid) return;
                var tableName = "Заявки";
                
                var updating = deal.GetUpdatingRecord();

                string newStatus;
                if (TryGetNewDealStatus(deal.Deal.Type, deal.Deal.Status, out newStatus))
                {
                    updating.fields.Add("Статус заявки", newStatus);
                }

                if (deal.Deal.ASSIGNED_BY_ID != null)
                {
                    var assignUserID = GetFirstRecordID("Пользователи B24", "{ID B24}='"+deal.Deal.ASSIGNED_BY_ID+"'");
                    if (!string.IsNullOrWhiteSpace(assignUserID))
                        updating.fields.Add("Ответственный New", new string[] { assignUserID });
                }

                var AtRecord_ID = GetFirstRecordID(tableName, $"Deal_ID='{deal.Deal.ID}'");

                if (AtRecord_ID != null)
                {
                    UpdateRecord(tableName, AtRecord_ID, updating);
                }
                else
                {
                    updating.fields.Add("Deal_ID", deal.Deal.ID);
                    AtRecord_ID = GetFirstRecordID(tableName, $"Lead_ID='{deal.Deal.LEAD_ID}'");
                    if (AtRecord_ID != null)
                    {
                        UpdateRecord(tableName, AtRecord_ID, updating);
                    }
                    else
                    {
                        updating.fields.Add("Lead_ID", deal.Deal.LEAD_ID);
                        CreateRecord(tableName, updating);
                    }
                }
            }
            else
            {
                Log.Debug("Старая сделка");
            }
        }

        private bool IsDealExist(string Lead_ID, out string recordID)
        {
            recordID = GetFirstRecordID("Заявки", $"AND(Lead_ID='{Lead_ID}',LEN(Deal_ID)>0)");
            if (recordID != null)
                return true;
            else
            {
                recordID = GetFirstRecordID("Заявки", $"Lead_ID='{Lead_ID}'");
                return false;
            }
        }

        private bool TryGetNewDealStatus(BitrixObjectType type, string status, out string newStatus)
        {
            string typeString = "";
            switch (type)
            {
                case BitrixObjectType.None:
                    newStatus = null;
                    return false;
                case BitrixObjectType.B2B:
                    typeString = "Deal_B2B";
                    break;
                case BitrixObjectType.B2C:
                    typeString = "Deal_B2C";
                    break;
            }

            string response = GetRecords("Статусы B24", $"AND(Name='{status}',StatusType='{typeString}')", 1, 1).FirstOrDefault();
            if (response != null)
            {
                var StatusResponce = JsonConvert.DeserializeObject<Statuses>(response);
                if (StatusResponce.records != null && StatusResponce.records.Count > 0 && !string.IsNullOrWhiteSpace(StatusResponce.records[0].fields.AirTableStatus))
                {
                    newStatus = StatusResponce.records[0].fields.AirTableStatus;
                    return true;
                }
            }
            newStatus = null;
            return false;
        }

        private bool TryGetNewLeadStatus(BitrixObjectType type, string status, out string newStatus)
        {
            string typeString = "";
            switch (type)
            {
                case BitrixObjectType.None:
                    newStatus = null;
                    return false;
                case BitrixObjectType.B2B:
                case BitrixObjectType.B2C:
                    typeString = "Lead";
                    break;
            }

            string response = GetRecords("Статусы B24", $"AND(Name='{status}',StatusType='{typeString}')", 1, 1).FirstOrDefault();
            if (response != null)
            {
                var StatusResponce = JsonConvert.DeserializeObject<Statuses>(response);
                if (StatusResponce.records != null && StatusResponce.records.Count > 0 && !string.IsNullOrWhiteSpace(StatusResponce.records[0].fields.AirTableStatus))
                {
                    newStatus = StatusResponce.records[0].fields.AirTableStatus;
                    return true;
                }
            }
            newStatus = null;
            return false;
        }

        private void UpdateRecord<T>(string tableName, string updatingID, T updatingRecord)
        {
            Patch($@"{Uri.EscapeUriString(tableName)}/{updatingID}", updatingRecord);
        }

        private TResponse CreateRecord<TResponse, T>(string tableName, T updatingRecord)
        {
            return JsonConvert.DeserializeObject<TResponse>(Post($@"{Uri.EscapeUriString(tableName)}", updatingRecord));
        }

        private void CreateRecord<T>(string tableName, T updatingRecord)
        {
            Post($@"{Uri.EscapeUriString(tableName)}", updatingRecord);
        }

        private string GetFirstRecordID(string tableName, string filter)
        {
            var data = JsonConvert.DeserializeObject<BaseRecords<BaseRecord>>(GetRecords(tableName, filter, 1, 1).First());
            if (data != null && data.records.Count > 0)
                return data.records[0].id;
            return null;
        }

        #region For Tests
        public string GetFirstRecordIDTest(string tableName, string filter)
        {
            return GetFirstRecordID(tableName, filter);
        }
        #endregion
    }
}