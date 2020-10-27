using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Repositories;
using TicketsDemo.Domain.Interfaces;
using TicketsDemo.DTO;

namespace TicketsDemo.Domain.PriceCalculationStrategies
{
    public class DrinkAndBedCalculationStrategy : IPriceCalculationStrategy
    {
        private IRunRepository _runRepository;
        private ITrainRepository _trainRepository;
        private ICarriageRepository _carriageRepository;

        public DrinkAndBedCalculationStrategy(IRunRepository runRepository, ITrainRepository trainRepository, ICarriageRepository carriageRepository)
        {
            _runRepository = runRepository;
            _trainRepository = trainRepository;
            _carriageRepository = carriageRepository;
        }

        public List<PriceComponent> CalculatePrice(TicketPriceParametersDTO priceParametersDTO)
        {
            var components = new List<PriceComponent>();

            var run = _runRepository.GetRunDetails(priceParametersDTO.PlaceInRun.RunId);
            var train = _trainRepository.GetTrainDetails(run.TrainId);
            var place =
                train.Carriages
                    .Select(car => car.Places.SingleOrDefault(pl =>
                        pl.Number == priceParametersDTO.PlaceInRun.Number &&
                        car.Number == priceParametersDTO.PlaceInRun.CarriageNumber))
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


            CarriageType carriagetype = _carriageRepository.GetCarriage(priceParametersDTO.PlaceInRun.CarriageNumber).Type;

            if (priceParametersDTO.Drink == true)
            {
                components.Add(new PriceComponent { Name = "Drink price", Value = 5, Ticket = priceParametersDTO.Ticket, TicketId = priceParametersDTO.Ticket.Id });
            }
            if (priceParametersDTO.Bed == true && carriagetype != CarriageType.Sedentary)
            {
                components.Add(new PriceComponent { Name = "Bed price", Value = 15, Ticket = priceParametersDTO.Ticket, TicketId = priceParametersDTO.Ticket.Id });
            }

            return components;
        }
    }
}
