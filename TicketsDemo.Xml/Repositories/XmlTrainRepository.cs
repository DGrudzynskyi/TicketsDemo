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
using TicketsDemo.Xml.Interfaces;

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

            var trains = _reader.XMLRead<Train>();

            foreach (Train train in trains)
            {
                foreach (Carriage carriage in train.Carriages)
                {
                    carriage.Train = train;
                    foreach (Place place in carriage.Places)
                    {
                        place.Carriage = carriage;
                    }
                }
            }
            return trains;
        }

        public Train GetTrainDetails(int id)
        {
            List<Train> trains = GetAllTrains();
            return trains.First(x => x.Id == id);
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
