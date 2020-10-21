using System;
using System.IO;
using TicketsDemo.Domain.Interfaces;

namespace TicketsDemo.Domain.DefaultImplementations
{
    public class Logger : ILogger
    {
        private string _fileName;
        public Logger(string fileName)
        {
            _fileName = fileName;
        }
        public void Log(string message, LogSeverity severity)
        {
            string fullPath = _fileName + "\\DebugLog.txt";

            try
            {
                using (StreamWriter sw = new StreamWriter(fullPath, true, System.Text.Encoding.Default))
                {
                    var wrtStr = String.Format("{0}[{1}]: {2}", severity, DateTime.Now, message);
                    sw.WriteLine(wrtStr);
                }
            }
            catch (Exception ex)
            {
                fullPath = _fileName + "\\DebugError.txt";
                using (StreamWriter sw = new StreamWriter(fullPath, true, System.Text.Encoding.Default))
                {
                    var wrtStr = String.Format("\n{1}\nError: {0} \n ", ex, DateTime.Now);
                    sw.WriteLine(wrtStr);
                }
                throw ex;
            }
        }
    }
}
