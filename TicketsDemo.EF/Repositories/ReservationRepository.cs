using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Repositories;

namespace TicketsDemo.EF.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        public List<Reservation> GetAllForRun(int runId)
        {
            using (var ctx = new TicketsContext())
            {
                var placesInRunIds = ctx.Runs.SelectMany(x => x.Places.Select(p => p.Id));
                return ctx.Reservations.Where(x => placesInRunIds.Contains(x.PlaceInRunId)).ToList();
            }
        }

        public List<Reservation> GetAllForPlaceInRun(int placeInRunId)
        {
            using (var ctx = new TicketsContext())
            {
                return ctx.Reservations.Where(x => x.PlaceInRunId == placeInRunId).ToList();
            }
        }
        public void Create(Reservation reservation)
        {
            using (var ctx = new TicketsContext())
            {
                ctx.Reservations.Add(reservation);
                ctx.SaveChanges();
            }
        }

        public void Update(Reservation reservation)
        {
            using (var ctx = new TicketsContext())
            {
                ctx.Reservations.Attach(reservation);
                ctx.Entry(reservation).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        public Reservation Get(int id)
        {
            using (var ctx = new TicketsContext())
            {
                return ctx.Reservations.FirstOrDefault(p => p.Id == id);
            }
        }
    }
}
