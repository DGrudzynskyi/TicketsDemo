using CsvHelper.Configuration;
using TicketsDemo.Data.Entities;

namespace TicketsDemo.DTO
{
    public struct TicketPriceParametersDTO
    {
        public PlaceInRun PlaceInRun { get; set; }
        public Ticket Ticket { get; set; }
        public bool Drink { get; set; }
        public bool Bed { get; set; }
    }
}
