using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Domain.Interfaces;

namespace TicketsDemo.Domain.DefaultImplementations
{
    public class TicketServiceLoggingDecorator : ITicketService
    {
        public ITicketService _decoratedObject;
        public ILogger _logger;
        private ISettingsService _settingsService;

        public TicketServiceLoggingDecorator(ITicketService decoratedObject, ILogger logger, ISettingsService settingsService)
        {
            _settingsService = settingsService;
            _decoratedObject = decoratedObject;
            _logger = logger ?? new FileLogger(_settingsService.LogFilePath);
        }

        public Ticket CreateTicket(int reservationId, string firstName, string lastName, string code)
        {
            try
            {
                var сreatedTicket = _decoratedObject.CreateTicket(reservationId, firstName, lastName, code);
                string line = string.Format("[{0}] Ticket purchase was successful: firstName:{1}, lastName:{2}, reservationId:{3}, code{4}",
                                             DateTime.Now, firstName, lastName, reservationId, code);
                _logger.Log(line, LogSeverity.Info);
                return сreatedTicket;
            }
            catch (Exception e)
            {
                string line = string.Format("[{0}] ERROR in ticket purchase: firstName:{1}, lastName:{2}, reservationId:{3}, code:{4}",
                                             DateTime.Now, firstName, lastName, reservationId, code);
                _logger.Log(line, LogSeverity.Error);
                throw e;
            }
        }

        public void SellTicket(Ticket ticket)
        {
            throw new NotImplementedException();
        }
    }
}
