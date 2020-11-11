using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Repositories;

namespace TicketsDemo.EF.Repositories
{
    public class TrainRepository : ITrainRepository
    {
        #region ITrainRepository Members

        public List<Train> GetAllTrains()
        {
            using (var ctx = new TicketsContext()) {
                return ctx.Trains.Include(x => x.Carriages.Select(c => c.Places)).ToList();    
            }
        }

        public Data.Entities.Train GetTrainDetails(int id)
        {
            using (var ctx = new TicketsContext())
            {
                return ctx.Trains.Include(x => x.Carriages.Select(c => c.Places)).First(x => x.Id == id);
            }
        }

        public void CreateTrain(Data.Entities.Train train)
        {
            using (var ctx = new TicketsContext())
            {
                ctx.Trains.Add(train);
                ctx.SaveChanges();
            }
        }

        public void UpdateTrain(Data.Entities.Train train)
        {
            using (var ctx = new TicketsContext())
            {
                ctx.Trains.Attach(train);
                ctx.Entry(train).State = EntityState.Modified; 
                ctx.SaveChanges();
            }
        }

        public void DeleteTrain(Data.Entities.Train train)
        {
            using (var ctx = new TicketsContext())
            {
                ctx.Trains.Remove(train);
                ctx.SaveChanges();
            }
        }
        
        #endregion
    }
}
