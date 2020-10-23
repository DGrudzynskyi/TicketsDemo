using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Repositories;
using TicketsDemo.DTO;
using TicketsDemo.Domain.Interfaces;

namespace TicketsDemo.EF.Repositories
{
    public class CSVTrainRepository : ITrainRepository
    {
        private ISettingsService _settingsService;
        public CSVTrainRepository(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        public List<Train> GetAllTrains()
        {
            var trains = new List<Train>();
            using (var reader = new StreamReader(_settingsService.TrainCSVPath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var train = new Train
                    {
                        Id = csv.GetField<int>(_settingsService.Id),
                        Number = csv.GetField<int>(_settingsService.Number),
                        StartLocation = csv.GetField(_settingsService.StartLocation),
                        EndLocation = csv.GetField(_settingsService.EndLocation),
                    };
                    trains.Add(train);
                }
            }
            return trains;
        }

        public Train GetTrainDetails(int id)
        {
            var train = new Train();
            using (var reader = new StreamReader(_settingsService.TrainCSVPath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    if (csv.GetField<int>(_settingsService.Id) == id)
                    {
                        train = new Train
                        {
                            Id = csv.GetField<int>(_settingsService.Id),
                            Number = csv.GetField<int>(_settingsService.Number),
                            StartLocation = csv.GetField(_settingsService.StartLocation),
                            EndLocation = csv.GetField(_settingsService.EndLocation),
                            Carriages = new List<Carriage>()
                        };
                    }
                }
            }

            using (var reader = new StreamReader(_settingsService.CarriageCSVPath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    if (csv.GetField<int>(_settingsService.TrainId) == train.Id)
                    {
                        var carriage = new Carriage
                        {
                            Id = csv.GetField<int>(_settingsService.Id),
                            Type = (CarriageType)csv.GetField<int>(_settingsService.Type),
                            DefaultPrice = csv.GetField<int>(_settingsService.DefaultPrice),
                            TrainId = csv.GetField<int>(_settingsService.TrainId),
                            Number = csv.GetField<int>(_settingsService.Number),
                            Places = new List<Place>()
                        };
                        train.Carriages.Add(carriage);
                    }
                }
            }

            using (var reader = new StreamReader(_settingsService.PlaceCSVPath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var place = new Place
                    {
                        Id = csv.GetField<int>(_settingsService.Id),
                        Number = csv.GetField<int>(_settingsService.Number),
                        PriceMultiplier = csv.GetField<decimal>(_settingsService.PriceMultiplier),
                        CarriageId = csv.GetField<int>(_settingsService.CarriageId),
                        Carriage = train.Carriages.FirstOrDefault(x => x.Id == csv.GetField<int>(_settingsService.CarriageId))
                    };
                    train.Carriages.FirstOrDefault(x => x.Id == place.CarriageId)?.Places.Add(place);
                }
            }
            return train;
        }

        public void CreateTrain(Train train)
        {
            var trains = GetAllTrains();
            trains.Add(train);
            WriteAllTrains(trains);
        }

        public void DeleteTrain(Train train)
        {
            var trains = GetAllTrains();
            trains.RemoveAll(x => x.Id == train.Id);
            WriteAllTrains(trains);
        }

        public void UpdateTrain(Train train)
        {
            var trains = GetAllTrains();
            var removeTrain = trains.FirstOrDefault(x => x.Id == train.Id);
            trains.Remove(removeTrain);
            trains.Add(train);
            WriteAllTrains(trains);
        }

        public void WriteAllTrains(IList<Train> trains)
        {
            using (var writer = new StreamWriter(_settingsService.TrainCSVPath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.Delimiter = ",";

                csv.Configuration.RegisterClassMap<TrainDto>();
                csv.WriteRecords(trains);
            }
        }
    }
}
