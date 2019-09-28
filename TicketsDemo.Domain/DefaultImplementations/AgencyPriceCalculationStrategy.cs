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
    class AgencyPriceCalculationStrategy : IPriceCalculationStrategy
    {
      
        private DefaultPriceCalculationStrategy defaultPriceStrat;
        private int AgencyId { get; set; }
        public AgencyPriceCalculationStrategy(DefaultPriceCalculationStrategy defaultPrice,int agId)
        {
            
            defaultPriceStrat = defaultPrice;
            AgencyId = agId;
        }
        public List<PriceComponent> CalculatePrice(PlaceInRun placeInRun)
        {
            var components = defaultPriceStrat.CalculatePrice(placeInRun);

            var run = defaultPriceStrat._rnRepository.GetRunDetails(placeInRun.RunId);
            var train = _trainRepository.GetTrainDetails(run.TrainId);

            switch (AgencyId)
            {
                case 1:
            }


        }
    }
}
