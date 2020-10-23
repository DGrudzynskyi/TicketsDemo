using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;

namespace TicketsDemo.Domain.Interfaces
{
    public interface IExtentedPriceCalculationStrategy:IPriceCalculationStrategy
    {
        IList<PriceComponent> CalculatePrice(int carriageNumber,Ticket ticket);
    }
}
