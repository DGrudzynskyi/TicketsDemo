using System.Configuration;
using MongoDB.Driver;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Entities.HolidayAggrigate;

namespace TicketsDemo.Mongo
{
    public class TContext
    {
        private IMongoDatabase MongoDB;

        public TContext()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MongoDb"].ConnectionString;
            var connection = new MongoUrlBuilder(connectionString);
            MongoClient client = new MongoClient(connectionString);
            MongoDB = client.GetDatabase(connection.DatabaseName);
        }

        public IMongoCollection<Train> Trains
        {
            get { return MongoDB.GetCollection<Train>("Train"); }
        }

        public IMongoCollection<Holiday> Holidays
        {
            get { return MongoDB.GetCollection<Holiday>("Holiday"); }
        }
    }
}
