using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsDemo.Data.Entities
{
    public class PlaceInRun
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int CarriageNumber { get; set; }
        public int RunId { get; set; }
        public Run Run { get; set; }
    }
}
