using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Domain.Interfaces;

namespace TicketsDemo.Domain.DefaultImplementations
{
    public class TicketServiceLoggingDecorator: ITicketService
    {
        public ITicketService _decoratedObject;
        public ILogger _logger;

        public TicketServiceLoggingDecorator(ITicketService decoratedObject,  ILogger logger)
        {
            _decoratedObject = decoratedObject;
            _logger = logger;

        }

        public Ticket CreateTicket(int reservationId, string firstName, string lastName)
        {
            try
            {
                var сreatedTicket = _decoratedObject.CreateTicket(reservationId, firstName, lastName);
                string line = string.Format("[{0}] Ticket purchase was successful: firstName:{1}, lastName:{2}, reservationId:{3}",
                                             DateTime.Now, firstName, lastName, reservationId);
                _logger.Log(line, LogSeverity.Info);
                return сreatedTicket;
            }
            catch (Exception e)
            {
                string line = string.Format("[{0}] ERROR in ticket purchase: firstName:{1}, lastName:{2}, reservationId:{3}",
                                             DateTime.Now, firstName, lastName, reservationId);
                _logger.Log(line, LogSeverity.Error);
                throw e;
            }
        }
    }
}
