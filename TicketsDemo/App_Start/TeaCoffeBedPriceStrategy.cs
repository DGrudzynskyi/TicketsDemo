using System;
using System.Collections.Generic;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Repositories;
using TicketsDemo.Domain.DefaultImplementations.PriceCalculationStrategy;
using TicketsDemo.Domain.Interfaces;

namespace TicketsDemo.App_Start
{
    public class TeaCoffeBedLincePriceStrategy : IPriceCalculationStrategy
    { 

        private ITrainRepository _trainRepository;

        public TeaCoffeBedLincePriceStrategy TeaCoffeeBedLincePriceStrategy(ITrainRepository trainRepository)
        {
            _trainRepository = trainRepository;
            return this;
        }

        public List<PriceComponent> CalculatePrice(PriceCalculationInfo info)
        {
            var train = _trainRepository.GetTrainDetails(info.placeInRun.Run.TrainId);
            var carriageType = train.Carriages.Find(c => c.Number == info.placeInRun.CarriageNumber).Type;

            var newPriceComponents = new List<PriceComponent>();

            if (info.Coffee == true)
            {
                newPriceComponents.Add(new PriceComponent { Name = "Coffee", Value = 3 });
            }
            if (info.Tea == true)
            { 
                newPriceComponents.Add(new PriceComponent { Name = "Tea", Value = 2.5m });
            }

            if (info.BedLince == true && carriageType != CarriageType.Sedentary)
            {
                newPriceComponents.Add(new PriceComponent { Name = "Bed", Value = 5 });
            }
            return newPriceComponents;
        }
    }
}