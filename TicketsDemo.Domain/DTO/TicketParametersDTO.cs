using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;

namespace TicketsDemo.Domain.DTO
{
    public class TicketParametersDTO
    {
        public PlaceInRun PlaceInRun { get; set; }

        public Ticket Ticket { get; set; }

        public string Code { get; set; }
    }
}
