using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsDemo.Data.Entities.BookingAggregate
{
    public class BookingAgency
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public decimal Markup { set; get; }
    }
}