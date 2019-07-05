using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Web;

namespace B24AirTableIntegration.Lib.Helpers
{
    public static class Log
    {
        public static void Debug(string message)
        {
            try
            {
                var data = Encoding.UTF8.GetBytes(message);
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://37.140.192.113/httpdocs/");
                request.Method = WebRequestMethods.Ftp.AppendFile;
                request.ContentLength = data.Length;
                request.Credentials = new NetworkCredential("u0757139", "9fCD9a4_");
                using (Stream requestStream = request.GetRequestStream())
                using (var writer = new StreamWriter(requestStream))
                {
                    writer.Write(message);
                }
                using (var response = request.GetResponse())
                {
                }
            }
            catch { }
            
        }
    }
}