using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Repositories;
using TicketsDemo.Domain.Interfaces;

namespace TicketsDemo.Domain.DefaultImplementations
{
    public class Schedule : ISchedule
    {
        public IRunRepository _runRepo;
        public ITrainRepository _trainRepo;
        public IReservationRepository _reservationRepository;
        public IReservationService _resService;

        public Schedule(
            IRunRepository runRepo, 
            ITrainRepository trainRepo, 
            IReservationRepository reservationRepository, 
            IReservationService resService)
        {
            _runRepo = runRepo;
            _trainRepo = trainRepo;
            _reservationRepository = reservationRepository;
            _resService = resService;
        }

        public List<DateTime> GetAvailableDatesForNewRun(int train, DateTime start, DateTime end) {
            var allRuns = _runRepo.GetRuns(start, end).Where(r => r.TrainId == train);

            var retIt = new List<DateTime>();
            for (var tt = start; tt < end; tt = tt.AddDays(1)) {
                if (!allRuns.Any(r => r.Date.Date == tt.Date)) {
                    retIt.Add(tt);
                }
            }

            return retIt;
        }


        public Run CreateRun(int trainId, DateTime date) {
            if (GetAvailableDatesForNewRun(trainId, date, date.AddDays(1)).Count < 1)
            {
                throw new InvalidOperationException(String.Format("Train {0} is occupied at {1}. Run can not be created",trainId,date));
            }

            var train = _trainRepo.GetTrainDetails(trainId);

            var run = new Run() { TrainId = train.Id, Date = date, Places = new List<PlaceInRun>() };
            

            foreach (var carriage in train.Carriages) {
                foreach (var place in carriage.Places) {
                    var newPlaceInRun = new PlaceInRun()
                    {
                        Number = place.Number,
                        CarriageNumber = carriage.Number,
                        Run = run
                    };
                    run.Places.Add(newPlaceInRun);
                };
            };

            _runRepo.CreateRun(run);

            return run;
        }


        public void DeleteRun(int runId) {
            var allReservations = _reservationRepository.GetAllForRun(runId);

            if (allReservations.Any(p => _resService.IsActive(p)))
            {
                throw new InvalidOperationException(String.Format("run {0} can not be deleted becouse some of it's places has been reserved", runId));
            }

            _runRepo.DeleteRun(runId);
        }
    }
}
