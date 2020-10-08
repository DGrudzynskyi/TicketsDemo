﻿using System;
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
            using (var ctx = new TicketsContext())
            {
                return ctx.BookingServices.FirstOrDefault(p => p.Id == id);
            }
        }

        public List<BookingService> GetAll()
        {
            using (var ctx = new TicketsContext())
            {
                return ctx.BookingServices.ToList();
            }
        }

        public List<BookingService> GetAllForBookingAgency(int bookingAgencyId)
        {
            using (var ctx = new TicketsContext())
            {
                return ctx.BookingServices.Where(p => p.BookingAgencyId == bookingAgencyId).ToList();
            }
        }
    }
}
