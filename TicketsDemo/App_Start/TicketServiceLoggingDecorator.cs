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
        private ITicketService _decoratedObject;
        private ILogger _logger;

        public TicketServiceLoggingDecorator(ITicketService DecoratedObject, ILogger logger)
        {
            _decoratedObject = DecoratedObject;
            _logger = logger;
        }
        public Ticket CreateTicket(int reservationId, string firstName, string lastName)
        {
            try
            {
                string message = string.Format("Attempt to buy a ticket: Id {0}, firstName {1}, lastName {2}", reservationId, firstName, lastName);
                _logger.Log(message, LogSeverity.Info);
                return _decoratedObject.CreateTicket(reservationId, firstName, lastName);
            }
            catch (Exception ex)
            {
                string message = string.Format("Error. \n Date: {3} \n Attempt to buy a ticket was failed: Id {0}, firstName {1}, lastName {2}, {4}", reservationId, firstName, lastName, DateTime.Now, ex);
                _logger.Log(message, LogSeverity.Info);
                throw ex;
            }
        }
        
    }
}