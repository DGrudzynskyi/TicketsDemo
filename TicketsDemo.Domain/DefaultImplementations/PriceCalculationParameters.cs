using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;

namespace TicketsDemo.Domain.DefaultImplementations
{
    public class PriceCalculationParameters
    {
        public PlaceInRun placeInRun { get; set; }
        public bool Coffee { get; set; }
        public bool Bed { get; set; }
        public bool Tea { get; set; }

    }
}
