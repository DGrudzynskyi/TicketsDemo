using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Domain.Interfaces;

namespace TicketsDemo.Domain.DefaultImplementations
{
   public class AppConfigSettingsService:ICSVPathSettingsService
    {
        public string TrainCSVPath
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory +  ConfigurationManager.AppSettings["TrainCSVPath"];
            }
        }

        public string CarriageCSVPath
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["CarriageCSVPath"];
            }
        }

        public string PlaceCSVPath
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["PlaceCSVPath"];
            }
        }
    }
}
