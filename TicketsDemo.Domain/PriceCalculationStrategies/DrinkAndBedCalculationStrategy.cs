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

        public DrinkAndBedCalculationStrategy(IRunRepository runRepository, ITrainRepository trainRepository)
        {
            _runRepository = runRepository;
            _trainRepository = trainRepository;
        }

        public List<PriceComponent> CalculatePrice(TicketPriceParametersDTO priceParametersDTO)
        {
            var components = new List<PriceComponent>();

            var run = _runRepository.GetRunDetails(priceParametersDTO.PlaceInRun.RunId);
            var train = _trainRepository.GetTrainDetails(run.TrainId);
            var carriagetype = train.Carriages.FirstOrDefault(x => x.Number == priceParametersDTO.PlaceInRun.CarriageNumber).Type;

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
