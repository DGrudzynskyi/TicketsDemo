using CsvHelper.Configuration;
using TicketsDemo.Data.Entities;

namespace TicketsDemo.DTO
{
    public class TrainDto:ClassMap<Train>
    {
        public TrainDto()
        {
            Map(x => x.Id).Name("Id");
            Map(x => x.Number).Name("Number");
            Map(x => x.StartLocation).Name("StartLocation");
            Map(x => x.EndLocation).Name("EndLocation");
        }
    }
}
