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
    public class ReservationServiceLoggingDecorator : IReservationService, ILogger
    {
        public IReservationService _decoratedObject;
        public string _logLocation;

       public void Log(string messege, LogSeverity severity)
        {
            using (StreamWriter streamWriter = File.AppendText(_logLocation))
            {
                streamWriter.WriteLine(severity+ "  "+ messege);
                
            }
        }
        public ReservationServiceLoggingDecorator (ReservationService decoratedObject)
        {
            _decoratedObject = decoratedObject;
            _logLocation = "D:\\1\\3 курс\\Proga\\TicketsDemo\\log.txt";
        }

        public Reservation Reserve(PlaceInRun place)
        { 
            try
            {  var reservation = _decoratedObject.Reserve(place);
                string line = "Action time " + DateTime.Now
                             + " : Successful place reservation: runDate:" + place.Run.Date
                             + ", place:" + place.Number
                             + ", carriage:" + place.CarriageNumber
                             + ", runId:" + place.Run.TrainId;
                Log(line, LogSeverity.Info);
                    return reservation;
            }
            catch (Exception e)
            {
                string line = "Action time " + DateTime.Now + ":"
                            + " Not successful place reservation: runDate:"+place.Run.Date
                            +" place: " + place.Number 
                            + ", carriage:"+place.CarriageNumber
                            +", runId:"+ place.Run.TrainId ;
                Log(line, LogSeverity.Error);
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
                string line = "Action time"+ DateTime.Now
                            +":  Successful remove place reservation: resrvationId:"+reservation.Id
                            +", placeInRun:"+reservation.PlaceInRunId
                            +", resrvationStart:"+reservation.Start;
                    Log(line, LogSeverity.Info);
            }
                catch (Exception e)
                {
                string line = "Action time"+DateTime.Now
                            +":  Unsuccessful remove place reservation: resrvationId:"+ reservation.Id
                            +", placeInRun:"+reservation.PlaceInRunId
                            +", resrvationStart:"+reservation.Start;
                Log(line, LogSeverity.Error);
                throw e;
                }
            
        }
              
    }
}
