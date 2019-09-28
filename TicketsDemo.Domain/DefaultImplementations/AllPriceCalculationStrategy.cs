using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Repositories;
using TicketsDemo.Domain.DefaultImplementations.PriceCalculationStrategy;
using TicketsDemo.Domain.Interfaces;

namespace TicketsDemo.Domain.DefaultImplementations
{
   public  class AllPriceCalculationStrategy:IPriceCalculationStrategy
    {
        private DefaultPriceCalculationStrategy defaultStrat;
        private AgencyPriceCalculationStrategy agencyStrat;
        public AllPriceCalculationStrategy(DefaultPriceCalculationStrategy _defaultStrat, AgencyPriceCalculationStrategy _agencyStrat)
        {
            defaultStrat = _defaultStrat;
            agencyStrat = _agencyStrat;
        }
        public List<PriceComponent> CalculatePrice(PlaceInRun placeInRun)
        {
            var setAllComponents = new List<PriceComponent>();

            setAllComponents.AddRange(defaultStrat.CalculatePrice(placeInRun));
            setAllComponents.AddRange(agencyStrat.CalculatePrice(placeInRun));

            return setAllComponents;
        }
    }
}
