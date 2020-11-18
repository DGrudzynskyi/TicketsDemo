using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsDemo.Domain.Interfaces
{
    public interface ILoggerConfig
    {
        string LogFilePath { get; }
    }
}
