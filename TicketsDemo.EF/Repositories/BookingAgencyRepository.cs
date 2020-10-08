using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Repositories;

namespace TicketsDemo.EF.Repositories
{
    public class BookingAgencyRepository : IBookingAgencyRepository
    {
        public void Create(BookingAgency bookingAgency)
        {
            using (var ctx = new TicketsContext())
            {
                ctx.BookingAgencies.Add(bookingAgency);
                ctx.SaveChanges();
            }
        }

        public void Update(BookingAgency bookingAgency)
        {
            using (var ctx = new TicketsContext())
            {
                ctx.BookingAgencies.Attach(bookingAgency);
                ctx.Entry(bookingAgency).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        public BookingAgency Get(int id)
        {
            using (var ctx = new TicketsContext())
            {
                return ctx.BookingAgencies.FirstOrDefault(p => p.Id == id);
            }
        }

        public List<BookingAgency> GetAll()
        {
            using (var ctx = new TicketsContext())
            {
                return ctx.BookingAgencies.ToList();
            }
        }
    }
}
