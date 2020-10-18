using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Configuration;
using TicketsDemo.Data.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace TicketsDemo.Mongo
{
    public class TicketsContext
    {
        private IMongoDatabase database;
        public TicketsContext()
        {
            //string connectionString = ConfigurationManager.ConnectionStrings["MongoDb"].ConnectionString;
            string connectionString = "mongodb://localhost:27017/TicketsDemo";
            MongoClient client = new MongoClient(connectionString);
            var connectionDB = new MongoUrlBuilder(connectionString);
            database = client.GetDatabase(connectionDB.DatabaseName);
        }

        public  IMongoCollection<Train> Trains 
        { 
            get { return database.GetCollection<Train>("Train"); }
        }
        public IMongoCollection<Carriage> Carriages 
        {
            get { return database.GetCollection<Carriage>("Carriage"); } 
        }
        public IMongoCollection<Place> Places 
        {
            get { return database.GetCollection<Place>("Place"); }
        }

    }
}