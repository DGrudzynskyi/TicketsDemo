using System;
using System.Collections.Generic;
using TicketsDemo.Data.Entities;
using TicketsDemo.Domain.Interfaces;

namespace TicketsDemo.App_Start
{
    public class FinalPriceCalculationStrategy:IPriceCalculationStrategy
    {
        List<IPriceCalculationStrategy> _strategies;

        public FinalPriceCalculationStrategy(List<IPriceCalculationStrategy> strategies)
        {
            _strategies = strategies;
        }

        public List<PriceComponent> CalculatePrice(PlaceInRun placeInRun)
        {
            var allPriceComponents = new List<PriceComponent>();

            foreach (var stratagy in _strategies)
            {
                allPriceComponents.AddRange(stratagy.CalculatePrice(placeInRun));
            }

            return allPriceComponents;
        }
    }
}