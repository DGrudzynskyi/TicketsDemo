﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsDemo.Data.Entities
{
    public class BookingService
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BookingAgencyId { get; set; }
        public BookingAgency HostAgency { get; set; }
    }
}
