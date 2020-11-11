using System.Collections.Generic;
using System.Linq;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Repositories;
using TicketsDemo.Domain.DefaultImplementations.PriceCalculationStrategy;
using TicketsDemo.Domain.Interfaces;
using TicketsDemo.OptionsForCalculationPriceDTO;

namespace TicketsDemo.Domain.DefaultImplementations
{
    public class TeaCoffeeBedPriceCalcStrategy : IPriceCalculationStrategy
    {
        private IPriceCalculationStrategy _priceCalculationStrategy;
        public TeaCoffeeBedPriceCalcStrategy(IPriceCalculationStrategy priceCalculationStrategy) {
            _priceCalculationStrategy = priceCalculationStrategy;
        }

        public List<PriceComponent> CalculatePrice(PriceCalculationParametersDTO PriceCalculationParameters)
        {
            var components = new List<PriceComponent>();
            components.AddRange(_priceCalculationStrategy.CalculatePrice(PriceCalculationParameters));
            if (PriceCalculationParameters.IsTea == true)
            {
                var cashForTea = new PriceComponent()
                {
                    Name = "Pay for tea",
                    Value = 7,

                };
                components.Add(cashForTea);
            }

            if (PriceCalculationParameters.IsCoffee == true)
            {
                var cashForCoffee = new PriceComponent()
                {
                    Name = "Pay for Coffee",
                    Value = 8
                };
                components.Add(cashForCoffee);
            }

            if (PriceCalculationParameters.IsBed == true)
            {
                var cashForBed = new PriceComponent()
                {
                    Name = "Pay for Bed",
                    Value = 15
                };
                components.Add(cashForBed);
            }
            return components;
        }

    }
}
