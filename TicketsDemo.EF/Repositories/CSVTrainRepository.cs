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
                        Id = csv.GetField<int>(_fieldSettingsService.CSVRecordIdFieldId),
                        Number = csv.GetField<int>(_fieldSettingsService.CSVRecordIdFieldNumber),
                        StartLocation = csv.GetField(_fieldSettingsService.CSVRecordIdFieldStartLocation),
                        EndLocation = csv.GetField(_fieldSettingsService.CSVRecordIdFieldEndLocation),
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
                    if (csv.GetField<int>(_fieldSettingsService.CSVRecordIdFieldId) == id)
                    {
                        train = new Train
                        {
                            Id = csv.GetField<int>(_fieldSettingsService.CSVRecordIdFieldId),
                            Number = csv.GetField<int>(_fieldSettingsService.CSVRecordIdFieldNumber),
                            StartLocation = csv.GetField(_fieldSettingsService.CSVRecordIdFieldStartLocation),
                            EndLocation = csv.GetField(_fieldSettingsService.CSVRecordIdFieldEndLocation),
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
                    if (csv.GetField<int>(_fieldSettingsService.CSVRecordIdFieldTrainId) == train.Id)
                    {
                        var carriage = new Carriage
                        {
                            Id = csv.GetField<int>(_fieldSettingsService.CSVRecordIdFieldId),
                            Type = (CarriageType)csv.GetField<int>(_fieldSettingsService.CSVRecordIdFieldType),
                            DefaultPrice = csv.GetField<int>(_fieldSettingsService.CSVRecordIdFieldDefaultPrice),
                            TrainId = csv.GetField<int>(_fieldSettingsService.CSVRecordIdFieldTrainId),
                            Number = csv.GetField<int>(_fieldSettingsService.CSVRecordIdFieldNumber),
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
                        Id = csv.GetField<int>(_fieldSettingsService.CSVRecordIdFieldId),
                        Number = csv.GetField<int>(_fieldSettingsService.CSVRecordIdFieldNumber),
                        PriceMultiplier = csv.GetField<decimal>(_fieldSettingsService.CSVRecordIdFieldPriceMultiplier),
                        CarriageId = csv.GetField<int>(_fieldSettingsService.CSVRecordIdFieldCarriageId),
                        Carriage = train.Carriages.FirstOrDefault(x => x.Id == csv.GetField<int>(_fieldSettingsService.CSVRecordIdFieldCarriageId))
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
