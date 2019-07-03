using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;

namespace B24AirTableIntegration.Lib.Helpers
{
    public static class Log
    {
        public static void Debug(string message)
        {
            using (StreamWriter sw = new StreamWriter(@"C:\Test\log.txt", true))
            {
                sw.WriteLine($"DEBUG ({DateTime.Now}) THREAD_ID {Thread.CurrentThread.ManagedThreadId} : {message}");
            }
        }
    }
}