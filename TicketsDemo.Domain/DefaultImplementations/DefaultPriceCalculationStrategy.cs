using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Entities.BookingAggregate;
using TicketsDemo.Data.Repositories;
using TicketsDemo.Domain.Interfaces;

namespace TicketsDemo.Domain.DefaultImplementations.PriceCalculationStrategy
{
    public class DefaultPriceCalculationStrategy : IPriceCalculationStrategy
    {
        private IRunRepository _runRepository;
        private ITrainRepository _trainRepository;
        private IBookingAgencies _bookingAgencies;

        public DefaultPriceCalculationStrategy(IRunRepository runRepository, ITrainRepository trainRepository, IBookingAgencies bookingAgencies) 
        {
            _runRepository = runRepository;
            _trainRepository = trainRepository;
            _bookingAgencies = bookingAgencies;
        }

        public List<PriceComponent> CalculatePrice(PlaceInRun placeInRun)
        {
            var components = new List<PriceComponent>();

            var run = _runRepository.GetRunDetails(placeInRun.RunId);
            var train = _trainRepository.GetTrainDetails(run.TrainId);
            var place = 
                train.Carriages
                    .Select(car => car.Places.SingleOrDefault(pl => 
                        pl.Number == placeInRun.Number && 
                        car.Number == placeInRun.CarriageNumber))
                    .SingleOrDefault(x => x != null);

            var placeComponent = new PriceComponent() { Name = "Main price" };
            placeComponent.Value = place.Carriage.DefaultPrice * place.PriceMultiplier;
            components.Add(placeComponent);


            if (placeComponent.Value > 30) {
                var cashDeskComponent = new PriceComponent()
                {
                    Name = "Cash desk service tax",
                    Value = placeComponent.Value * 0.2m
                };
                components.Add(cashDeskComponent);
            }

            return components;
        }

        public List<PriceComponent> CalculatePriceWithCode(PlaceInRun placeInRun, string code)
        {
            var components = new List<PriceComponent>();

            var run = _runRepository.GetRunDetails(placeInRun.RunId);
            var train = _trainRepository.GetTrainDetails(run.TrainId);
            var place =
                train.Carriages
                    .Select(car => car.Places.SingleOrDefault(pl =>
                        pl.Number == placeInRun.Number &&
                        car.Number == placeInRun.CarriageNumber))
                    .SingleOrDefault(x => x != null);


            var markup = _bookingAgencies.GetMarkup(code);

            var placeComponent = new PriceComponent() { Name = "Main price" };

            if (markup == 0)
            {

                placeComponent.Value = place.Carriage.DefaultPrice * place.PriceMultiplier;
                components.Add(placeComponent);
            }
            else
            {
                placeComponent.Value = ((place.Carriage.DefaultPrice * place.PriceMultiplier) + (place.Carriage.DefaultPrice * place.PriceMultiplier) * markup);
                components.Add(placeComponent);
            }            


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
