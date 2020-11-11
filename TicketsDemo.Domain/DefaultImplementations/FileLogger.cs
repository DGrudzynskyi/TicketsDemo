using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Domain.Interfaces;

namespace TicketsDemo.Domain.DefaultImplementations
{
    public class FileLogger : ILogger
    {
        private string _dataFolder;
        
        public FileLogger(string dataFolder)
        {
            _dataFolder = dataFolder;
        }

        public void Log(string message, LogSeverity severity)
        {
            string logFilePath;
            switch (severity){
                case LogSeverity.Debug:
                    logFilePath = "DebugLogs.txt";
                    break;
                case LogSeverity.Error:
                    logFilePath = "ErrorLogs.txt";
                    break;
                case LogSeverity.Fatal:
                    logFilePath = "FatalLogs.txt";
                    break;
                case LogSeverity.Info:
                    logFilePath = "InfoLogs.txt";
                    break;
                case LogSeverity.Warning:
                    logFilePath = "WarningLogs.txt";
                    break;
                default:
                    logFilePath = "DebugLogs.txt";
                    break;
            }

            using (var fileStreamWriter = new StreamWriter(Path.Combine(_dataFolder, logFilePath), true))
            {
                var writeMessage = String.Format("{0}[{1}]: {2}", severity, DateTime.Now, message);
                fileStreamWriter.WriteLine(writeMessage);
            }
        }
    }
}