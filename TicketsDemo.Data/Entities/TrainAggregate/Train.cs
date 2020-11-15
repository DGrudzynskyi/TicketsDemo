using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsDemo.Data.Entities
{
    public class Train
    {
        public int Id { get; set; }
        [ConcurrencyCheck]
        public int Number { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public virtual List<Carriage> Carriages { get; set; }
        public virtual List<Run> Runs { get; set; }
    }
}
