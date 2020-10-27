using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsDemo.Data.Entities.BookingAggregate
{
    public class AgencyRepresentative
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public int BookingAgencyId { set; get; }
        public string BookingAgencyCode { set; get; }
        public BookingAgency BookingAgency { set; get; }
    }
}