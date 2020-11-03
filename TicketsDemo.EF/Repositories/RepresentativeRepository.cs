using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Entities.BookingAggregate;
using TicketsDemo.Data.Repositories;

namespace TicketsDemo.EF.Repositories
{
    public class RepresentativeRepository : IRepresentativeRepository
    {
        public AgencyRepresentative GetRepresentative(string code)
        {
            using (var ctx = new TicketsContext())
            {
                return ctx.AgencyRepresentatives.Include(b => b.BookingAgency).Where(x => x.BookingAgencyCode == code).FirstOrDefault();
            }
        }

        public void CreateRepresentative(AgencyRepresentative agencyRepresentative)
        {
            using (var ctx = new TicketsContext())
            {
                ctx.AgencyRepresentatives.Add(agencyRepresentative);
                ctx.SaveChanges();
            }
        }

        public void DeleteRepresentative(AgencyRepresentative agencyRepresentative)
        {
            using (var ctx = new TicketsContext())
            {
                ctx.AgencyRepresentatives.Remove(agencyRepresentative);
                ctx.SaveChanges();
            }
        }

        public void UpdateRepresentative(AgencyRepresentative agencyRepresentative)
        {
            using (var ctx = new TicketsContext())
            {
                ctx.AgencyRepresentatives.Attach(agencyRepresentative);
                ctx.Entry(agencyRepresentative).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }
    }
}