using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Entities.BookingAggregate;
using TicketsDemo.Domain.DTO;

namespace TicketsDemo.Domain.Interfaces
{
    public interface IPriceCalculationStrategy
    {
        List<PriceComponent> CalculatePrice(TicketParametersDTO parametrs);
    }
}
