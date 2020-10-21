using System;
using TicketsDemo.Data.Entities;
using TicketsDemo.Domain.Interfaces;

namespace TicketsDemo.Domain.DefaultImplementations
{
    public class LoggingDecorator : IReservationService
    {
        public IReservationService _decoratedObject;
        public ILogger _logger;

        public LoggingDecorator(IReservationService decoratedObject, ILogger logger)
        {
            _decoratedObject = decoratedObject;
            _logger = logger;
        }

        public Reservation Reserve(PlaceInRun place)
        {
            try
            {
                var reservedTicket = _decoratedObject.Reserve(place);
                string logLine = string.Format($"[{DateTime.Now}] Reservation was successful: runID:{place.RunId}, " +
                    $"number:{place.Number}, carriageNumber:{place.CarriageNumber}");

                _logger.Log(logLine, LogSeverity.Info);
                return reservedTicket;
            }
            catch (Exception e)
            {
                string logLine = string.Format($"[{DateTime.Now}] [ERROR] Reservation was failed: runID:{place.RunId}, " +
                    $"number:{place.Number}, carriageNumber:{place.CarriageNumber}");

                _logger.Log(logLine, LogSeverity.Info);
                throw e;
            }
        }

        public void RemoveReservation(Reservation reservation)
        {
            try
            {
                string logLine = string.Format($"[{DateTime.Now}] Reservation was successfully removed: ticketID:{reservation.TicketId}, " +
                    $"placeInRunId:{reservation.PlaceInRunId}");

                _logger.Log(logLine, LogSeverity.Info);
            }
            catch (Exception e)
            {
                string logLine = string.Format($"[{DateTime.Now}][ERROR] Reservation was failed: ticketID:{reservation.TicketId}, " +
                    $"placeInRunId:{reservation.PlaceInRunId}");

                _logger.Log(logLine, LogSeverity.Info);
                throw e;
            }
        }

        public bool IsActive(Reservation reservation)
        {
            throw new NotImplementedException();
        }

        public bool PlaceIsOccupied(PlaceInRun place)
        {
            throw new NotImplementedException();
        }
    }
}