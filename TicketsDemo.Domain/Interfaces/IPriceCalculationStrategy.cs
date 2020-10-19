using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Entities.BookingAggregate;

namespace TicketsDemo.Domain.Interfaces
{
    public interface IPriceCalculationStrategy
    {
        List<PriceComponent> CalculatePrice(PlaceInRun placeInRun);

        //add strategy
        List<PriceComponent> CalculatePriceWithCode(PlaceInRun placeInRun, string code);
    }
}
