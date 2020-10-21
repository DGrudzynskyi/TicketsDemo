using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities.BookingAggregate;

namespace TicketsDemo.Data.Repositories
{
    public interface IBookingAgencies
    {
        decimal GetMarkup(string code);

    }
}
