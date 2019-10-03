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
        private DefaultPriceCalculationStrategy _defaultStrat;
        private AgencyPriceCalculationStrategy _agencyStrat;

        private IList<IPriceCalculationStrategy> _priceStrategies;
        public AllPriceCalculationStrategy(IList<IPriceCalculationStrategy> priceStrategies)
        {
            _priceStrategies = priceStrategies;
        }
        public List<PriceComponent> CalculatePrice(PlaceInRun placeInRun)
        {
            var setAllComponents = new List<PriceComponent>();

            foreach(IPriceCalculationStrategy strat in _priceStrategies)
            {
                setAllComponents.AddRange(strat.CalculatePrice(placeInRun));
            }

            return setAllComponents;
        }
    }
}
