using System;
using System.Diagnostics;
using System.IO;
using TicketsDemo.Domain.Interfaces;

namespace TicketsDemo.Domain.DefaultImplementations
{
    public class FileLogger : ILogger
    {
        private string _dataFolder;

        public FileLogger()
        {
            _dataFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log.txt");
        }
        public FileLogger(string dataFolder)
        {
            _dataFolder = dataFolder;
        }

        public void Log(string message, LogSeverity severity)
        {
            using (var fileStreamWriter = new StreamWriter(Path.Combine(_dataFolder, "log.txt"), true))
            {
                var wrtStr = String.Format("{0}[{1}]: {2}", severity, DateTime.Now, message);
                fileStreamWriter.WriteLine(wrtStr);
                fileStreamWriter.WriteLine($"{severity}: {message}");

                Debug.WriteLine($"{severity}: {message}");
            }
        }
    }
}
