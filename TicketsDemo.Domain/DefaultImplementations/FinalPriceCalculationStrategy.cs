using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Domain.Interfaces;

namespace TicketsDemo.Domain.DefaultImplementations
{
    public class FinalPriceCalculationStrategy: IPriceCalculationStrategy
    {
       protected List<IPriceCalculationStrategy> _strategys;
       public FinalPriceCalculationStrategy (List<IPriceCalculationStrategy> strategys)
        {
            _strategys = strategys;
        }

        public List<PriceComponent> CalculatePrice(PriceCalculationParameters parameters)
        {
            var allPriceComponents = new List<PriceComponent>();
            foreach (IPriceCalculationStrategy strategy in _strategys) 
            {
                allPriceComponents.AddRange(strategy.CalculatePrice(parameters));
            }
            decimal totalPrice = 0;
            foreach (PriceComponent component in allPriceComponents)
            {
                totalPrice+=component.Value;
            }
            var FinalPriceComponent = new PriceComponent()
            {
                Name = "Total price",
                Value = totalPrice
            };
            allPriceComponents.Add(FinalPriceComponent);
            return allPriceComponents;
        }
    }
}
