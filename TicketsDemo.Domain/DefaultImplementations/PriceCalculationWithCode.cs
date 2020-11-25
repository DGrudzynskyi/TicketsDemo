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

namespace TicketsDemo.Domain.DefaultImplementations
{
    public class PriceCalculationWithCode : IPriceCalculationStrategy
    {
        private IRunRepository _runRepository;
        private ITrainRepository _trainRepository;
        private IBookingAgencie _bookingAgencies;

        public PriceCalculationWithCode(IRunRepository runRepository, ITrainRepository trainRepository, IBookingAgencie bookingAgencies)
        {
            _runRepository = runRepository;
            _trainRepository = trainRepository;
            _bookingAgencies = bookingAgencies;
        }  

        public List<PriceComponent> CalculatePrice(TicketParametersDTO parametrs)
        {
            var components = new List<PriceComponent>();

            var run = _runRepository.GetRunDetails(parametrs.placeInRun.RunId);
            var train = _trainRepository.GetTrainDetails(run.TrainId);
            var place =
                train.Carriages
                    .Select(car => car.Places.SingleOrDefault(pl =>
                        pl.Number == parametrs.placeInRun.Number &&
                        car.Number == parametrs.placeInRun.CarriageNumber))
                    .SingleOrDefault(x => x != null);

            var markup = _bookingAgencies.GetMarkup(parametrs.code);


            var placeComponent = new PriceComponent() { Name = "Main price" };
            placeComponent.Value = ((place.Carriage.DefaultPrice * place.PriceMultiplier) + (place.Carriage.DefaultPrice * place.PriceMultiplier) * markup);
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
