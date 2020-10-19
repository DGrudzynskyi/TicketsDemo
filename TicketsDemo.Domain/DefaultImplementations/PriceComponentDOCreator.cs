using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Repositories;
using TicketsDemo.Domain.Interfaces;

namespace TicketsDemo.Domain.DefaultImplementations
{
    public class PriceComponentDOCreator : IPriceComponentDOCreator
    {
        public PriceComponentDO CreatePriceComponentsDO( BookingService bookingService)
        {
            return new PriceComponentDO
            {
                Name = bookingService.Name + " fare",
                Value = bookingService.HostAgency.FareCoef,
                Type = PriceComponentDOType.Relative
            };
        }
    }
}
