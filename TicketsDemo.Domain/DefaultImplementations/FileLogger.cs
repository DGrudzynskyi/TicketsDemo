using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            using (var fileStreamWriter = new StreamWriter(Path.Combine(_dataFolder, "log.txt"), true))
            {
                fileStreamWriter.WriteLine("{0}: {1}", severity, message);
                Debug.WriteLine("{0}: {1}", severity, message);
            }
        }
    }
}
