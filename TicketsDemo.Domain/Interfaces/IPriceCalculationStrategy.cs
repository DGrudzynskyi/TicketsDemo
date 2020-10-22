using System.Collections.Generic;
using TicketsDemo.Data.Entities;

namespace TicketsDemo.Domain.Interfaces
{
    public interface IPriceCalculationStrategy
    {
        List<PriceComponent> CalculatePrice(PriceCalculationParameters parameters);
    }
}
