using System;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Repositories;

namespace TicketsDemo.Mongo.Repositories
{
    public class MongoTrainRepository : ITrainRepository 
    { 

        #region ITrainRepository Members

        public List<Train> GetAllTrains()
        {

            var ctx = new TicketsContext();
            var train = ctx.Trains.Find(new BsonDocument()).ToList();
            foreach (var doc in train)
            {
                doc.Carriages = new List<Carriage>();
            }
            return train;
        }

        public Data.Entities.Train GetTrainDetails(int id)
        {
            var ctx = new TicketsContext();

            var train = ctx.Trains.Find(new BsonDocument("_id", id)).FirstOrDefaultAsync().Result;
            train.Carriages = new List<Carriage>();

            var carriage = ctx.Carriages.Find(new BsonDocument("TrainId", id)).ToList();
            foreach (var new_carriage in carriage)
            {
                new_carriage.Places = new List<Place>();
                new_carriage.Train = train;
                var place = ctx.Places.Find(new BsonDocument("CarriageId", new_carriage.Id)).ToList();
                foreach(var new_place in place)
                {
                    new_place.Carriage = new_carriage;
                    new_carriage.Places.Add(new_place);
                }
                train.Carriages.Add(new_carriage);
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
