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
        private string _logFilePath = "DebugLogs.txt";
        
        public FileLogger(string dataFolder)
        {
            _dataFolder = dataFolder;
        }

        public void Log(string message, LogSeverity severity)
        {
            using (var fileStreamWriter = new StreamWriter(Path.Combine(_dataFolder, _logFilePath), true))
            {
                var writeMessage = String.Format("{0}[{1}]: {2}", severity, DateTime.Now, message);
                fileStreamWriter.WriteLine(writeMessage);
            }
        }
    }
}