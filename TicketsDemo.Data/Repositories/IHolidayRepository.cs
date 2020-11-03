﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;

namespace TicketsDemo.Data.Repositories
{
    public interface IHolidayRepository
    {
        List<Holiday> GetAllHolidays();
        Holiday GetHolidayDetails(string id);
        Holiday GetHolidayDetails(DateTime date);
        void CreateHoliday(Holiday holiday);
        void DeleteHoliday(Holiday holiday);
    }
}