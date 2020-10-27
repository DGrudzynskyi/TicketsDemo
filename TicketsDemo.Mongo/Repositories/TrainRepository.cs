using System;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Repositories;
using MongoDB.Driver.Linq;
using System.Linq;

namespace TicketsDemo.Mongo.Repositories
{
    public class MongoTrainRepository : ITrainRepository 
    { 

        #region ITrainRepository Members

        public List<Train> GetAllTrains()
        {

            var ctx = new TicketsContext();
            var train = ctx.Trains.Find(new BsonDocument()).ToList();
            return train;
        }

        public Data.Entities.Train GetTrainDetails(int id)
        {
            var ctx = new TicketsContext();
            var train = ctx.Trains.Find(new BsonDocument("_id", id)).FirstOrDefaultAsync().Result;
            foreach (var car in train.Carriages)
            {
                foreach (var pl in car.Places)
                {
                    pl.Carriage = car;
                }
                car.Train = train;

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

        #endregion
    }
}
