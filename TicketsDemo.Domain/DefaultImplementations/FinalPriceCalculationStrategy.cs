using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.DTO;
using TicketsDemo.Data.Entities;
using TicketsDemo.Domain.Interfaces;

namespace TicketsDemo.Domain.DefaultImplementations
{
    public class FinalPriceCalculationStrategy : IPriceCalculationStrategy
    {
        protected List<IPriceCalculationStrategy> _priceCalculationStrategies;

        public FinalPriceCalculationStrategy(List<IPriceCalculationStrategy> priceCalculationStrategies)
        {
            _priceCalculationStrategies = priceCalculationStrategies;
        }

        public List<PriceComponent> CalculatePrice(PriceParametersDTO priceParametersDTO)
        {
            var components = new List<PriceComponent>();

            foreach (var strategy in _priceCalculationStrategies)
            {
                components.AddRange(strategy.CalculatePrice(priceParametersDTO));
            }

            return components;
        }
    }
}
