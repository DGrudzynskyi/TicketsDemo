using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsDemo.Data.Entities.BookingAggregate
{
    public class BookingAgencies
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public float Markup { set; get; }
        public List<AgencyRepresentatives> AgencyRepresentatives { set; get; }

    }
}
