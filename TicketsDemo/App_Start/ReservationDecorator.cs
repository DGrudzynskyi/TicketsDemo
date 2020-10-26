
using System.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Repositories;
using TicketsDemo.Domain.Interfaces;
using TicketsDemo.Domain.DefaultImplementations;
using System.Diagnostics;

    
    namespace TicketsDemo.Domain.NewImplementations
    {
        public class NewReservationService : IReservationService
        {

            protected IReservationRepository _resRepo;
            protected ILogger _resRepoLogger;

            public NewReservationService(IReservationRepository resRepo, ILogger logger)
            {
                _resRepo = resRepo;
                _resRepoLogger = logger;
                _resRepoLogger.Log("NewReservationService Created", 0);
                Debug.WriteLine("NewReservationService Created");

            }

            public Reservation Reserve(PlaceInRun place)
            {
                if (PlaceIsOccupied(place))
                {
                    throw new InvalidOperationException(String.Format("place {0} can't be reserved becouse it is currently occupied", place.Id));

                }
                var createIt = new Reservation()
                {
                    Start = DateTime.Now,
                    End = DateTime.Now.AddMinutes(20),
                    PlaceInRunId = place.Id,
                };

                _resRepo.Create(createIt);
                _resRepoLogger.Log("NewReservation created", 0);
                Debug.WriteLine("NewReservation created");
                return createIt;
            }

            public void RemoveReservation(Reservation reservation)
            {
                reservation.End = DateTime.Now;
                _resRepo.Update(reservation);
                _resRepoLogger.Log($"Reservation with ID: {reservation.Id} Removed", 0);
                Debug.WriteLine($"Reservation with ID: {reservation.Id} Removed");
            }

            public bool IsActive(Reservation reservation)
            {
                return reservation.TicketId.HasValue || reservation.Start < DateTime.Now && reservation.End > DateTime.Now;
            }

            public bool PlaceIsOccupied(PlaceInRun place)
            {
                var reservationsForCurrentPlace = _resRepo.GetAllForPlaceInRun(place.Id);
                if (reservationsForCurrentPlace == null)
                    return false;

                var activeReservationFound = false;
                foreach (var res in reservationsForCurrentPlace)
                {
                    if (IsActive(res))
                    {
                        activeReservationFound = true;
                    }
                }

                return activeReservationFound;
            }

        }
    }