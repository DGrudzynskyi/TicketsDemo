using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Repositories;

namespace TicketsDemo.MongoDB.Repositories
{
  public  class mongoDbTrainRepository : ITrainRepository
    {
        public void  CreateTrain(Train train)
        {
            TicketsContext tc = new TicketsContext();
             tc.Trains.InsertOne(train);
        }

        public void DeleteTrain(Train train)
        {
            TicketsContext tc = new TicketsContext();
             tc.Trains.DeleteOne(new BsonDocument("_id", train.Id));
        }

        public List<Train> GetAllTrains()
        {
            TicketsContext tc = new TicketsContext();
            return  tc.Trains.Find(new BsonDocument()).ToList(); 
        }

        public Train GetTrainDetails(int trainId)
        {
            TicketsContext tc = new TicketsContext();
            return  tc.Trains.Find(new BsonDocument("_id", trainId)).FirstOrDefault();
        }

        public void UpdateTrain(Train train)
        {
            TicketsContext tc = new TicketsContext();
            tc.Trains.ReplaceOne(new BsonDocument("_id", train.Id), train);
        }


    }
}
