using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;

namespace TicketsDemo.CSV1
{
    public class TrainMap : ClassMap<Train>
    {
        public TrainMap()
        {
            Map(m => m.Id).Name("Id");
            Map(m => m.Number).Name("Number");
            Map(m => m.StartLocation).Name("StartLocation");
            Map(m => m.EndLocation).Name("EndLocation");
        }
    }

    public class PlaceMap : ClassMap<Place>
    {
        public PlaceMap()
        {
            Map(m => m.Id).Name("Id");
            Map(m => m.Number).Name("Number");
            Map(m => m.PriceMultiplier).Name("PriceMultiplier");
            Map(m => m.CarriageId).Name("CarriageId");
        }
    }

    public class CarriageMap : ClassMap<Carriage>
    {
        public CarriageMap()
        {
            Map(m => m.Id).Name("Id");
            Map(m => m.Type).Name("Type");
            Map(m => m.TrainId).Name("TrainId");
            Map(m => m.DefaultPrice).Name("DefaultPrice");
            Map(m => m.Number).Name("Number");
        }
    }

   
}
