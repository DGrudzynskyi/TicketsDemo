using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Repositories;
using TicketsDemo.Domain.Interfaces;
using TicketsDemo.XML;


namespace TicketsDemo.Domain.DefaultImplementations
{
    public class WeekendsAndHolidaysPriceCalculationStrategy : IPriceCalculationStrategy
    {
        private IHolidayRepository _holidayRepository;
        
        public WeekendsAndHolidaysPriceCalculationStrategy(IHolidayRepository holidayRepository)
        {
            _holidayRepository = holidayRepository;
        }

        public List<PriceComponent> CalculatePrice(PlaceInRun placeInRun)
        {
            var components = new List<PriceComponent>();
            var runDate = placeInRun.Run.Date;
            foreach (Holiday holiday in _holidayRepository.GetHolidaysList())
            {
                if (runDate.Day == holiday.Date.Day && runDate.Month == holiday.Date.Month && runDate.Year == holiday.Date.Year)
                {
                    var HolidayComponent = new PriceComponent()
                    {
                        Name = $"Holiday service tax for {holiday.Name}",
                        Value = 200m
                    };
                    components.Add(HolidayComponent);
                    return components;
                }
            }
            if (runDate.DayOfWeek == DayOfWeek.Saturday || runDate.DayOfWeek == DayOfWeek.Sunday)
            {
                var WeekendComponent = new PriceComponent()
                {
                    Name = "Weekend service tax",
                    Value = 50m
                };
                components.Add(WeekendComponent);
            }
            else
            {
                components = null;
            }
            return components;
        }
    }
}
