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
    public class DefaultPriceCalculationStrategy : IExtentedPriceCalculationStrategy
    {
        private IRunRepository _runRepository;
        private ITrainRepository _trainRepository;
        private ICarriageRepository _carriageRepository;

        public DefaultPriceCalculationStrategy(IRunRepository runRepository, ITrainRepository trainRepository, ICarriageRepository carriageRepository) {
            _runRepository = runRepository;
            _trainRepository = trainRepository;
            _carriageRepository = carriageRepository;
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

        public IList<PriceComponent> CalculatePrice(int carriageNumber, Ticket ticket)
        {
            var components = new List<PriceComponent>();
            CarriageType carriagetype = (CarriageType)_carriageRepository.GetCarriage(carriageNumber).Number;

            if (ticket.Drink == true)
            {
                components.Add(new PriceComponent { Name = "Drink price", Value = 5, Ticket = ticket, TicketId = ticket.Id });
            }
            if(ticket.Bed == true &&  carriagetype != CarriageType.Sedentary)
            {
                components.Add(new PriceComponent { Name = "Bed price", Value = 15, Ticket = ticket, TicketId = ticket.Id });
            }
            return components;
        }
    }
}
