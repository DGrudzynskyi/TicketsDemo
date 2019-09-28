using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Repositories;
using TicketsDemo.Domain.DefaultImplementations.PriceCalculationStrategy;
using TicketsDemo.Domain.Interfaces;

namespace TicketsDemo.Domain.DefaultImplementations
{
   public class AgencyPriceCalculationStrategy : IPriceCalculationStrategy
    {
        private IRunRepository _runRepository;
        private ITrainRepository _trainRepository;

        public AgencyPriceCalculationStrategy(IRunRepository runRepository, ITrainRepository trainRepository)
        {
            _runRepository = runRepository;
            _trainRepository = trainRepository;
        }

        public List<PriceComponent> CalculatePrice(PlaceInRun placeInRun)
        {
            var priceComponents = new List<PriceComponent>();
            var run = _runRepository.GetRunDetails(placeInRun.RunId);
            var train = _trainRepository.GetTrainDetails(run.TrainId);
            var agencyPrice = new PriceComponent()
            {
                Name = "AgencyFee",
                Value = train.trainAgency.PriceForUsing
            };
            priceComponents.Add(agencyPrice);
            return priceComponents;

        }
    }
}
