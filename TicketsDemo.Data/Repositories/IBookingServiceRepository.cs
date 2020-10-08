using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;

namespace TicketsDemo.Data.Repositories
{
    public interface IBookingServiceRepository
    {
        void Create(BookingService bookingService);
        void Update(BookingService bookingService);
        BookingService Get(int id);
        List<BookingService> GetAll();
        List<BookingService> GetAllForBookingAgency(int bookingAgencyId);
    }
}
