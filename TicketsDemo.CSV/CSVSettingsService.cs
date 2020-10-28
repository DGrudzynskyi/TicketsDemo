using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsDemo.CSV
{
    class CSVSettingsService
    {
        public string TrainCSVPath
        {
            get
            {
                return $@"{AppDomain.CurrentDomain.BaseDirectory}\App_Data\Trains.csv";
            }
        }

        public string CarriageCSVPath
        {
            get
            {
                return $@"{AppDomain.CurrentDomain.BaseDirectory}\App_Data\Carriage.csv";
            }
        }

        public string PlaceCSVPath
        {
            get
            {
                return $@"{AppDomain.CurrentDomain.BaseDirectory}\App_Data\Place.csv";
            }
        }

    }
}
