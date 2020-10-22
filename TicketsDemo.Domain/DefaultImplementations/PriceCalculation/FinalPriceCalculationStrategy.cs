using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Domain.Interfaces;

namespace TicketsDemo.Domain.DefaultImplementations
{
    public class FinalPriceCalculationStrategy : IPriceCalculationStrategy
    {
        List<IPriceCalculationStrategy> _strategies;

        public FinalPriceCalculationStrategy(List<IPriceCalculationStrategy> strategies)
        {
            _strategies = strategies;
        }

        public List<PriceComponent> CalculatePrice(PriceCalculationParameters parameters)
        {
            var finalPriceComponents = new List<PriceComponent>();

            foreach(var strategy in _strategies)
            {
                finalPriceComponents.AddRange(strategy.CalculatePrice(parameters));
            }

            return finalPriceComponents;
        }
    }
}
