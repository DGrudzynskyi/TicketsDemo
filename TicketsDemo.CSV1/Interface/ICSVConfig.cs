using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsDemo.CSV1.Interface
{
    public interface ICSVConfig
    {
        string TrainsFilePath { get; }
        string CarriagesFilePath { get; }
        string PlacesFilePath { get; }
    }
}
