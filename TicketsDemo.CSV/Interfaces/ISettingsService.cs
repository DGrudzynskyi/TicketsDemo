using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsDemo.CSV.Interfaces
{
    interface ISettingsService
    {
        public string TrainCSVPath { get; }
        public string CarriageCSVPath { get; }
        public string PlaceCSVPath { get; }
    }
}
