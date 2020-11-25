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
    public class BookingAgencieRepository : IBookingAgencie
    {
        public decimal GetMarkup(string code)
        {
            using (var ctx = new TicketsContext())
            {
                var representatives = ctx.AgencyRepresentatives.FirstOrDefault(r => r.BookingAgenciesCode == code);

                if(representatives == null)
                {
                    return 0;
                }

                var bookingAgencies = ctx.BookingAgencies.FirstOrDefault(b => b.Id == representatives.BookingAgenciesId);
               
                return (decimal)bookingAgencies.Markup;
            }
        }
    }
}
