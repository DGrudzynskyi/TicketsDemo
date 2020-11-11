using System;
using System.Collections.Generic;
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
