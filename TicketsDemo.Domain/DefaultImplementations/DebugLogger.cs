using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Domain.Interfaces;

namespace TicketsDemo.Domain.DefaultImplementations
{
    class DebugLogger : ILogger
    {
        public void Log(string log_text)
        {
            Debug.WriteLine($"{log_text} + {DateTime.Now.ToString()}");
        }
    }
}
