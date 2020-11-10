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
    public class ReservationServiceLogging : ILogger
    {
        public string _logLocation;
        public ReservationServiceLogging()
        {
            _logLocation = AppDomain.CurrentDomain.BaseDirectory + "log.txt";
        }
        public void Log(string messege, LogSeverity severity)
        {
            using (StreamWriter streamWriter = File.AppendText(_logLocation))
            {
                streamWriter.WriteLine(severity + " : " + messege);
            }
        }
    }
}
