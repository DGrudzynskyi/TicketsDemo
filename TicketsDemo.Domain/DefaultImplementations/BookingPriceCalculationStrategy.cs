using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Domain.DTO;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Repositories;
using TicketsDemo.Domain.Interfaces;

namespace TicketsDemo.Domain.DefaultImplementations
{
    public class BookingPriceCalculationStrategy : IPriceCalculationStrategy
    {
        private IRunRepository _runRepository;
        private ITrainRepository _trainRepository;
        private IRepresentativeRepository _representativeRepository;

        public BookingPriceCalculationStrategy(
            IRunRepository runRepository, 
            ITrainRepository trainRepository,
            IRepresentativeRepository representativeRepository)
        {
            _runRepository = runRepository;
            _trainRepository = trainRepository;
            _representativeRepository = representativeRepository;
        }

        public List<PriceComponent> CalculatePrice(TicketParametersDTO parameters)
        {
            var components = new List<PriceComponent>();

            var agency = _representativeRepository.GetRepresentative(parameters.agencyCode);
            var run = _runRepository.GetRunDetails(parameters.placeInRun.RunId);
            var train = _trainRepository.GetTrainDetails(run.TrainId);
            var place =
                train.Carriages
                    .Select(car => car.Places.SingleOrDefault(pl =>
                        pl.Number == parameters.placeInRun.Number &&
                        car.Number == parameters.placeInRun.CarriageNumber))
                    .SingleOrDefault(x => x != null);

            var placeComponent = new PriceComponent() { Name = "Main price" };
            placeComponent.Value = place.Carriage.DefaultPrice * place.PriceMultiplier;
            //components.Add(placeComponent);


            if (agency != null)
            {
                var bookingAgencyComponent = new PriceComponent()
                {
                    Name = String.Format("{0} markup", agency.BookingAgency.Name),
                    Value = placeComponent.Value * agency.BookingAgency.Markup
                };
                components.Add(bookingAgencyComponent);
            }

            return components;
        }
    }
}
