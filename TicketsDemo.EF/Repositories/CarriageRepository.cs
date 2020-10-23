using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Repositories;

namespace TicketsDemo.EF.Repositories
{
    public class CarriageRepository : ICarriageRepository
    {
        public Carriage GetCarriage(int number)
        {
            using (var ctx = new TicketsContext())
            {
                return ctx.Carriages.FirstOrDefault(x=>x.Number == number);
            }
        }
    }
}
