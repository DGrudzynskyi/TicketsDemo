using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Domain.Interfaces;

namespace TicketsDemo.Domain.DefaultImplementations
{
    public class TicketLogger : ILogger
    {
        private string _Path;

        public TicketLogger(string path)
        {
            _Path = path;
        }

        public void Log(string message, LogSeverity severity)
        {
            string fullPath = _Path + "\\Debug.txt";

            try
            {
                using (StreamWriter sw = new StreamWriter(fullPath, true, System.Text.Encoding.Default))
                {
                    var writeStr = String.Format("{0} {1}: {2}", DateTime.Now, severity, message);
                    sw.WriteLine(writeStr);
                }
            }
            catch (Exception exc)
            {
                fullPath = _Path + "\\Errors.txt";
                using (StreamWriter sw = new StreamWriter(fullPath, true, System.Text.Encoding.Default))
                {
                    var writeStr = String.Format("\n{1}\nError: {0} \n ", exc, DateTime.Now);
                    sw.WriteLine(writeStr);
                }
                throw exc;
            }
        }
    }
}
