using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace B24AirTableIntegration.App_Code.Helpers
{
    public class ApiClient
    {
        private string RootUrl { get; set; }
        private Dictionary<string, string> Headers { get; set; }
        private Dictionary<string, string> PermanentParams { get; set; }

        protected ApiClient(string RootUrl, Dictionary<string, string> Headers, Dictionary<string, string> PermanentParams = null)
        {
            ServicePointManager.Expect100Continue = true;
            if (PermanentParams != null)
                this.PermanentParams = PermanentParams;
            else
                this.PermanentParams = new Dictionary<string, string>();

            if (Headers != null)
                this.Headers = Headers;
            else
                this.Headers = new Dictionary<string, string>();

            this.Headers.Add("Content-Type", "application/json");
            this.RootUrl = RootUrl;
        }
        protected T Get<T>(string EndPoint, Dictionary<string, string> parameters = null)
        {
            return JsonConvert.DeserializeObject<T>(Get(EndPoint, parameters));
        }

        protected string Get(string EndPoint, Dictionary<string, string> parameters = null)
        {

            string url = $"{Path.Combine(RootUrl, EndPoint).Trim('/')}?{GetTotalParamString(parameters)}".Trim('?');
            string result;
            using (var client = GetClient())
            {
                result = Encoding.UTF8.GetString(client.DownloadData(url));
            }

            return result;
        }

        protected string Put<T>(string EndPoint, T content, Dictionary<string, string> parameters = null)
        {
            return Put(EndPoint, JsonConvert.SerializeObject(content), parameters);
        }

        protected string Put(string EndPoint, string content, Dictionary<string, string> parameters = null)
        {
            return UploadContent(EndPoint, content, parameters, "PUT");
        }

        protected string Patch<T>(string EndPoint, T content, Dictionary<string, string> parameters = null)
        {
            return Patch(EndPoint, JsonConvert.SerializeObject(content), parameters);
        }

        protected string Patch(string EndPoint, string content, Dictionary<string, string> parameters = null)
        {
            return UploadContent(EndPoint, content, parameters, "PATCH");
        }

        protected string Post<T>(string EndPoint, T content, Dictionary<string, string> parameters = null)
        {
            return Post(EndPoint, JsonConvert.SerializeObject(content), parameters);
        }

        protected string Post(string EndPoint, string content, Dictionary<string, string> parameters = null)
        {
            return UploadContent(EndPoint, content, parameters);
        }

        private string UploadContent(string EndPoint, string content, Dictionary<string, string> parameters = null, string method = "POST")
        {
            string url = $"{Path.Combine(RootUrl, EndPoint).Trim('/')}?{GetTotalParamString(parameters)}".Trim('?');
            byte[] result;
            using (var client = GetClient())
            {
                result = client.UploadData(url, method, Encoding.UTF8.GetBytes(content));
            }
            return Encoding.UTF8.GetString(result);
        }

        private string GetTotalParamString(Dictionary<string, string> parameters)
        {
            string res = "";
            string ParamString = parameters?.ToUrlString();
            if (!string.IsNullOrEmpty(ParamString))
                res = ParamString;
            ParamString = PermanentParams.ToUrlString();
            if (!string.IsNullOrEmpty(res))
            {
                if (!string.IsNullOrEmpty(ParamString))
                    res += $"&{ParamString}";
            }
            else
            {
                if (!string.IsNullOrEmpty(ParamString))
                    res += $"{ParamString}";
            }
            return res;
        }

        private WebClient GetClient()
        {
            var client = new WebClient();
            foreach (var item in Headers)
            {
                client.Headers.Add(item.Key, item.Value);
            }
            return client;
        }
    }

    public static class Extensions
    {
        public static string ToUrlString(this Dictionary<string, string> dict)
        {
            if (dict.Count == 0)
                return "";
            StringBuilder sb = new StringBuilder();
            foreach (var item in dict)
            {

                sb.AppendFormat("{0}={1}&", item.Key, Uri.EscapeDataString(item.Value));
            }
            return sb.Remove(sb.Length - 1, 1).ToString();
        }
    }
}
