using System;

namespace TicketsDemo.Data.Entities.HolidayAggregate
{
    public class Holiday
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public decimal Markup { get; set; }
    }
}
