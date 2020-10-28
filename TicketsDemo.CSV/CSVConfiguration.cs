using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsDemo.CSV
{
    class CSVConfiguration : ICSVConfiguration
    {
        public string TrainsFilePath { get { return $@"{AppDomain.CurrentDomain.BaseDirectory}\App_Data\Trains.csv"; } }

        public string CarriagesFilePath { get { return $@"{AppDomain.CurrentDomain.BaseDirectory}\App_Data\Carriages.csv"; } }

        public string PlacesFilePath { get { return $@"{AppDomain.CurrentDomain.BaseDirectory}\App_Data\Places.csv"; } }
    }
}
