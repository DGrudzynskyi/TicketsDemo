using System;
using System.Collections.Generic;
using TicketsDemo.Data.Entities;
using TicketsDemo.Domain.Interfaces;

namespace TicketsDemo.App_Start
{
    public class TeaCoffeeBedPriceStrategy : IPriceCalculationStrategy
    {
        public TeaCoffeeBedPriceStrategy()
        {

        }
        public List<PriceComponent> CalculatePrice(PriceCalculationParameters parameters)
        {
            return new List<PriceComponent>();
        }
    }
}