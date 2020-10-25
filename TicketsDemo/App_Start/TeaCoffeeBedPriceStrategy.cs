using System;
using System.Collections.Generic;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Repositories;
using TicketsDemo.Domain.DefaultImplementations;
using TicketsDemo.Domain.Interfaces;

namespace TicketsDemo.App_Start
{
    public class TeaCoffeeBedPriceStrategy : IPriceCalculationStrategy
    {
        private ITrainRepository _trainRepository;
        public TeaCoffeeBedPriceStrategy(ITrainRepository trainRepository)
        {

            _trainRepository = trainRepository;
        }
        public List<PriceComponent> CalculatePrice(PriceCalculationParameters parameters)
        {
            var train = _trainRepository.GetTrainDetails(parameters.placeInRun.Run.TrainId);
            var carriageType = train.Carriages.Find(c => c.Number == parameters.placeInRun.CarriageNumber).Type;

            var newPriceComponents = new List<PriceComponent>();

            if (parameters.Coffee == true)
            {
                newPriceComponents.Add(new PriceComponent { Name = "Coffee", Value = 3 });
            }
            if (parameters.Tea == true)
            {
                newPriceComponents.Add(new PriceComponent { Name = "Tea", Value = 2.5m });
            }

            if (parameters.Bed == true && carriageType != CarriageType.Sedentary)
            {
                newPriceComponents.Add(new PriceComponent { Name = "Bed", Value = 5 });
            }
            return newPriceComponents;
        }
    }
}