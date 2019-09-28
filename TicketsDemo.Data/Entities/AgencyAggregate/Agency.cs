using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsDemo.Data.Entities
{
   public  class Agency
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal PriceForUsing { get; set; }
    }
}
