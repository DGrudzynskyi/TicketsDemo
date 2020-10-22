using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;

namespace TicketsDemo.Domain.Interfaces
{
    public class PriceCalculationParameters
    {
        public PlaceInRun placeInRun { get; set; }
        public bool Coffee { get; set; }
    }
}
