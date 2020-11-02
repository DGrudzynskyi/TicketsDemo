using System.Collections.Generic;
using System.Linq;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.OptionsForCalculationPrice;
using TicketsDemo.Data.Repositories;
using TicketsDemo.Domain.DefaultImplementations.PriceCalculationStrategy;
using TicketsDemo.Domain.Interfaces;

namespace TicketsDemo.Domain.DefaultImplementations.TeaCoffeeBedPriceCalcStrategy
{
    public class TeaCoffeeBedPriceCalcStrategy : IPriceCalculationStrategy
    {
        private IRunRepository _runRepository;
        private ITrainRepository _trainRepository;


        public TeaCoffeeBedPriceCalcStrategy(IRunRepository runRepository, ITrainRepository trainRepository)
        {
            _runRepository = runRepository;
            _trainRepository = trainRepository;
        }

        public List<PriceComponent> CalculatePrice(TeaCoffeeBedParametrs teaCoffeeBedParametrs)
        {
            var DefaultPriceStrategy = new DefaultPriceCalculationStrategy(_runRepository, _trainRepository);
            var components = DefaultPriceStrategy.CalculatePrice(teaCoffeeBedParametrs);


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
