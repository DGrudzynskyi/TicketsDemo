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
    public class TicketService : ITicketService
    {
        private ITicketRepository _tickRepo;
        private IPriceCalculationStrategy _priceStr;
        private IReservationRepository _resRepo;
        private IRunRepository _runRepository;

        public TicketService(ITicketRepository tickRepo, IReservationRepository resRepo,
            IPriceCalculationStrategy priceCalculationStrategy, IRunRepository runRepository)
        {
            _tickRepo = tickRepo;
            _resRepo = resRepo;
            _priceStr = priceCalculationStrategy;
            _runRepository = runRepository;
        }

        public Ticket CreateTicket(int reservationId, string fName, string lName, PriceCalculationParameters parameters)
        {
            var res = _resRepo.Get(reservationId);

            if (res.TicketId != null) {
                throw new InvalidOperationException("ticket has been already issued to this reservation, unable to create another one");
            }

            var placeInRun = _runRepository.GetPlaceInRun(res.PlaceInRunId);

            var newTicket = new Ticket()
            {
                ReservationId = res.Id,
                CreatedDate = DateTime.Now,
                FirstName = fName,
                LastName = lName,
                Status = TicketStatusEnum.Active,
                PriceComponents = new List<PriceComponent>()
            };

            parameters.PlaceInRun = placeInRun;
            newTicket.PriceComponents = _priceStr.CalculatePrice(parameters);

            res.TicketId = newTicket.Id;
            _resRepo.Update(res);

            _tickRepo.Create(newTicket);
            return newTicket;
        }
    }
}
