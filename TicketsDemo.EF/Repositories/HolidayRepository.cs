using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Repositories;
using TicketsDemo.Data.Entities;
namespace TicketsDemo.EF.Repositories
{
    public class HolidayRepository : IHolidayRepository
    {
        public List<Holiday> GetAllHolidays()
        {
            using (var ctx = new TicketsContext())
            {
                return ctx.Holidays.ToList();
            }
        }

        public Holiday GetHolidayDetails(String id)
        {
            using (var ctx = new TicketsContext())
            {
                return ctx.Holidays.First(x => x.Id == id);
            }
        }
        public Holiday GetHolidayDetails(DateTime date)
        {
            using (var ctx = new TicketsContext())
            {
                return ctx.Holidays.Where(x => x.Date == date.Date).FirstOrDefault();
            }
        }
        public void CreateHoliday(Holiday holiday)
        {
            using (var ctx = new TicketsContext())
            {
                ctx.Holidays.Add(holiday);
                ctx.SaveChanges();
            }
        }
        public void DeleteHoliday(Holiday holiday)
        {
            using (var ctx = new TicketsContext())
            {
                ctx.Holidays.Remove(holiday);
                ctx.SaveChanges();
            }
        }

    }
}
