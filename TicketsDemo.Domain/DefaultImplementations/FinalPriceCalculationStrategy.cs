using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Repositories;
using TicketsDemo.Domain.Interfaces;
using TicketsDemo.Domain.DefaultImplementations.PriceCalculationStrategy;
using TicketsDemo.EF.Repositories;
using TicketsDemo.XML;


namespace TicketsDemo.Domain.DefaultImplementations
{
    public class FinalPriceCalculationStrategy : IPriceCalculationStrategy
    {
        List<IPriceCalculationStrategy> _priceCalculationStrategies;

        public FinalPriceCalculationStrategy(List<IPriceCalculationStrategy> priceCalculationStrategies)
        {
            _priceCalculationStrategies = priceCalculationStrategies;
        }

        public List<PriceComponent> CalculatePrice(PlaceInRun placeInRun)
        {
            List<PriceComponent> components = null;
            
            foreach(IPriceCalculationStrategy priceCalculationStrategy in _priceCalculationStrategies)
            {
                var price = priceCalculationStrategy.CalculatePrice(placeInRun);
                if (price != null)
                {
                    if (components == null)
                    {
                        components = price;
                    }
                    else
                    {
                        components.AddRange(price);
                    }
                }
            }
            return components;
        }
    }
}
