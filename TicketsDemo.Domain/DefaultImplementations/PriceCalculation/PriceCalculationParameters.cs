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
        public PlaceInRun PlaceInRun { get; set; }
        public int BookingServiceId { get; set; }
    }
}
