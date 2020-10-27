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
        private ICSVPathSettingsService _pathSettingsService;
        private ICSVFieldSettingsService _fieldSettingsService;
        public CSVTrainRepository(ICSVPathSettingsService pathSettingsService, ICSVFieldSettingsService fieldSettingsService)
        {
            _pathSettingsService = pathSettingsService;
            _fieldSettingsService = fieldSettingsService;
        }

        public List<Train> GetAllTrains()
        {
            var trains = new List<Train>();
            using (var reader = new StreamReader(_pathSettingsService.TrainCSVPath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var train = new Train
                    {
                        Id = csv.GetField<int>(_fieldSettingsService.Id),
                        Number = csv.GetField<int>(_fieldSettingsService.Number),
                        StartLocation = csv.GetField(_fieldSettingsService.StartLocation),
                        EndLocation = csv.GetField(_fieldSettingsService.EndLocation),
                    };
                    trains.Add(train);
                }
            }
            return trains;
        }

        public Train GetTrainDetails(int id)
        {
            var train = new Train();
            using (var reader = new StreamReader(_pathSettingsService.TrainCSVPath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    if (csv.GetField<int>(_fieldSettingsService.Id) == id)
                    {
                        train = new Train
                        {
                            Id = csv.GetField<int>(_fieldSettingsService.Id),
                            Number = csv.GetField<int>(_fieldSettingsService.Number),
                            StartLocation = csv.GetField(_fieldSettingsService.StartLocation),
                            EndLocation = csv.GetField(_fieldSettingsService.EndLocation),
                            Carriages = new List<Carriage>()
                        };
                    }
                }
            }

            using (var reader = new StreamReader(_pathSettingsService.CarriageCSVPath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    if (csv.GetField<int>(_fieldSettingsService.TrainId) == train.Id)
                    {
                        var carriage = new Carriage
                        {
                            Id = csv.GetField<int>(_fieldSettingsService.Id),
                            Type = (CarriageType)csv.GetField<int>(_fieldSettingsService.Type),
                            DefaultPrice = csv.GetField<int>(_fieldSettingsService.DefaultPrice),
                            TrainId = csv.GetField<int>(_fieldSettingsService.TrainId),
                            Number = csv.GetField<int>(_fieldSettingsService.Number),
                            Places = new List<Place>()
                        };
                        train.Carriages.Add(carriage);
                    }
                }
            }

            using (var reader = new StreamReader(_pathSettingsService.PlaceCSVPath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var place = new Place
                    {
                        Id = csv.GetField<int>(_fieldSettingsService.Id),
                        Number = csv.GetField<int>(_fieldSettingsService.Number),
                        PriceMultiplier = csv.GetField<decimal>(_fieldSettingsService.PriceMultiplier),
                        CarriageId = csv.GetField<int>(_fieldSettingsService.CarriageId),
                        Carriage = train.Carriages.FirstOrDefault(x => x.Id == csv.GetField<int>(_fieldSettingsService.CarriageId))
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
            using (var writer = new StreamWriter(_pathSettingsService.TrainCSVPath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.Delimiter = ",";

                csv.WriteRecords(trains);
            }
        }
    }
}
