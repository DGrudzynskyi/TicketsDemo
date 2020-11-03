using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Domain.Interfaces;
using TicketsDemo.DTO;

namespace TicketsDemo.Domain.PriceCalculationStrategies
{
    public class FinalPriceCalculationStrategy : IPriceCalculationStrategy
    {
        protected List<IPriceCalculationStrategy> _strategies;
        public FinalPriceCalculationStrategy(List<IPriceCalculationStrategy> strategies)
        {
            _strategies = strategies;
        }
        public List<PriceComponent> CalculatePrice(TicketPriceParametersDTO priceParametersDTO)
        {
            var components = new List<PriceComponent>();
            foreach (var strategy in _strategies)
            {
                components.AddRange(strategy.CalculatePrice(priceParametersDTO));
            }
            return components;
        }
    }
}
