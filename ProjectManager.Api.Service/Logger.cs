using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace ProjectManager.Api.Service
{
    public class Logger
    {
        public static void WriteLog(string errorMessage)
        {
            var content = "Error Message: " + errorMessage + Environment.NewLine +
                "Date: " + DateTime.Now + Environment.NewLine +
                "*****************************************************";
            string logpath = ConfigurationManager.AppSettings["logPath"].ToString();

            using (StreamWriter sw = new StreamWriter(logpath, true))
            {
                sw.WriteLine(content);
            }
        }
    }
}