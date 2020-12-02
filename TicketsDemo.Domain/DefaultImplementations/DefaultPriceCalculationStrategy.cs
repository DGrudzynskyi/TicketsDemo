using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Entities.BookingAggregate;
using TicketsDemo.Data.Repositories;
using TicketsDemo.Domain.DTO;
using TicketsDemo.Domain.Interfaces;

namespace TicketsDemo.Domain.DefaultImplementations.PriceCalculationStrategy
{
    public class DefaultPriceCalculationStrategy : IPriceCalculationStrategy
    {
        private IRunRepository _runRepository;
        private ITrainRepository _trainRepository;

        public DefaultPriceCalculationStrategy(IRunRepository runRepository, ITrainRepository trainRepository) 
        {
            _runRepository = runRepository;
            _trainRepository = trainRepository;
        }

        public List<PriceComponent> CalculatePrice(TicketParametersDTO parametrs)
        {
            var components = new List<PriceComponent>();

            var run = _runRepository.GetRunDetails(parametrs.PlaceInRun.RunId);
            var train = _trainRepository.GetTrainDetails(run.TrainId);
            var place =
                train.Carriages
                    .Select(car => car.Places.SingleOrDefault(pl =>
                        pl.Number == parametrs.PlaceInRun.Number &&
                        car.Number == parametrs.PlaceInRun.CarriageNumber))
                    .SingleOrDefault(x => x != null);

            var placeComponent = new PriceComponent() { Name = "Main price" };
            placeComponent.Value = place.Carriage.DefaultPrice * place.PriceMultiplier;
            components.Add(placeComponent);

            if (placeComponent.Value > 30)
            {
                var cashDeskComponent = new PriceComponent()
                {
                    Name = "Cash desk service tax",
                    Value = placeComponent.Value * 0.2m
                };
                components.Add(cashDeskComponent);
            }

            return components;
        }
    }
}
