using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Domain.DTO;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Repositories;
using TicketsDemo.Domain.Interfaces;
using TicketsDemo.Domain.DefaultImplementations.PriceCalculationStrategy;

namespace TicketsDemo.Domain.DefaultImplementations
{
    public class BookingPriceCalculationStrategy : IPriceCalculationStrategy
    {
        private IPriceCalculationStrategy _priceCalculationStrategy;
        private IRepresentativeRepository _representativeRepository;

        public BookingPriceCalculationStrategy(
            IPriceCalculationStrategy priceCalculationStrategy,
            IRepresentativeRepository representativeRepository)
        {
            _priceCalculationStrategy = priceCalculationStrategy;
            _representativeRepository = representativeRepository;
        }

        public List<PriceComponent> CalculatePrice(TicketParametersDTO parameters)
        {
            var components = new List<PriceComponent>();

            var agency = _representativeRepository.GetRepresentative(parameters.agencyCode);
            var defaultPrice = _priceCalculationStrategy.CalculatePrice(parameters);
            PriceComponent mainPrice = defaultPrice.Find(p => p.Name == "Main price");

            if (agency != null)
            {
                var bookingAgencyComponent = new PriceComponent()
                {
                    Name = String.Format("{0} markup", agency.BookingAgency.Name),
                    Value = mainPrice.Value * agency.BookingAgency.Markup
                };
                components.Add(bookingAgencyComponent);
            }

            return components;
        }
    }
}
