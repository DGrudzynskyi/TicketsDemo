using System.Collections.Generic;
using TicketsDemo.Data.Entities;
using TicketsDemo.Domain.DefaultImplementations;

namespace TicketsDemo.Domain.Interfaces
{
    public interface IPriceCalculationStrategy
    {
        List<PriceComponent> CalculatePrice(PriceCalculationParameters parameters);
    }
}
