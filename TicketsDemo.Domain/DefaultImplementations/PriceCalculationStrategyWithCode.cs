using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Entities.BookingAggregate;
using TicketsDemo.Data.Repositories;
using TicketsDemo.Domain.DTO;
using TicketsDemo.Domain.Interfaces;

namespace TicketsDemo.Domain.DefaultImplementations
{
    public class PriceCalculationStrategyWithCode : IPriceCalculationStrategy
    { 
        private IBookingAgencieRepository _bookingAgencies;
        private IPriceCalculationStrategy _priceCalculationStrategy;

        public PriceCalculationStrategyWithCode(IBookingAgencieRepository bookingAgencieRepository, IPriceCalculationStrategy priceCalculationStrategy)
        {
            _bookingAgencies = bookingAgencieRepository;
            _priceCalculationStrategy = priceCalculationStrategy;
        }

        public List<PriceComponent> CalculatePrice(TicketParametersDTO parametrs)
        {
            var components = new List<PriceComponent>();

            var defaultPrice = _priceCalculationStrategy.CalculatePrice(parametrs);
            PriceComponent mainPrice = defaultPrice.Find(p => p.Name == "Main price");

            if (parametrs.Code != null)
            {
                components.Add(new PriceComponent 
                { 
                    Name = "Code markup price", 
                    Ticket = parametrs.Ticket, 
                    TicketId = parametrs.Ticket.Id, 
                    Value = _bookingAgencies.GetMarkup(parametrs.Code)
                });
                components.Add(new PriceComponent
                {
                    Name = "Price with markup",
                    Ticket = parametrs.Ticket,
                    TicketId = parametrs.Ticket.Id,
                    Value = (decimal)mainPrice.Value + _bookingAgencies.GetMarkup(parametrs.Code)
                });
            }

            return components;
        }     
    }
}
