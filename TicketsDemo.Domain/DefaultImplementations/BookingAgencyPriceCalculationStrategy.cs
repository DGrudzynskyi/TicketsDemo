using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Repositories;
using TicketsDemo.Domain.Interfaces;

namespace TicketsDemo.Domain.DefaultImplementations.PriceCalculationStrategy
{
    public class BookingAgencyPriceCalculationStrategy : IPriceCalculationStrategy
    {     
        private IAgentRepository _agentRepository;
        private IPriceCalculationStrategy _priceCalculationStrategy;
        public BookingAgencyPriceCalculationStrategy(IPriceCalculationStrategy priceCalculationStrategy , IAgentRepository agentRepository)
        {
            _agentRepository = agentRepository;
            _priceCalculationStrategy = priceCalculationStrategy;
        }
        public List<PriceComponent> CalculatePrice(PriceCalculationParameters parameters)
        {
            var components = new List<PriceComponent>();
            Agent agent = _agentRepository.GetAgent(parameters.agentId);
            if (agent != null)
            {
                var priceComponents = _priceCalculationStrategy.CalculatePrice(parameters);
                var agentPercent = (decimal) agent.Percent;
                PriceComponent mainComponent = priceComponents.Find(p => p.Name == "Main price");
                var bookingCompanyComponent = new PriceComponent()
                {
                    Name = "Booking agent services",
                    Value =mainComponent.Value* agentPercent
                };
                components.Add(bookingCompanyComponent);
            }            
            return components;
        }
    }
}
