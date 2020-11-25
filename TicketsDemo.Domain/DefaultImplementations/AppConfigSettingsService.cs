using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Domain.Interfaces;

namespace TicketsDemo.Domain.DefaultImplementations
{
    public class AppConfigSettingsService : ISettingsService
    {
        public string TrainXMLPath
        {
            get
            {
                return $@"{AppDomain.CurrentDomain.BaseDirectory}\App_Data\XmlTrainRepository.xml";
            }
        }

        public string LogFilePath
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory + "/log.txt";
            }
        }
    }
}
