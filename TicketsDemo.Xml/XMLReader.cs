using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TicketsDemo.Xml
{
    public class XMLReader : IXMLReader
    {
        public List<T> ReadFile<T>(string file, string elementName)
        {
            using (var fs = new FileStream(file, FileMode.Open))
            {
                var ser = new XmlSerializer(typeof(List<T>), new XmlRootAttribute(elementName));

                return (List<T>)ser.Deserialize(fs);
            }
        }
    }
}
