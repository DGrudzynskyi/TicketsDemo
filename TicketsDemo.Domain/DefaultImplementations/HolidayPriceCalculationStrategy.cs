using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using TicketsDemo.Data.Repositories;
using TicketsDemo.Data.Entities.TicketAggregate;
using TicketsDemo.Mongo;
using MongoDB.Driver;
using TicketsDemo.Domain.Interfaces;
using TicketsDemo.Data.Entities;

namespace TicketsDemo.Domain.DefaultImplementations
{
    public class HolidayPriceCalculationStrategy: IPriceCalculationStrategy
    {
        private IPriceCalculationStrategy _priceCalculationStrategy;
        private IHolidayRepository _holidayRepository;

        public HolidayPriceCalculationStrategy(IPriceCalculationStrategy priceCalculationStrategy, IHolidayRepository holidayRepository)
        {
            _priceCalculationStrategy = priceCalculationStrategy;
            _holidayRepository = holidayRepository;
        }

        public List<PriceComponent> CalculatePrice(PlaceInRun placeInRun)
        {
            List<PriceComponent> components = new List<PriceComponent>();
            var runDate = placeInRun.Run.Date;
            var priceComponents = _priceCalculationStrategy.CalculatePrice(placeInRun);
            Holiday holiday = _holidayRepository.GetInfoAboutHoliday(runDate);

            // Наценка по празднику
            if (holiday != null)
            {
                var value = priceComponents.Select(x => x.Value * holiday.Percent).Sum();
                var holidayPriceComponent = new PriceComponent()
                {
                    Name = holiday.Name,
                    Value = value
                };
                components.Add(holidayPriceComponent);
                return components;
            }

            // Наценка по выходному дню
            if(runDate.DayOfWeek==DayOfWeek.Saturday || runDate.DayOfWeek == DayOfWeek.Sunday)
            {
                var weekendDayPercent = 0.5m;
                var value = priceComponents.Select(x => x.Value * weekendDayPercent).Sum();
                var holidayPriceComponent = new PriceComponent()
                {
                    Name = "Weekend Markup",
                    Value = value
                };
                components.Add(holidayPriceComponent);
                return components;
            }
            return components;
        }
    }
}
