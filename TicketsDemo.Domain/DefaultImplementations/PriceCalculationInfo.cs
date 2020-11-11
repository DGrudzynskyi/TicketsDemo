using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;

namespace TicketsDemo.Domain.DefaultImplementations.PriceCalculationStrategy
{
    public class PriceCalculationInfo
    {
        public PlaceInRun placeInRun { get; set; }
        public bool Coffee { get; set; }
        public bool Tea { get; set; }
        public bool BedLince { get; set; }
    }
}

