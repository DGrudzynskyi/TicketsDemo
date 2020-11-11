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
    public class AllPriceDecorator: IPriceCalculationStrategy
    {
        List<IPriceCalculationStrategy> _priceCalculationStrategy;
        public AllPriceDecorator(List<IPriceCalculationStrategy> priceCalculationStrategy)
        {
            _priceCalculationStrategy = priceCalculationStrategy;
        }
        public List<PriceComponent> CalculatePrice(PriceCalculationParametersDTO PriceCalculationParameters)
        {
            var AllPriceComponents = new List<PriceComponent>();
            foreach (var item in _priceCalculationStrategy)
            {
                AllPriceComponents.AddRange(item.CalculatePrice(PriceCalculationParameters));
            }
            
            return AllPriceComponents;
        }
    }
}
