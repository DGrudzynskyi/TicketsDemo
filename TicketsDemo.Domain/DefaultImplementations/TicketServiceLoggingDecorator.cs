using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Domain.Interfaces;

namespace TicketsDemo.Domain.DefaultImplementations
{
    class TicketServiceLoggingDecorator : ITicketService
    {
        private ITicketService _decoratedobject;
        private ILogger _logger;
        public TicketServiceLoggingDecorator(ITicketService service, ILogger logger)
        {
            _decoratedobject = service;
            _logger = logger;
        }

        public Ticket CreateTicket(int reservationId, string firstName, string lastName)
        {
            StringBuilder sb = new StringBuilder($"\t\t\tAttempt to create ticket with {reservationId}, for {firstName}-{lastName}\n");
            try
            {
                var decorTicket = _decoratedobject.CreateTicket(reservationId, firstName, lastName);
                sb.Append($"Ticket created succesfully for {reservationId} with passenger {firstName}-{lastName}");
                _logger.Log(sb.ToString());
                return decorTicket;
             }
            catch (Exception ex)
            {
                 sb.Append($"{ex.Message} \n Error due to create Ticket for Reservation: {reservationId} with passenger {firstName}-{lastName}");
                _logger.Log(sb.ToString());
                throw ex;
             }
                
            
        }

        public void SellTicket(Ticket ticket)
        {
            _decoratedobject.SellTicket(ticket);
        }
    }
}
