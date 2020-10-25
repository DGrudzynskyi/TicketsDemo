using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Repositories;
using TicketsDemo.Domain.Interfaces;

namespace TicketsDemo.Domain.DefaultImplementations.PriceCalculationStrategy
{
    public class BookingAgencyPriceCalculationStrategy : IPriceCalculationStrategy
    {
        private IRunRepository _runRepository;
        private ITrainRepository _trainRepository;
        private IAgentRepository _agentRepository;


        public BookingAgencyPriceCalculationStrategy(IRunRepository runRepository, ITrainRepository trainRepository, IAgentRepository agentRepository)
        {
            _runRepository = runRepository;
            _trainRepository = trainRepository;
            _agentRepository = agentRepository;
        }

        public List<PriceComponent> CalculatePrice(PriceCalculationParameters parameters)
        {
            var components = new List<PriceComponent>();
            
            var run = _runRepository.GetRunDetails(parameters.placeInRun.RunId);
            var train = _trainRepository.GetTrainDetails(run.TrainId);
            var agentPercent = _agentRepository.AgentPercent(parameters.agentId);

            var place =
                train.Carriages
                    .Select(car => car.Places.SingleOrDefault(pl =>
                        pl.Number == parameters.placeInRun.Number &&
                        car.Number == parameters.placeInRun.CarriageNumber))
                    .SingleOrDefault(x => x != null);

            var placeComponent = new PriceComponent() { Name = "Main price" };
            placeComponent.Value = place.Carriage.DefaultPrice * place.PriceMultiplier;
            

            if (agentPercent > 0)
            {
                var bookingCompanyComponent = new PriceComponent()
                {
                    Name = "Booking agent services",
                    Value = placeComponent.Value * agentPercent
                };
                components.Add(bookingCompanyComponent);
            }

            return components;
        }
    }
}
