using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Domain.Interfaces;
using TicketsDemo.OptionsForCalculationPriceDTO;

namespace TicketsDemo.Domain.DefaultImplementations
{
    public class TeaCoffeeBedDecorator: IPriceCalculationStrategy
    {
        private IPriceCalculationStrategy _priceCalculationStrategy;
        public TeaCoffeeBedDecorator(IPriceCalculationStrategy priceCalculationStrategy)
        {
            _priceCalculationStrategy = priceCalculationStrategy;
        }
        public List<PriceComponent> CalculatePrice(PriceCalculationParametersDTO teaCoffeeBedParametrs)
        {
            return _priceCalculationStrategy.CalculatePrice(teaCoffeeBedParametrs);
        }
    }
}
