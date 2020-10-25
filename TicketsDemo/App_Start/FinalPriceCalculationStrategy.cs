using System;
using System.Collections.Generic;
using TicketsDemo.Data.Entities;
using TicketsDemo.Domain.Interfaces;
using TicketsDemo.Domain.DefaultImplementations;
using TicketsDemo.Domain;

namespace TicketsDemo.App_Start
{
    public class FinalPriceCalculationStrategy:IPriceCalculationStrategy
    {
        List<IPriceCalculationStrategy> _strategies;

        public FinalPriceCalculationStrategy(List<IPriceCalculationStrategy> strategies)
        {
            _strategies = strategies;
        }

        public List<PriceComponent> CalculatePrice(PriceCalculationParameters parameters)
        {
            var allPriceComponents = new List<PriceComponent>();

            foreach(var s in _strategies)
            {
                allPriceComponents.AddRange(s.CalculatePrice(parameters));
            }

            return allPriceComponents;
        }

    }
}