using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Repositories;
using TicketsDemo.Domain.DefaultImplementations;
using TicketsDemo.Domain.Interfaces;
using TicketsDemo.DTO;

namespace TicketsDemo.Domain.Decorators
{
    public class TicketServiceLoggingDecorator : ITicketService
    {
        private ILogger _logger;
        private ITicketService _ticketService;

        public TicketServiceLoggingDecorator(ILogger logger, ITicketService ticketService)
        {
            _logger = logger;
            _ticketService = ticketService;
        }
        public Ticket CreateTicket(int reservationId, string firstName, string lastName, bool drink = false, bool bed= false)
        {
            try
            {
                var сreatedTicket = _ticketService.CreateTicket(reservationId, firstName, lastName, drink, bed);
                string log = $"Ticket was sold for {firstName} {lastName} (reservation id:{reservationId})";
                _logger.Log(log, LogSeverity.Info);
                return сreatedTicket;
            }
            catch (Exception ex)
            {
                string log  = $"Ticket wasn't sold for {1} {2} (reservationId:{3})\n";
                log += $"ERROR - {ex.Message}";
                _logger.Log(log, LogSeverity.Fatal);
                throw new Exception(ex.Message);
            }
        }
    }
}
