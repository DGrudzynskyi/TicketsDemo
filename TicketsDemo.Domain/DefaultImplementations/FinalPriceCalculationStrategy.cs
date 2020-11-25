using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Domain.DTO;
using TicketsDemo.Domain.Interfaces;

namespace TicketsDemo.Domain.DefaultImplementations
{
    public class FinalPriceCalculationStrategy : IPriceCalculationStrategy
    {
        private List<IPriceCalculationStrategy> _strategys;
        public FinalPriceCalculationStrategy(List<IPriceCalculationStrategy> strategys)
        {
            _strategys = strategys;
        }

        public List<PriceComponent> CalculatePrice(TicketParametersDTO parameters)
        {
            var PriceComponents = new List<PriceComponent>();

            foreach (IPriceCalculationStrategy strategy in _strategys)
            {
                PriceComponents.AddRange(strategy.CalculatePrice(parameters));
            }

            return PriceComponents;
        }
    }
}
