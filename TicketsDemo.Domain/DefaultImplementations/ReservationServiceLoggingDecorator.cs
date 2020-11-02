using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Domain.Interfaces;

namespace TicketsDemo.Domain.DefaultImplementations
{
    public class ReservationServiceLoggingDecorator : IReservationService
    {
        public IReservationService _decoratedObject;
        public ILogger _logger;
        public ReservationServiceLoggingDecorator(IReservationService decoratedObject, ILogger logger)
        {
            _decoratedObject = decoratedObject;
            _logger = logger;
        }
        public Reservation Reserve(PlaceInRun place)
        {
            try
            {
                var reservation = _decoratedObject.Reserve(place);
                string line = string.Format("Action time {0}: Successful place reservation: runDate:{1}, place:{2}, carriage:{3}, runId:{4}",
                    DateTime.Now, place.Run.Date, place.Number, place.CarriageNumber, place.Run.TrainId);
                _logger.Log(line, LogSeverity.Info);
                return reservation;
            }
            catch (Exception e)
            {
                string line = string.Format("Action time {0}: Not successful place reservation: runDate:{1}, place:{2}, carriage:{3}, runId:{4}",
                    DateTime.Now, place.Run.Date, place.Number, place.CarriageNumber, place.Run.TrainId);
                _logger.Log(line, LogSeverity.Error);
                throw e;
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
                string line = string.Format("Time {0} :  Successful remove place reservation: resrvationId:{1}, placeInRun:{2}, resrvationStart:{3}",
                    DateTime.Now, reservation.Id, reservation.PlaceInRunId, reservation.Start);
                _logger.Log(line, LogSeverity.Info);
            }
            catch (Exception e)
            {
                string line = string.Format("Time {0} :  Unsuccessful remove place reservation: resrvationId:{1}, placeInRun:{2}, resrvationStart:{3} ",
                    DateTime.Now, reservation.Id, reservation.PlaceInRunId, reservation.Start);
                _logger.Log(line, LogSeverity.Error);
                throw e;
            }
        }
    }
}
