﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Repositories;
using TicketsDemo.Domain.Interfaces;

namespace TicketsDemo.Domain.DefaultImplementations
{
    public class HolidayPriceCalculationStrategy : IPriceCalculationStrategy
    {
        private IPriceCalculationStrategy _calculationStrategy;
        private IHolidayRepository _holidayRepository;
        public HolidayPriceCalculationStrategy(IPriceCalculationStrategy strategy, IHolidayRepository holidayRepository)
        {
            _calculationStrategy = strategy;
            _holidayRepository = holidayRepository;
        }
        public List<PriceComponent> CalculatePrice(PlaceInRun placeInRun)
        {
            List<PriceComponent> components = new List<PriceComponent>();
            Holiday holiday = _holidayRepository.GetHolidayDetails(DateTime.Now);
            
            if (holiday != null)
            {
                var priceComponents = _calculationStrategy.CalculatePrice(placeInRun);
                var value = priceComponents.Select(x => x.Value * holiday.Percenте).Sum();
                var holidayPriceComponent = new PriceComponent()
                {
                    Name = "Holiday percent",
                    Value = value
                };
                components.Add(holidayPriceComponent);
            }
            decimal weekendPercent = Weekend(DateTime.Now);
            if (weekendPercent != 0)
            {
                var priceComponents = _calculationStrategy.CalculatePrice(placeInRun);
                var value = priceComponents.Select(x => x.Value * weekendPercent).Sum();
                var weekendPriceComponent = new PriceComponent()
                {
                    Name = "Weekend percent",
                    Value = value
                };
                components.Add(weekendPriceComponent);
            }
            return components;

        }

        protected decimal Weekend(DateTime date)
        {
            double percent = 0;
            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            {
                percent = 0.25;
            }
            return (decimal)percent;
        }
    }
}