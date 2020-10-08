using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Repositories;
using TicketsDemo.Domain.Interfaces;

namespace TicketsDemo.Domain.DefaultImplementations
{

    public class ReservationServiceLoggingDecorator : IReservationService
    {
        IReservationService _resServ;
        ILogger _logger;
        string _logLocation;

        public ReservationServiceLoggingDecorator(IReservationService resServ, ILogger logger)
        {
            _resServ = resServ;
            _logger = logger;
            _logLocation = AppDomain.CurrentDomain.BaseDirectory + "log.txt";
        }

        bool IReservationService.IsActive(Reservation reservation)
        {
            return _resServ.IsActive(reservation);
        }

        bool IReservationService.PlaceIsOccupied(PlaceInRun place)
        {
            return _resServ.PlaceIsOccupied(place);
        }

        void IReservationService.RemoveReservation(Reservation reservation)
        {
            var message = "Removed reservation: place " + reservation.PlaceInRunId + "that was reserved on" + reservation.Start;
            _logger.Log(message, LogSeverity.Info);
            _resServ.RemoveReservation(reservation);
        }

        Reservation IReservationService.Reserve(PlaceInRun place)
        {
            try
            {
                var reservation = _resServ.Reserve(place);
                var message = "Successful reservation: place " + place.Id + " in train №" + place.Run.TrainId + " that departs on " + place.Run.Date;
                _logger.Log(message, LogSeverity.Info);
                return reservation;
            }
            catch (InvalidOperationException ex)
            {
                var message = "Unsuccessful reservation: place " + place.Id + " in train №" + place.Run.TrainId + " that departs on " + place.Run.Date + ". Reason: " + ex.Message;
                _logger.Log(message, LogSeverity.Error);
                throw ex;
            }
        }
    }
}
