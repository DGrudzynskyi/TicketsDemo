using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Repositories;

namespace TicketsDemo.EF.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        public TicketRepository()
        {
        }

        public void Create(Data.Entities.Ticket ticket)
        {
            using (var ctx = new TicketsContext())
            {
                ctx.Tickets.Add(ticket);

                ctx.SaveChanges();
            }
        }

        public void Update(Data.Entities.Ticket ticket)
        {
            using (var ctx = new TicketsContext())
            {
                ctx.Tickets.Attach(ticket);
                ctx.Entry(ticket).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        public Ticket Get(int id) {
            using (var ctx = new TicketsContext())
            {
                return ctx.Tickets
                    .Include("PriceComponents").Where(r => r.Id == id).Single();
            }
        }
    }
}
