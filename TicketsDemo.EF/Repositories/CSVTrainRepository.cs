using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Repositories;

namespace TicketsDemo.EF.Repositories
{
   public class CSVTrainRepository: ITrainRepository
    {
        private string _path = $@"{AppDomain.CurrentDomain.BaseDirectory}\App_Data\";
        public List<Train> GetAllTrains()
        {
            var trains = new List<Train>();
            using (var reader = new StreamReader(_path + "trains.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var train = new Train
                    {
                        Id = csv.GetField<int>("Id"),
                        Number = csv.GetField<int>("Number"),
                        StartLocation = csv.GetField("StartLocation"),
                        EndLocation = csv.GetField("EndLocation")
                    };
                    trains.Add(train);
                }
            }
            return trains;
        }

        public Data.Entities.Train GetTrainDetails(int id)
        {
            var train = new Train();
            using (var reader = new StreamReader(_path + "trains.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    if (csv.GetField<int>("Id") == id)
                    {
                        train = new Train
                        {
                            Id = csv.GetField<int>("Id"),
                            Number = csv.GetField<int>("Number"),
                            StartLocation = csv.GetField("StartLocation"),
                            EndLocation = csv.GetField("EndLocation"),
                            Carriages = new List<Carriage>()
                        };
                    }
                }
            }

            using (var reader = new StreamReader(_path + "carriages.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    if (csv.GetField<int>("TrainId") == train.Id)
                    {
                        var carriage = new Carriage
                        {
                            Id = csv.GetField<int>("Id"),
                            Type = (CarriageType)csv.GetField<int>("Type"),
                            DefaultPrice = csv.GetField<int>("DefaultPrice"),
                            TrainId = csv.GetField<int>("TrainId"),
                            Number = csv.GetField<int>("Number"),
                            Places = new List<Place>()
                        };
                        train.Carriages.Add(carriage);
                    }
                }
            }

            using (var reader = new StreamReader(_path + "places.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    if (train.Carriages.Any(x=>x.Id == csv.GetField<int>("CarriageId")))
                    {
                       var place = new Place
                        {
                            Id = csv.GetField<int>("Id"),
                            Number = csv.GetField<int>("Number"),
                            PriceMultiplier = csv.GetField<decimal>("PriceMultiplier"),
                            CarriageId = csv.GetField<int>("CarriageId"),
                            Carriage = train.Carriages.FirstOrDefault(x=>x.Id == csv.GetField<int>("CarriageId"))
                       };
                        train.Carriages.FirstOrDefault(x => x.Id == place.CarriageId).Places.Add(place);
                    }
                }
            }
            return train;
        }

        public void CreateTrain(Train train)
        {
            throw new NotImplementedException();
        }

        public void DeleteTrain(Train train)
        {
            throw new NotImplementedException();
        }

        public void UpdateTrain(Train train)
        {
            throw new NotImplementedException();
        }
    }
}
