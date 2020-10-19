using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;

namespace TicketsDemo.Domain.Interfaces
{
    public interface IPriceComponentDOCreator
    {
        PriceComponentDO CreatePriceComponentsDO(BookingService bookingService);
    }
}
