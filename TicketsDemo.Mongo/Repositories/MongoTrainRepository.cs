using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Repositories;

namespace TicketsDemo.Mongo
{
    public class MongoTrainRepository : ITrainRepository
    {

        public List<Train> GetAllTrains()
        {
            var ctx = new TicketsContext();
            var train = ctx.Trains.Find(new BsonDocument()).ToList();
            return train;
        }

        public Train GetTrainDetails(int id)
        {
            var ctx = new TicketsContext();
            var train = ctx.Trains.Find(new BsonDocument("_id", id)).FirstOrDefaultAsync().Result;
            foreach (var carriage in train.Carriages)
            {
                foreach (var place in carriage.Places)
                {
                    place.Carriage = carriage;
                }
                carriage.Train = train;
            }
            return train;
        }

        public void CreateTrain(Data.Entities.Train train)
        {
            var ctx = new TicketsContext();
            ctx.Trains.InsertOneAsync(train);
        }

        public void UpdateTrain(Data.Entities.Train train)
        {
            var ctx = new TicketsContext();
            ctx.Trains.ReplaceOne(new BsonDocument("_id", train.Id), train);
        }

        public void DeleteTrain(Data.Entities.Train train)
        {
            var ctx = new TicketsContext();
            ctx.Trains.DeleteOne(new BsonDocument("_id", train.Id));
        }
    }
}
