using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsDemo.Xml
{
    public interface IXMLReader
    {
        List<T> XMLRead<T>();
    }
}
