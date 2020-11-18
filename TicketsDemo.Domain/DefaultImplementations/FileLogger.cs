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
        private ILoggerConfig _logConfig;
        
        public FileLogger(string dataFolder, ILoggerConfig logFilePath)
        {
            _dataFolder = dataFolder;
            _logConfig = logFilePath;
        }

        public void Log(string message, LogSeverity severity)
        {
            using (var fileStreamWriter = new StreamWriter(Path.Combine(_dataFolder, _logConfig.LogFilePath), true))
            {
                var writeMessage = String.Format("{0}[{1}]: {2}", severity, DateTime.Now, message);
                fileStreamWriter.WriteLine(writeMessage);
            }
        }
    }
}