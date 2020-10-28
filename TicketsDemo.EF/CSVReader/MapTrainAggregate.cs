using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using CsvHelper.Configuration;

namespace TicketsDemo.EF.CSVReader
{
    class MapTrainAggregate
    {
        public class MapTrain : ClassMap<Train>
        {
            public MapTrain()
            {
                Map(t => t.Id).Name("Id");
                Map(t => t.Number).Name("Number");
                Map(t => t.StartLocation).Name("StartLocation");
                Map(t => t.EndLocation).Name("EndLocation");
            }
        }
        public class MapCarriage : ClassMap<Carriage>
        {
            public MapCarriage()
            {
                Map(c => c.Id).Name("Id");
                Map(c => c.Type).Name("Type");
                Map(c => c.DefaultPrice).Name("DefaultPrice");
                Map(c => c.TrainId).Name("TrainId");
                Map(c => c.Number).Name("Number");
            }
        }
        public class MapPlace : ClassMap<Place>
        {
            public MapPlace()
            {
                Map(p => p.Id).Name("Id");
                Map(p => p.Number).Name("Number");
                Map(p => p.PriceMultiplier).Name("PriceMultiplier");
                Map(p => p.CarriageId).Name("CarriageId");
            }
        }

    }
}
