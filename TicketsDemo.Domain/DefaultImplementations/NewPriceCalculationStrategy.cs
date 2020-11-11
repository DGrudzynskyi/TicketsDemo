using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Domain.Interfaces;
using TicketsDemo.Domain.DefaultImplementations;

namespace TicketsDemo.Domain.DefaultImplementations.PriceCalculationStrategy
{   
    public class NewPriceCalculationStrategy : IPriceCalculationStrategy
    {
        List<IPriceCalculationStrategy> _strategies;

        public NewPriceCalculationStrategy(List<IPriceCalculationStrategy> strategies)
        {
            _strategies = strategies;
        }

        public List<PriceComponent> CalculatePrice(PriceCalculationInfo info)
        {
            var allPriceComponents = new List<PriceComponent>();

            foreach (var s in _strategies)
            {
                allPriceComponents.AddRange(s.CalculatePrice(parameters));
            }

            return allPriceComponents;
        }

    }
}
