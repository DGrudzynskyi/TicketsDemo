using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Repositories;
using TicketsDemo.Domain.Interfaces;
using TicketsDemo.Domain;
using TicketsDemo.Domain.DefaultImplementations.PriceCalculationStrategy;

namespace TicketsDemo.Domain.DefaultImplementations
{
    public class TicketServiceLoggingDecorator : ITicketService
    {
        private ITicketService _decoratedObj;
        private ILogger _logger;

        public TicketServiceLoggingDecorator(ITicketService DecoratedObj, ILogger logger)
        {
            _decoratedObj = DecoratedObj;
            _logger = logger;
        }

        public Ticket CreateTicket(int reservationId, string firstName, string lastName, PriceCalculationInfo info)
        {
            try
            {
                string logMessage = string.Format("Trying to buy a ticket: Id {0}, firstName {1}, lastName {2}",
                                                                           reservationId, firstName, lastName);
                _logger.Log(logMessage, LogSeverity.Info);

                return _decoratedObj.CreateTicket(reservationId, firstName, lastName, info);
            }
            catch (Exception ex)
            {
                string logMessage = string.Format("Error. \n Date: {3} \n Attempt to buy a ticket was failed: " +
                "Id {0}, firstName {1}, lastName {2}, {4}", reservationId, firstName, lastName, DateTime.Now, ex.Message);

                _logger.Log(logMessage, LogSeverity.Info);

                throw ex;
            }
        }

        public void SellTicket(Ticket ticket)
        {
            try
            {
                string logMessage = string.Format("Trying to sell a ticket: Id {0}, firstName {1}, lastName {2}",
                                                                            ticket.ReservationId, ticket.FirstName, ticket.LastName);
                _logger.Log(logMessage, LogSeverity.Info);

                _decoratedObj.SellTicket(ticket);

                logMessage = string.Format("Sold a ticket: Id {0}, firstName {1}, lastName {2}",
                                                                           ticket.ReservationId, ticket.FirstName, ticket.LastName);
                _logger.Log(logMessage, LogSeverity.Info);

            }
            catch (Exception ex)
            {
                string logMessage = string.Format("Error. \n Date: {3} \n Attempt to sell a ticket was failed: " +
                "Id {0}, firstName {1}, lastName {2}, {4}", ticket.ReservationId, ticket.FirstName, ticket.LastName, DateTime.Now, ex.Message);

                _logger.Log(logMessage, LogSeverity.Info);

                throw ex;
            }

        }

    }
}
