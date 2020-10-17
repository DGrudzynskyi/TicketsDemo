using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsDemo.Data.Entities
{
    public class Agent
    {
        public string Id { get; set; }
        public string CompanyId { get; set; }
        public double Percent { get; set; }
    }
}
