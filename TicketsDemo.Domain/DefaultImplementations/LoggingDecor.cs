using System;
using TicketsDemo.Data.Entities;
using TicketsDemo.Domain.Interfaces;

namespace TicketsDemo.Domain.DefaultImplementations
{
    public class LoggingDecor : IReservationService
    {
        private IReservationService _decorObject;
        private ILogger _logger;
        private string _path;

        public LoggingDecor(IReservationService reservation, ILogger logger)
        {
            _decorObject = reservation;
            _logger = logger;
            _path = $"{AppDomain.CurrentDomain.BaseDirectory}log.txt"; 
        }

        public bool IsActive(Reservation reservation)
        {
            return _decorObject.IsActive(reservation);
        }

        public bool PlaceIsOccupied(PlaceInRun place)
        {
            return _decorObject.PlaceIsOccupied(place);
        }

        public void RemoveReservation(Reservation reservation)
        {
            try
            {
                _decorObject.RemoveReservation(reservation);
                var message = $"Reservation is successfully removed\nPlace: {reservation.PlaceInRunId}";

                _logger.Log(message, LogSeverity.Info);
            }
            catch (Exception ex)
            {
                var errorMassage = $"Unable to remove reservation\nPlace: {reservation.PlaceInRunId}";


                _logger.Log(errorMassage, LogSeverity.Error);
                throw ex;
            }
        }

        public Reservation Reserve(PlaceInRun place)
        {
            try
            {
                var reserv = _decorObject.Reserve(place);
                var message = $"Reservation is successful\nPlace: {place.Id}, Train: {place.Run.TrainId}, Departure: {place.Run.Date}";

                _logger.Log(message, LogSeverity.Info);
                return reserv;
            }
            catch (Exception ex)
            {
                var errorMassage = $"Reservation is failed\nPlace: {place.Id}, Train: {place.Run.TrainId}, Departure: {place.Run.Date}\n{ex.Message}";


                _logger.Log(errorMassage, LogSeverity.Error);
                throw ex;
            }
        }
    }
}