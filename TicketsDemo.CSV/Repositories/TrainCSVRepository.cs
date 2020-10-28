using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Repositories;

namespace TicketsDemo.CSV.Repositories
{
    public class TrainCSVRepository : ITrainRepository
    {
        private ICSVConfiguration _csvConfig;

        public void CreateTrain(Train train)
        {
            var trains = GetAllTrains();
            var carriages = trains.SelectMany(t => t.Carriages).ToList();
            var places = carriages.SelectMany(c => c.Places).ToList();

            //assigning id's
            int newCarriageStartId;
            int newPlaceStartId;
            if (trains.Count > 0)
            {
                train.Id = trains.Max(t => t.Id) + 1;
                newCarriageStartId = carriages.Max(c => c.Id) + 1;
                newPlaceStartId = places.Max(p => p.Id) + 1;
            }
            else
            {
                train.Id = 1;
                newCarriageStartId = 1;
                newPlaceStartId = 1;
            }

            foreach(var carriage in train.Carriages)
            {
                carriage.Id = newCarriageStartId++;
            }

            foreach(var place in train.Carriages.SelectMany(c => c.Places))
            {
                place.Id = newPlaceStartId++;
            }

            train.Carriages.ForEach(c => c.TrainId = c.Train.Id);
            train.Carriages.SelectMany(c => c.Places).ToList().ForEach(p => p.CarriageId = p.Carriage.Id);

            trains.Add(train);
            WriteTrains(trains);
        }

        public void DeleteTrain(Train train)
        {
            var trains = GetAllTrains();
            trains.RemoveAll(t => t.Number == train.Id);
            WriteTrains(trains);
        }

        public List<Train> GetAllTrains()
        {
            List<Train> trains;
            List<Carriage> carriages;
            List<Place> places;

            using (var reader = new StreamReader(_csvConfig.TrainsFilePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.Delimiter = ";";
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.RegisterClassMap<TrainMap>();
                trains = csv.GetRecords<Train>().ToList();
            }

            using (var reader = new StreamReader(_csvConfig.CarriagesFilePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.Delimiter = ";";
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.RegisterClassMap<CarriageMap>();
                carriages = csv.GetRecords<Carriage>().ToList();
            }

            using (var reader = new StreamReader(_csvConfig.PlacesFilePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.Delimiter = ";";
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.RegisterClassMap<PlaceMap>();
                places = csv.GetRecords<Place>().ToList();
            }

            foreach(var carriage in carriages)
            {
                carriage.Places = places.FindAll(p => p.CarriageId == carriage.Id);
            }

            foreach(var train in trains)
            {
                train.Carriages = carriages.FindAll(c => c.TrainId == train.Id);
            }

            return trains;
        }

        public Train GetTrainDetails(int trainId)
        {
            var trains = GetAllTrains();
            return trains.Find(t => t.Id == trainId);
        }

        public void UpdateTrain(Train train)
        {
            var trains = GetAllTrains();
            trains.RemoveAll(t => t.Number == train.Id);
            trains.Add(train);
            WriteTrains(trains);
        }

        private void WriteTrains(List<Train> trains)
        {
            if (trains.Count > 0)
            {
                var carriages = trains.SelectMany(t => t.Carriages).ToList();
                var places = carriages.SelectMany(c => c.Places).ToList();

                using (var writer = new StreamWriter(_csvConfig.TrainsFilePath))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {

                    csv.Configuration.Delimiter = ";";
                    csv.Configuration.HasHeaderRecord = true;

                    csv.Configuration.RegisterClassMap<TrainMap>();
                    csv.WriteRecords(trains);
                }

                using (var writer = new StreamWriter(_csvConfig.CarriagesFilePath))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {

                    csv.Configuration.Delimiter = ";";
                    csv.Configuration.HasHeaderRecord = true;

                    csv.Configuration.RegisterClassMap<CarriageMap>();
                    csv.WriteRecords(carriages);
                }

                using (var writer = new StreamWriter(_csvConfig.PlacesFilePath))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {

                    csv.Configuration.Delimiter = ";";
                    csv.Configuration.HasHeaderRecord = true;

                    csv.Configuration.RegisterClassMap<PlaceMap>();
                    csv.WriteRecords(places);
                }
            }
        }
    }
}
