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
        public TeaCoffeeBedPriceCalcStrategy() {
        }

        public List<PriceComponent> CalculatePrice(PriceCalculationParametersDTO teaCoffeeBedParametrs)
        {
            var components = new List<PriceComponent>();
            if (teaCoffeeBedParametrs.IsTea == true)
            {
                var cashForTea = new PriceComponent()
                {
                    Name = "Pay for tea",
                    Value = 7,

                };
                components.Add(cashForTea);
            }

            if (teaCoffeeBedParametrs.IsCoffee == true)
            {
                var cashForCoffee = new PriceComponent()
                {
                    Name = "Pay for Coffee",
                    Value = 8
                };
                components.Add(cashForCoffee);
            }

            if (teaCoffeeBedParametrs.IsBed == true)
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
