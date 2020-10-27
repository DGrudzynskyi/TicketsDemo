using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Repositories;

namespace TicketsDemo.EF.Repositories
{
    public class RunRepository : IRunRepository
    {
        ITrainRepository _trainRepo;

        public RunRepository(ITrainRepository trainRepo)
        {
            _trainRepo = trainRepo;
        }

        #region IRunRepository Members

        public void CreateRun(Data.Entities.Run run)
        {
            using (var ctx = new TicketsContext())
            {
                ctx.Runs.Add(run);
                ctx.SaveChanges();
            }
        }

        public void DeleteRun(int runId)
        {
            using (var ctx = new TicketsContext())
            {
                var run = ctx.Runs.Find(runId);
                ctx.Runs.Remove(run);
                ctx.SaveChanges();
            }
        }

        public List<Data.Entities.Run> GetRuns(DateTime start, DateTime end, int? traintId = null)
        {
            using (var ctx = new TicketsContext())
            {
                return ctx.Runs.Where(r => r.Date > start && r.Date < end && (traintId == null || r.TrainId == traintId)).ToList();
            }
        }

        public Data.Entities.Run GetRunDetails(int runId)
        {
            using (var ctx = new TicketsContext())
            {
                return ctx.Runs.Include("Places")
                    .Where(r => r.Id == runId).Single();
            }
        }
        public PlaceInRun GetPlaceInRun(int placeInRunId) {
            using (var ctx = new TicketsContext())
            {
                return ctx.PlacesInRuns
                    .Include(x => x.Run)
                    .Include(x => x.Run.Places).Single(x => x.Id == placeInRunId);
            }
        }

        #endregion
    }
}
