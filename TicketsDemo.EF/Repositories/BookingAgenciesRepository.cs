using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Entities.BookingAggregate;
using TicketsDemo.Data.Repositories;

namespace TicketsDemo.EF.Repositories
{ 
    public class BookingAgenciesRepository : IBookingAgencies
    {
        public decimal GetMarkup(string code)
        {
            using (var ctx = new TicketsContext())
            {
                var representatives = ctx.AgencyRepresentative.FirstOrDefault(r => r.BookingAgenciesCode == code);
                var bookingAgencies = ctx.BookingAgencie.FirstOrDefault(b => b.Id == representatives.BookingAgenciesId);
              
                /*var representatives = ctx.AgencyRepresentative
                    .Include("AgencyRepresentative").Where(r => r.BookingAgenciesCode == code).Single();
                var bookingAgencies = ctx.BookingAgencie
                    .Include("BookingAgencie").Where(b => b.Id == representatives.BookingAgenciesId).Single();*/

                return (decimal)bookingAgencies.Markup;
            }
        }
        public List<BookingAgencies> GetAllAgencies()
        {
            using (var ctx = new TicketsContext())
            {
                return ctx.BookingAgencie.ToList();
            }
        }

        public AgencyRepresentatives GetAgency(string code)
        {
            using (var ctx = new TicketsContext())
            {
                return ctx.AgencyRepresentative.FirstOrDefault(r => r.BookingAgenciesCode == code);
            }
        }

        public BookingAgencies Get(int id)
        {
            using (var ctx = new TicketsContext())
            {
                return ctx.BookingAgencie
                    .Include("AgencyRepresentatives").Where(r => r.Id == id).Single();
            }
        }
    }
}
