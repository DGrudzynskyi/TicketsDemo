using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TicketsDemo.Data.Entities;
using TicketsDemo.Domain.Interfaces;

namespace TicketsDemo.Xml
{
    public class XMLReader : IXMLReader
    {
        private ISettingsService _settingsService;

        public XMLReader(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        public List<T> XMLRead<T>()
        {
            var serializer = new XmlSerializer(typeof(List<T>));
            List<T> trains;

            using (FileStream fs = new FileStream(_settingsService.TrainXMLPath, FileMode.Open))
            {
                 return trains = (List<T>)serializer.Deserialize(fs);
            }
        }
    }
}
