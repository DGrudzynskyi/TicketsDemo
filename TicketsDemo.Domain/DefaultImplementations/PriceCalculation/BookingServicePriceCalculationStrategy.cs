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
    public class BookingServicePriceCalculationStrategy : IPriceCalculationStrategy
    {
        private IPriceCalculationStrategy _defaultStrategy;
        private IBookingServiceRepository _bookingServiceRepo;
        
        public BookingServicePriceCalculationStrategy(
            IPriceCalculationStrategy defaultStrategy,
            IBookingServiceRepository bookingServiceRepo)
        {
            _defaultStrategy = defaultStrategy;
            _bookingServiceRepo = bookingServiceRepo;
        }

        public List<PriceComponent> CalculatePrice(PriceCalculationParameters parameters)
        {
            var priceComponents = new List<PriceComponent>();
            if (parameters.BookingServiceId != 0)
            {
                var baseParameters = _defaultStrategy.CalculatePrice(parameters);

                var bookingService = _bookingServiceRepo.Get(parameters.BookingServiceId);
                var multiplier = bookingService.HostAgency.FareCoef;

                var newvalue = baseParameters.Select(x => x.Value * multiplier).Sum();

                priceComponents.Add(
                    new PriceComponent
                    {
                        Name = bookingService.Name + " fare",
                        Value = newvalue
                    });
            }

            return priceComponents;
        }
    }
}
