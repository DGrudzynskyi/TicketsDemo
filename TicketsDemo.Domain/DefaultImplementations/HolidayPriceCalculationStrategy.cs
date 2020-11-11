using System;
using System.Collections.Generic;
using System.Linq;
using TicketsDemo.Data.DTO;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Repositories;
using TicketsDemo.Domain.Interfaces;

namespace TicketsDemo.Domain.DefaultImplementations
{
    public class HolidayPriceCalculationStrategy : IPriceCalculationStrategy
    {
        private IHolidayRepository _holidayRepository;
        public HolidayPriceCalculationStrategy(IHolidayRepository holidayRepository)
        {
            _holidayRepository = holidayRepository;
        }
        public List<PriceComponent> CalculatePrice(PriceParametersDTO priceParametersDTO)
        {
            var components = new List<PriceComponent>();
            var holidays = _holidayRepository.GetHolidays();
            var isHoliday = holidays.FirstOrDefault(x => x.Date.ToString("d") == priceParametersDTO.PlaceInRun.Run.Date.ToString("d"));


            if (priceParametersDTO.PlaceInRun.Run.Date.DayOfWeek == DayOfWeek.Saturday || priceParametersDTO.PlaceInRun.Run.Date.DayOfWeek == DayOfWeek.Sunday)
            {
                components.Add(new PriceComponent
                {
                    Name = "Weekend",
                    Value = 7
                }
                );
            }


            if (isHoliday != null)
            {
                components.Add(new PriceComponent
                {
                    Name = isHoliday.Name,
                    Value = isHoliday.Markup
                }
                );
            }

            return components;

        }
    }
}
