using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Repositories;


namespace TicketsDemo.Xml.Repositories
{
    public class XmlTrainRepository : ITrainRepository
    {
        private IXMLReader _reader;

        public XmlTrainRepository(IXMLReader reader)
        {
            _reader = reader;
        }

        public List<Train> GetAllTrains()
        {
            List<Train> Trains = _reader.ReadFile<Train>($@"{ AppDomain.CurrentDomain.BaseDirectory}\App_Data\Train.xml", "Trains");

            return Trains.ToList();
        }

        public Train GetTrainDetails(int id)
        {
            var train = GetAllTrains().Single(t => t.Id == id);
            train.Carriages = new List<Carriage>();

            List<Carriage> resuCarr = _reader.ReadFile<Carriage>($@"{ AppDomain.CurrentDomain.BaseDirectory}\App_Data\Carriage.xml", "Carriages");
            List<Carriage> tempCarr = new List<Carriage>();

            foreach (var carrT in resuCarr)
            {
                if (id == carrT.TrainId)
                {
                    tempCarr.Add(carrT);
                }
            }

            foreach (var carriageDTO in tempCarr)
            {
                if (id == carriageDTO.TrainId)
                {
                    carriageDTO.Places = new List<Place>();
                    carriageDTO.Train = train;

                    List<Place> resuPlac = _reader.ReadFile<Place>($@"{ AppDomain.CurrentDomain.BaseDirectory}\App_Data\Place.xml", "Places");

                    foreach (var plac in resuPlac)
                    {
                        if (carriageDTO.Id == plac.CarriageId)
                        {
                            plac.Carriage = carriageDTO;
                            carriageDTO.Places.Add(plac);
                        }
                    }               
                }
                train.Carriages.Add(carriageDTO);
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
