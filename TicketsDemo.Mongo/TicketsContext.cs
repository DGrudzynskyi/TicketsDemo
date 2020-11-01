using MongoDB.Driver;
using System.Configuration;
using TicketsDemo.Data.Entities;

namespace TicketsDemo.Mongo
{
    class TicketsContext
    {
        private IMongoDatabase database;
        public TicketsContext()
        {
            // строка подключения
            string connectionString = ConfigurationManager.ConnectionStrings["MongoDb"].ConnectionString;
            var connection = new MongoUrlBuilder(connectionString);/*.DatabaseName*/
            // получаем клиента для взаимодействия с базой данных
            MongoClient client = new MongoClient(connectionString);
            // получаем доступ к самой базе данных
            database = client.GetDatabase(connection.DatabaseName);
        }
        public IMongoCollection<Train> Trains
        {
            get { return database.GetCollection<Train>("Train"); }
        }
    }
}
