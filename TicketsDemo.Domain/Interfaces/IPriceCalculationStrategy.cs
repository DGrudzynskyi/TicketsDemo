using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.DTO;

namespace TicketsDemo.Domain.Interfaces
{
    public interface IPriceCalculationStrategy
    {
        List<PriceComponent> CalculatePrice(TicketPriceParametersDTO priceParametersDTO);
    }
}
