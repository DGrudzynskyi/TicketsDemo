using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using TicketsDemo.Data;
using TicketsDemo.Data.Repositories;
using TicketsDemo.Data.Entities;


namespace TicketsDemo.Mongo
{
    public class MongoTrainRepository: ITrainRepository
    {
        public List<Train> GetAllTrains()
        {
            var ctx = new TContext();
            var trains = ctx.Trains.Find(new BsonDocument()).ToList();
            foreach (var train in trains)
            {
                Init_Train(train);
            }
            return trains;
        }

        public Train GetTrainDetails(int id)
        {
            var ctx = new TContext();
            var train = ctx.Trains.Find(new BsonDocument("_id", id)).FirstOrDefaultAsync().Result;
            Init_Train(train);
            return train;
        }

        public void CreateTrain(Data.Entities.Train train)
        {
            var ctx = new TContext();
            ctx.Trains.InsertOneAsync(train);
        }

        public void UpdateTrain(Data.Entities.Train train)
        {
            var ctx = new TContext();
            ctx.Trains.ReplaceOne(new BsonDocument("_id", train.Id), train);
        }

        public void DeleteTrain(Data.Entities.Train train)
        {
            var ctx = new TContext();
            ctx.Trains.DeleteOne(new BsonDocument("_id", train.Id));
        }
        
        private Train Init_Train(Train train)
        {
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

    }
}
