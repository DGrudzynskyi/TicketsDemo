using System;

namespace TicketsDemo.Data.Entities.TicketAggregate
{
    public class Holiday
    {
        public int ID { get; set; }
        public string Name { get; set;}
        public DateTime Date { get; set; }
        public decimal Percent { get; set; }  
    }
}
