using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.CSV1.Interface;

namespace TicketsDemo.CSV1
{
    public class CSVConfig : ICSVConfig
    {
        public string TrainsFilePath { get { return $@"{AppDomain.CurrentDomain.BaseDirectory}\App_Data\CSV_Data\Trains.csv"; } }

        public string CarriagesFilePath { get { return $@"{AppDomain.CurrentDomain.BaseDirectory}\App_Data\CSV_Data\Carriages.csv"; } }

        public string PlacesFilePath { get { return $@"{AppDomain.CurrentDomain.BaseDirectory}\App_Data\CSV_Data\Places.csv"; } }
    }
}
