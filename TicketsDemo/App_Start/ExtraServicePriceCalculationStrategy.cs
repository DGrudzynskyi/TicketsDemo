using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Domain.Interfaces;
using TicketsDemo.Data.Entities;
using TicketsDemo.Domain.DefaultImplementations.PriceCalculationStrategy;

namespace TicketsDemo.App_Start
{
    public class ExtraServicePriceCalculationStrategy : IPriceCalculationStrategy
    {
        List<IPriceCalculationStrategy> _strategies;

        public ExtraServicePriceCalculationStrategy(List<IPriceCalculationStrategy> strategies)
        {
            _strategies = strategies;
        }

        public List<PriceComponent> CalculatePrice(PriceCalculationInfo info)
        {
            var allPriceComponents = new List<PriceComponent>();

            foreach (var s in _strategies)
            {
                allPriceComponents.AddRange(s.CalculatePrice(info));
            }

            return allPriceComponents;
        }

    }
}
