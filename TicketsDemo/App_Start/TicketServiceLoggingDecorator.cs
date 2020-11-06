using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Repositories;
using TicketsDemo.Domain.Interfaces;

namespace TicketsDemo.App_Start
{
    public class TicketServiceLoggingDecorator : ITicketService
    {
        private ITicketRepository _tickRepo;
        private ITicketService _decoratedObject;
        private ILogger _logger;

        public TicketServiceLoggingDecorator(ITicketService DecoratedObject, ILogger logger)
        {
            _decoratedObject = DecoratedObject;
            _logger = logger;
        }
        public Ticket CreateTicket(int reservationId, string firstName, string lastName)
        {

            string message = string.Format("Attempt to buy a ticket: Id {0}, firstName {1}, lastName {2}", reservationId, firstName, lastName);
            _logger.Log(message, LogSeverity.Info);
            return _decoratedObject.CreateTicket(reservationId, firstName, lastName);
        }

        public void SellTicket(Ticket ticket)
        {
            if (ticket.Status == TicketStatusEnum.Sold)
            {
                string message = string.Format("Ticket is already sold out: Id {0}, firstName {1}, lastName {2}", ticket.Id, ticket.FirstName, ticket.LastName);
            }
            else
            {
                ticket.Status = TicketStatusEnum.Sold;
                _tickRepo.Update(ticket);
                string message = string.Format("Ticket sold out: Id {0}, firstName {1}, lastName {2}", ticket.Id, ticket.FirstName, ticket.LastName);
            }
            
        }
    }
}