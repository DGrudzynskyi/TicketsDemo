using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsDemo.CSV
{
    public interface ICSVConfiguration
    {
        string TrainsFilePath { get; }
        string CarriagesFilePath { get; }
        string PlacesFilePath { get; }
    }
}
