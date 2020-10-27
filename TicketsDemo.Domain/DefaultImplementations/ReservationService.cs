using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Repositories;
using TicketsDemo.Domain.Interfaces;

namespace TicketsDemo.Domain.DefaultImplementations
{
    public class ReservationService : IReservationService
    {
        IReservationRepository _resRepo;

        public ReservationService(IReservationRepository resRepo)
        {
            _resRepo = resRepo;
        }


        public Reservation Reserve(PlaceInRun place)
        {
            if (PlaceIsOccupied(place))
                throw new InvalidOperationException(String.Format("place {0} can't be reserved becouse it is currently occupied", place.Id));

            var createIt = new Reservation()
            {
                Start = DateTime.Now,
                End = DateTime.Now.AddMinutes(20),
                PlaceInRunId = place.Id,
            };

            _resRepo.Create(createIt);

            return createIt;
        }

        public void RemoveReservation(Reservation reservation)
        {
            reservation.End = DateTime.Now;
            _resRepo.Update(reservation);
        }

        public bool IsActive(Reservation reservation) {
            return reservation.TicketId.HasValue || reservation.Start < DateTime.Now && reservation.End > DateTime.Now;
        }

        public bool PlaceIsOccupied(PlaceInRun place)
        {
            var reservationsForCurrentPlace = _resRepo.GetAllForPlaceInRun(place.Id);
            if (reservationsForCurrentPlace == null)
                return false;

            var activeReservationFound = false;
            foreach(var res in reservationsForCurrentPlace)
            {
                if(IsActive(res))
                {
                    activeReservationFound = true;
                }
            }

            return activeReservationFound;
        }
    }
}
