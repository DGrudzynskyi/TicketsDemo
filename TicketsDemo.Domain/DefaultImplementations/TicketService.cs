using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Repositories;
using TicketsDemo.Domain.Interfaces;

namespace TicketsDemo.Domain.DefaultImplementations
{
    public class TicketService : ITicketExtentedService
    {
        private ITicketRepository _tickRepo;
        private IPriceCalculationStrategy _priceStr;
        private IExtentedPriceCalculationStrategy _extentedPriceCalculationStrategy;
        private IReservationRepository _resRepo;
        private IRunRepository _runRepository;

        public TicketService(ITicketRepository tickRepo, IReservationRepository resRepo,
            IPriceCalculationStrategy priceCalculationStrategy, IRunRepository runRepository, IExtentedPriceCalculationStrategy extentedPriceCalculationStrategy)
        {
            _extentedPriceCalculationStrategy = extentedPriceCalculationStrategy;
            _tickRepo = tickRepo;
            _resRepo = resRepo;
            _priceStr = priceCalculationStrategy;
            _runRepository = runRepository;
        }

        public Ticket CreateTicket(int reservationId, string fName, string lName)
        {
            var res = _resRepo.Get(reservationId);

            if (res.TicketId != null) {
                throw new InvalidOperationException("ticket has been already issued to this reservation, unable to create another one");
            }

            var placeInRun = _runRepository.GetPlaceInRun(res.PlaceInRunId);

            var newTicket = new Ticket()
            {
                ReservationId = res.Id,
                CreatedDate = DateTime.Now,
                FirstName = fName,
                LastName = lName,
                Status = TicketStatusEnum.Active,
                PriceComponents = new List<PriceComponent>()
            };

            newTicket.PriceComponents = _priceStr.CalculatePrice(placeInRun);

            res.TicketId = newTicket.Id;
            _resRepo.Update(res);

            _tickRepo.Create(newTicket);
            return newTicket;
        }

        public Ticket CreateTicketExtented(int reservationId, string firstName, string lastName, bool drink, bool bed)
        {
            var res = _resRepo.Get(reservationId);

            if (res.TicketId != null)
            {
                throw new InvalidOperationException("ticket has been already issued to this reservation, unable to create another one");
            }

            var placeInRun = _runRepository.GetPlaceInRun(res.PlaceInRunId);

            var newTicket = new Ticket()
            {
                ReservationId = res.Id,
                CreatedDate = DateTime.Now,
                FirstName = firstName,
                LastName = lastName,
                Drink = drink,
                Bed = bed,
                Status = TicketStatusEnum.Active,
                PriceComponents = new List<PriceComponent>()
            };

            newTicket.PriceComponents = _priceStr.CalculatePrice(placeInRun);
            newTicket.PriceComponents.AddRange((List<PriceComponent>)_extentedPriceCalculationStrategy.CalculatePrice(placeInRun.CarriageNumber, newTicket));

            res.TicketId = newTicket.Id;
            _resRepo.Update(res);

            
            _tickRepo.Create(newTicket);
            return newTicket;
        }
    }
}
