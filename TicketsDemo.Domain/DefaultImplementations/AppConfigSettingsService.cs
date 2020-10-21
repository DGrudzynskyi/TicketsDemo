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
   public class AppConfigSettingsService:ISettingsService
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

        public string Id
        {
            get
            {
                return ConfigurationManager.AppSettings["Id"];
            }
        }
        public string Number
        {
            get
            {
                return ConfigurationManager.AppSettings["Number"];
            }
        }
        public string StartLocation
        {
            get
            {
                return  ConfigurationManager.AppSettings["StartLocation"];
            }
        }
        public string EndLocation
        {
            get
            {
                return  ConfigurationManager.AppSettings["EndLocation"];
            }
        }
        public string TrainId
        {
            get
            {
                return  ConfigurationManager.AppSettings["TrainId"];
            }
        }
        public string Type
        {
            get
            {
                return ConfigurationManager.AppSettings["Type"];
            }
        }
        public string DefaultPrice
        {
            get
            {
                return ConfigurationManager.AppSettings["DefaultPrice"];
            }
        }
        public string PriceMultiplier
        {
            get
            {
                return ConfigurationManager.AppSettings["PriceMultiplier"];
            }
        }
        public string CarriageId
        {
            get
            {
                return ConfigurationManager.AppSettings["CarriageId"];
            }
        }
    }
}
