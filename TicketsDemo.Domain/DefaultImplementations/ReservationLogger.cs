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
    class ReservationLogger: ILogger
    {
        public string _logLocation;
        public ReservationLogger() 
        {
            _logLocation = AppDomain.CurrentDomain.BaseDirectory + "log.txt";
        }
        public void Log (string messege, LogSeverity severity)
        {
            using (StreamWriter streamWriter = File.AppendText(_logLocation))
            {
                streamWriter.WriteLine(severity + " : " + messege);  
                Debug.WriteLine(severity + " : " + messege);
            }
        }
    }
}
