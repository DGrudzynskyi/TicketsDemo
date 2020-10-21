using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsDemo.Data.Entities.Decorators
{
    public abstract class PriceDecorator : PriceComponent
    {
        protected PriceComponent component;
        public PriceDecorator(PriceComponent component):base()
        {
            this.component = component;
        }
    }
}
