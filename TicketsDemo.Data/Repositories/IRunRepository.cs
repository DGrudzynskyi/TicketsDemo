using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;

namespace TicketsDemo.Data.Repositories
{
    public interface IRunRepository
    {
        void CreateRun(Run run);
        void DeleteRun(int runId);
        List<Run> GetRuns(DateTime start, DateTime end,int? trainId = null);
        Run GetRunDetails(int runId);
        PlaceInRun GetPlaceInRun(int placeInRunId);
    }
}
