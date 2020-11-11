using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Repositories;
using TicketsDemo.Domain.Interfaces;

namespace TicketsDemo.Domain.NewImplementations
{
    public class TicketServiceWithLogger : ITicketService
    {
        private ITicketRepository _tickRepo;
        private IPriceCalculationStrategy _priceStr;
        private IReservationRepository _resRepo;
        private IRunRepository _runRepository;
        private ILogger _logger;

        public TicketServiceWithLogger(ITicketRepository tickRepo, IReservationRepository resRepo,
            IPriceCalculationStrategy priceCalculationStrategy, IRunRepository runRepository, ILogger logger)
        {
            _tickRepo = tickRepo;
            _resRepo = resRepo;
            _priceStr = priceCalculationStrategy;
            _runRepository = runRepository;
            _logger = logger;
        }

        public Ticket CreateTicket(int reservationId, string fName, string lName)
        {
            var res = _resRepo.Get(reservationId);

            if (res.TicketId != null)
            {
                throw new InvalidOperationException("ticket has been already issued to this reservation, unable to create another one");
                _logger.Log("ticket has been already issued to this reservation, unable to create another one", 0);
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

            _tickRepo.Create(newTicket);
            _logger.Log($"Created new ticket with ReservationId: {newTicket.ReservationId} " +
                $"for owner {newTicket.FirstName} {newTicket.LastName} .", 0);
            return newTicket;
        }

        public void SellTicket(Ticket ticket)
        {
            if (ticket.Status == TicketStatusEnum.Sold)
            {
                throw new ArgumentException("ticket is already sold");
                _logger.Log($"Ticket with ReservationId: {ticket.ReservationId} is already sold !", 0);
            }

            ticket.Status = TicketStatusEnum.Sold;
            _tickRepo.Update(ticket);
            _logger.Log($"Sold ticket with ReservationId: {ticket.ReservationId} " +
                $"for owner {newTicket.FirstName} {newTicket.LastName} .", 0);
        }
    }
}
