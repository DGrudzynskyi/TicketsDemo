using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities.TicketAggregate;

namespace TicketsDemo.Data.Repositories
{
    public interface IHolidayRepository
    {
        List<Holiday> GetHolidays();
        Holiday GetInfoAboutHoliday(int id);
        Holiday GetInfoAboutHoliday(DateTime date);

        void CreateHoliday(Holiday holiday);
        void UpdateHoliday(Holiday holiday);
        void DeleteHoliday(Holiday holiday);
    }
}
