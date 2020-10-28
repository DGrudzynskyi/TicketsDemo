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
using System.Data.Entity.Infrastructure;

namespace TicketsDemo.EF.CSVReader
{
    public class TrainRepositoryCSV : ITrainRepository
    {
        private string _trainFile;
        private string _carriagesFile;
        private string _placeFile;
        public TrainRepositoryCSV()
        {
            _trainFile = AppDomain.CurrentDomain.BaseDirectory + "Trains.csv";
            _carriagesFile = AppDomain.CurrentDomain.BaseDirectory + "Carriages.csv";
            _placeFile = AppDomain.CurrentDomain.BaseDirectory + "Places.csv";
        }
        private CsvReader CreateReader(StreamReader streamReader)
        {
            CsvReader csvReader= null;
            try
            {
                csvReader = new CsvReader(streamReader, System.Globalization.CultureInfo.CurrentCulture);
                csvReader.Configuration.Delimiter = ";";
                csvReader.Configuration.HasHeaderRecord = true;
                csvReader.Configuration.RegisterClassMap<MapTrainAggregate.MapTrain>();
                csvReader.Configuration.RegisterClassMap<MapTrainAggregate.MapCarriage>();
                csvReader.Configuration.RegisterClassMap<MapTrainAggregate.MapPlace>();
                return csvReader;
            }
            catch (Exception e)
            {
                if (csvReader != null)
                {
                    csvReader.Dispose();
                }
                throw e;
            }
        }
        #region ITrainRepository Members
        public List<Train> GetAllTrains()
        {
            List<Train> Trains;
            List<Carriage> Carriages;
            List<Place> Places;
            using (StreamReader streamReader = new StreamReader(_trainFile))
            {
                CsvReader csvReader = CreateReader(streamReader);
                Trains = csvReader.GetRecords<Train>().ToList();
            }
            using (StreamReader streamReader = new StreamReader(_carriagesFile))
            {
                using (CsvReader csvReader = CreateReader(streamReader))
                    Carriages = csvReader.GetRecords<Carriage>().ToList();
            }
            using (StreamReader streamReader = new StreamReader(_placeFile))
            {
                CsvReader csvReader = CreateReader(streamReader);                
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
            return trains.FirstOrDefault(t => t.Id == id);            
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
            trains.Remove(removeTrain);
            trains.Add(train);
            WriteTrains(trains);
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
        private void WriteTrains(List<Train> train)
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
