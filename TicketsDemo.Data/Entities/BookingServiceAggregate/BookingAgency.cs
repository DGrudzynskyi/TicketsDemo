using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsDemo.Data.Entities
{
    public class BookingAgency
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double FareCoef { get; set; }
        public List<BookingService> BookingServices { get; set; }
    }
}
