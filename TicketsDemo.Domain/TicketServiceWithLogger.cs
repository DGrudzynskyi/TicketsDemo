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
        private ILogger _ticketsLogger;

        public TicketServiceWithLogger(ITicketRepository tickRepo, IReservationRepository resRepo,
            IPriceCalculationStrategy priceCalculationStrategy, IRunRepository runRepository, ILogger logger)
        {
            _tickRepo = tickRepo;
            _resRepo = resRepo;
            _priceStr = priceCalculationStrategy;
            _runRepository = runRepository;
            _ticketsLogger = logger;
        }

        public Ticket CreateTicket(int reservationId, string fName, string lName)
        {
            var res = _resRepo.Get(reservationId);

            if (res.TicketId != null)
            {
                _ticketsLogger.Log("ticket has been already issued to this reservation, unable to create another one", LogSeverity.Debug);
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

            _tickRepo.Create(newTicket);
            _ticketsLogger.Log($"Created new ticket with ReservationId: {newTicket.ReservationId} " +
                $"for owner {newTicket.FirstName} {newTicket.LastName} .", LogSeverity.Info);
            return newTicket;
        }

        public void SellTicket(Ticket ticket)
        {
            if (ticket.Status == TicketStatusEnum.Sold)
            {
                _ticketsLogger.Log($"Ticket with ReservationId: {ticket.ReservationId} is already sold !", LogSeverity.Info);
                throw new ArgumentException("ticket is already sold");
                
            }

            ticket.Status = TicketStatusEnum.Sold;
            _tickRepo.Update(ticket);
            _ticketsLogger.Log($"Sold ticket with ReservationId: {ticket.ReservationId} " +
                $"for owner {ticket.FirstName} {ticket.LastName} .", LogSeverity.Info);
        }
    }
}
