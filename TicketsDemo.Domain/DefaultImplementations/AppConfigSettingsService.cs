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
   public class AppConfigSettingsService:ICSVPathSettingsService, ICSVFieldSettingsService
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

        public string CSVRecordIdFieldId
        {
            get
            {
                return ConfigurationManager.AppSettings["CSVRecordIdFieldId"];
            }
        }
        public string CSVRecordIdFieldNumber
        {
            get
            {
                return ConfigurationManager.AppSettings["CSVRecordIdFieldNumber"];
            }
        }
        public string CSVRecordIdFieldStartLocation
        {
            get
            {
                return  ConfigurationManager.AppSettings["CSVRecordIdFieldStartLocation"];
            }
        }
        public string CSVRecordIdFieldEndLocation
        {
            get
            {
                return  ConfigurationManager.AppSettings["CSVRecordIdFieldEndLocation"];
            }
        }
        public string CSVRecordIdFieldTrainId
        {
            get
            {
                return  ConfigurationManager.AppSettings["CSVRecordIdFieldTrainId"];
            }
        }
        public string CSVRecordIdFieldType
        {
            get
            {
                return ConfigurationManager.AppSettings["CSVRecordIdFieldType"];
            }
        }
        public string CSVRecordIdFieldDefaultPrice
        {
            get
            {
                return ConfigurationManager.AppSettings["CSVRecordIdFieldDefaultPrice"];
            }
        }
        public string CSVRecordIdFieldPriceMultiplier
        {
            get
            {
                return ConfigurationManager.AppSettings["CSVRecordIdFieldPriceMultiplier"];
            }
        }
        public string CSVRecordIdFieldCarriageId
        {
            get
            {
                return ConfigurationManager.AppSettings["CSVRecordIdFieldCarriageId"];
            }
        }
    }
}
