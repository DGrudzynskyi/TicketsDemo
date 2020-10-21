using System;
using TicketsDemo.Data.Entities;
using TicketsDemo.Domain.Interfaces;

namespace TicketsDemo.App_Start
{
    public class ReservationLoggingDecorator : IReservationService
    {
        public IReservationService _decoratedObject;
        private ILogger _logger;

        public ReservationLoggingDecorator(IReservationService DecoratedObject, ILogger logger)
        {
            _decoratedObject = DecoratedObject;
            _logger = logger;
        }

        public Reservation Reserve(PlaceInRun place)
        {
            try
            {
                var reserve = _decoratedObject.Reserve(place);
                string message = string.Format("Successfully make a reservation for a ticket with " +
                    "Id {0}, " +
                    "number {2}" +
                    " for run {1}" +
                    " with run Id {3}.",
                place.Id,
                place.Run, 
                place.Number, 
                place.RunId);
                _logger.Log(message, LogSeverity.Info);
                return reserve;
            }
            catch(Exception exception)
            {
                string message = string.Format("Attempt to reserve a ticket with " +
                    "Id {0}," +
                    " number {2} " +
                    "for run {1} " +
                    "with run Id {3} was failed.",
                place.Id, 
                place.Run, 
                place.Number, 
                place.RunId);
                _logger.Log(message, LogSeverity.Error);
         
                throw exception;
            }
            
        }

        public bool IsActive(Reservation reservation)
        {
            return _decoratedObject.IsActive(reservation);
        }

        public bool PlaceIsOccupied(PlaceInRun place)
        {
            return _decoratedObject.PlaceIsOccupied(place);
        }

        public void RemoveReservation(Reservation reservation)
        {
            try
            {
                _decoratedObject.RemoveReservation(reservation);
                string message = string.Format("Successfully remove a reservation for a place with " +
                    "reservation Id {0} " +
                    "for place in run Id {1} " +
                    "with ticket Id {2}.",
                reservation.Id, 
                reservation.PlaceInRunId,
                reservation.TicketId);
                _logger.Log(message, LogSeverity.Info);
            }
            catch (Exception exception)
            {
                string message = string.Format("Attempt to remove a reservation for a place " +
                    "with reservation Id {0}" +
                    " for place in run Id {1}" +
                    " with ticket Id {2} was failed.",
                reservation.Id, reservation.PlaceInRunId, reservation.TicketId);
                _logger.Log(message, LogSeverity.Error);

                throw exception;
            }
        }
    }
}