using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;

namespace TicketsDemo.Data.OptionsForCalculationPrice
{
    public struct TeaCoffeeBedParametrs
    {
        public Ticket ticket { get; set; }
        public PlaceInRun placeInRun { get; set; }
        public bool IsBed { get; set; }
        public bool IsTea { get; set; }
        public bool IsCoffee { get; set; }
    }

}
