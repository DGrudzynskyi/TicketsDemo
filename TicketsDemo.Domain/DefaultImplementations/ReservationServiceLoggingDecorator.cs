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
    public class ReservationServiceLoggingDecorator : IReservationService
    {
        public IReservationService _decoratedObject;
        public string _logLocation;
        public ReservationServiceLoggingDecorator (ReservationService decoratedObject)
        {
            _decoratedObject = decoratedObject;
            _logLocation = AppDomain.CurrentDomain.BaseDirectory + "log.txt";
        }

        public Reservation Reserve(PlaceInRun place)
        { using (StreamWriter streamWriter = File.AppendText(_logLocation))
            try
            {  var reservation = _decoratedObject.Reserve(place);
                streamWriter.WriteLine("Action time {4}: Successful place reservation: runDate:{0}, place:{1}, carriage:{2}, runId:{3}, reservationDate:{4} ", 
                                    place.Run.Date, place.Number, place.CarriageNumber, place.Run.TrainId, DateTime.Now);
                    return reservation;
            }
            catch (Exception e)
            {
               streamWriter.WriteLine("Action time {0}:ERROR: Not successful place reservation: runDate:{1}, place:{2}, carriage:{3}, runId:{4} ",
                                      DateTime.Now,place.Run.Date, place.Number, place.CarriageNumber, place.Run.TrainId);
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
            using (StreamWriter streamWriter = File.AppendText(_logLocation))
            {   try
                {
                    _decoratedObject.RemoveReservation(reservation);
                    streamWriter.WriteLine("Action time {0}:  Successful remove place reservation: resrvationId:{1}, placeInRun:{2}, resrvationStart:{3}",
                                            DateTime.Now, reservation.Id, reservation.PlaceInRunId, reservation.Start);
                }
                catch (Exception e)
                {
                    streamWriter.WriteLine("Action time {0} ERROR:  Unsuccessful remove place reservation: resrvationId:{1}, placeInRun:{2}, resrvationStart:{3}",
                                              DateTime.Now, reservation.Id, reservation.PlaceInRunId, reservation.Start);
                    throw e;
                }
            }
        }
              
    }
}
