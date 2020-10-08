using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;

namespace TicketsDemo.Data.Repositories
{
    public interface IBookingAgencyRepository
    {
        void Create(BookingAgency bookingAgency);
        void Update(BookingAgency bookingAgency);
        BookingAgency Get(int id);
        List<BookingAgency> GetAll();
    }
}
