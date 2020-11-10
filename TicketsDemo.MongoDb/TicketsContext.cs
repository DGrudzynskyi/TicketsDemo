using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;

namespace TicketsDemo.MongoDb
{
    public class TicketsContext
    {
        private IMongoDatabase _database;

        public TicketsContext()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MongoDb"].ConnectionString;
            var connection = new MongoUrlBuilder(connectionString);
            MongoClient client = new MongoClient(connectionString);

            _database = client.GetDatabase(connection.DatabaseName);
        }

        public IMongoCollection<Train> Trains
        {
            get { return _database.GetCollection<Train>("Train"); }
        }
    }
}
