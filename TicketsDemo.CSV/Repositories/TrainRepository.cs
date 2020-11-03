using System;
using System.Collections.Generic;
using System.Text;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Repositories;
using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;
using System.IO;
using System.Linq;
using TicketsDemo.CSV;
using System.Configuration;

namespace TicketsDemo.CSV.Repositories
{
    public class TrainRepository : ITrainRepository
    {

        private CSVSettingsService csv_set = new CSVSettingsService();


        public void CreateTrain(Train train)
        {
            List<Train> allTrains = GetAllTrains();
            allTrains.Add(train);
            WriteTrains(allTrains);
        }

        public void DeleteTrain(Train train)
        {
            List<Train> allTrains = GetAllTrains();
            Train removingTrain;
            removingTrain = allTrains.Single(t => t.Id == train.Id);
            allTrains.Remove(removingTrain);
            WriteTrains(allTrains);
        }

        public List<Train> GetAllTrains()
        {
            List<Train> trains;
            List<Carriage> carriages;
            List<Place> places;

            using (var reader = new StreamReader(csv_set.TrainCSVPath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.Delimiter = ";";
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.RegisterClassMap<TrainMap>();
                trains = csv.GetRecords<Train>().ToList();
            }

            using (var reader = new StreamReader(csv_set.CarriageCSVPath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.Delimiter = ";";
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.RegisterClassMap<CarriageMap>();
                carriages = csv.GetRecords<Carriage>().ToList();
            }

            using (var reader = new StreamReader(csv_set.PlaceCSVPath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.Delimiter = ";";
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.RegisterClassMap<PlaceMap>();
                places = csv.GetRecords<Place>().ToList();
            }

            foreach (Carriage carriage in carriages)
            {
                int carriageId = carriage.Id;
                carriage.Places = new List<Place>();

                foreach (Place place in places)
                {
                    if (carriageId == place.CarriageId)
                    {
                        place.CarriageId = carriageId;
                        place.Carriage = carriage;
                        carriage.Places.Add(place);
                    }
                }
            }

            foreach (Train train in trains)
            {
                int trainId = train.Id;
                train.Carriages = new List<Carriage>();

                foreach (Carriage carriage in carriages)
                {
                    if (trainId == carriage.TrainId)
                    {
                        carriage.TrainId = trainId;
                        carriage.Train = train;
                        train.Carriages.Add(carriage);
                    }
                }
            }

            return trains;
        }

        public Train GetTrainDetails(int trainId)
        {
            List<Train> allTrains = GetAllTrains();
            return allTrains.Find(t => t.Id == trainId);
        }

        public void UpdateTrain(Train train)
        {
            List<Train> allTrains = GetAllTrains();
            Train removingTrain = allTrains.Single(x => x.Id == train.Id);
            TrainRepository trainRep = new TrainRepository();

            allTrains.Remove(removingTrain);
            allTrains.Add(train);

            trainRep.WriteTrains(allTrains);

        }

        private void WriteTrains(List<Train> trains)
        {

            using (var writer = new StreamWriter(csv_set.TrainCSVPath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {

                csv.Configuration.Delimiter = ";";
                csv.Configuration.HasHeaderRecord = true;

                csv.Configuration.RegisterClassMap<TrainMap>();
                csv.WriteRecords(trains);
            }
        }


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


    }


}

