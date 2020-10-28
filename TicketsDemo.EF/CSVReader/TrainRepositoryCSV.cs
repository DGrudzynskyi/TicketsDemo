using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Repositories;
using System.IO;
using CsvHelper;
using System.Globalization;

namespace TicketsDemo.EF.CSVReader
{
    public class TrainRepositoryCSV : ITrainRepository
    {
        private string _trainFile;
        private string _carriagesFile;
        private string _placeFile;
        //public TrainRepositoryCSV(string trainFile = @"D:\1\3 курс\Proga\TicketsDemo\TicketsDemo\Trains.csv",
        //                          string carriagesFile= @"D:\1\3 курс\Proga\TicketsDemo\TicketsDemo\Carriages.csv",
        //                          string placeFile = @"D:\1\3 курс\Proga\TicketsDemo\TicketsDemo\Places.csv") 
        //{
        //    _trainFile = trainFile;
        //    _carriagesFile = carriagesFile;
        //    _placeFile = placeFile;
        //}
        public TrainRepositoryCSV()
        {
            _trainFile = AppDomain.CurrentDomain.BaseDirectory + "Trains.csv";
            _carriagesFile = AppDomain.CurrentDomain.BaseDirectory + "Carriages.csv";
            _placeFile = AppDomain.CurrentDomain.BaseDirectory + "Places.csv";
        }
        #region ITrainRepository Members
        public List<Train> GetAllTrains()
        {
            List<Train> Trains;
            List<Carriage> Carriages;
            List<Place> Places;
            using (StreamReader streamReader = new StreamReader(_trainFile))
            {
                using (CsvReader csvReader = new CsvReader(streamReader, System.Globalization.CultureInfo.CurrentCulture))
                {
                    csvReader.Configuration.Delimiter = ";";
                    csvReader.Configuration.HasHeaderRecord = true;
                    csvReader.Configuration.RegisterClassMap<MapTrainAggregate.MapTrain>();
                    Trains = csvReader.GetRecords<Train>().ToList();
                }
            }
            using (StreamReader streamReader = new StreamReader(_carriagesFile))
                using (CsvReader csvReader = new CsvReader(streamReader, System.Globalization.CultureInfo.CurrentCulture))
                {
                    csvReader.Configuration.Delimiter = ";";
                    csvReader.Configuration.HasHeaderRecord = true;
                    csvReader.Configuration.RegisterClassMap<MapTrainAggregate.MapCarriage>();
                    Carriages = csvReader.GetRecords<Carriage>().ToList();
                }
            using (StreamReader streamReader = new StreamReader(_placeFile))
                using (CsvReader csvReader = new CsvReader(streamReader, System.Globalization.CultureInfo.CurrentCulture))
                {
                    csvReader.Configuration.Delimiter = ";";
                    csvReader.Configuration.HasHeaderRecord = true;
                    csvReader.Configuration.RegisterClassMap<MapTrainAggregate.MapPlace>();
                    Places = csvReader.GetRecords<Place>().ToList();
                }
            foreach (Carriage carriage in Carriages)
            {
                int carriageId = carriage.Id;
                carriage.Places = new List<Place>();
                foreach (Place place in Places)
                {
                    if (carriageId == place.CarriageId)
                    {
                        place.CarriageId = carriageId;
                        place.Carriage = carriage;
                        carriage.Places.Add(place);
                    }
                }
            }
            foreach (Train train in Trains)
            {
                int trainId = train.Id;
                train.Carriages = new List<Carriage>();

                foreach (Carriage carriage in Carriages)
                {
                    if (trainId == carriage.TrainId)
                    {
                        carriage.TrainId = trainId;
                        carriage.Train = train;
                        train.Carriages.Add(carriage);
                    }
                }
            }

            return Trains;
        }
        public Data.Entities.Train GetTrainDetails(int id)
        {
            List<Train> trains = GetAllTrains();
            foreach (Train train in trains)
            {
                if (train.Id == id)
                {
                    return train;
                }
            }
            return new Train();
        }
        public void CreateTrain(Data.Entities.Train train)
        {
            List<Train> trains = GetAllTrains();
            trains.Add(train);
            WriteTrains(trains);
        }

        public void UpdateTrain(Data.Entities.Train train)
        {
            List<Train> trains = GetAllTrains();
            Train removeTrain = trains.Single(x => x.Id == train.Id);
            TrainRepositoryCSV trainRep = new TrainRepositoryCSV();
            trains.Remove(removeTrain);
            trains.Add(train);
            trainRep.WriteTrains(trains);
        }

        public void DeleteTrain(Data.Entities.Train train)
        {
            List<Train> trains = GetAllTrains();
            List<Train> allTrains = GetAllTrains();
            Train removeTrain = trains.Single(t => t.Id == train.Id);
            trains.Remove(removeTrain);
            WriteTrains(trains);
        }
        #endregion
        public void WriteTrains(List<Train> train)
        {
            using (StreamWriter streamWriter = new StreamWriter(_trainFile))
            using (CsvWriter csvWriter = new CsvWriter(streamWriter, System.Globalization.CultureInfo.CurrentCulture))
            {
                csvWriter.Configuration.Delimiter = ";";
                csvWriter.Configuration.HasHeaderRecord = true;
                csvWriter.Configuration.RegisterClassMap<MapTrainAggregate.MapTrain>();
                csvWriter.WriteRecords(train);
            }
        }
    }
}
