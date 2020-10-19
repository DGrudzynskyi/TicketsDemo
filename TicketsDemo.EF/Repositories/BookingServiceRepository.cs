using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Repositories;

namespace TicketsDemo.EF.Repositories
{
    public class BookingServiceRepository : IBookingServiceRepository
    {
        private IBookingAgencyRepository _bookingAgencyRepo;

        public BookingServiceRepository(IBookingAgencyRepository bookingAgencyRepo)
        {
            _bookingAgencyRepo = bookingAgencyRepo;
        }

        public void Create(BookingService bookingService)
        {
            using (var ctx = new TicketsContext())
            {
                ctx.BookingServices.Add(bookingService);
                ctx.SaveChanges();
            }
        }

        public void Update(BookingService bookingService)
        {
            using (var ctx = new TicketsContext())
            {
                ctx.BookingServices.Attach(bookingService);
                ctx.Entry(bookingService).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        public BookingService Get(int id)
        {
            BookingService bookingService;
            using (var ctx = new TicketsContext())
            {
                bookingService =  ctx.BookingServices.FirstOrDefault(p => p.Id == id);
            }
            return InjectBookingAgency(bookingService);
        }

        public List<BookingService> GetAll()
        {
            List<BookingService> bookingServices;
            using (var ctx = new TicketsContext())
            {
                bookingServices = ctx.BookingServices.ToList();
            }
            foreach(BookingService bookingService in bookingServices)
            {
                InjectBookingAgency(bookingService);
            }
            return bookingServices;
        }

        public List<BookingService> GetAllForBookingAgency(int bookingAgencyId)
        {
            List<BookingService> bookingServices;
            using (var ctx = new TicketsContext())
            {
                bookingServices = ctx.BookingServices.Where(p => p.BookingAgencyId == bookingAgencyId).ToList();
            }
            foreach (BookingService bookingService in bookingServices)
            {
                InjectBookingAgency(bookingService);
            }
            return bookingServices;
        }

        private BookingService InjectBookingAgency(BookingService bookingService)
        {
            var bookingAgency = _bookingAgencyRepo.Get(bookingService.BookingAgencyId);
            bookingService.HostAgency = bookingAgency;
            return bookingService;
        }
    }
}
