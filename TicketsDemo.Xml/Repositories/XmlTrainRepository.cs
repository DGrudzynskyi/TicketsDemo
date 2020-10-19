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
            /*using (FileStream fs = new FileStream("C:\\Users\\Вова\\Desktop\\DB\\Train.xml", FileMode.Open))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Train>), new XmlRootAttribute("Trains")
                {
                    Namespace = "urn:task-manager"
                });
                List<Train> res = (List<Train>)xmlSerializer.Deserialize(fs);

                foreach(var dog in res)
                {
                    dog.Carriages = new List<Carriage>();
                }

                return res;
            }*/
        }
        public Train GetTrainDetails(int id)
        {
            var train = GetAllTrains().Single(t => t.Id == id);
            train.Carriages = new List<Carriage>();

            using (FileStream fs = new FileStream("C:\\Users\\Вова\\Desktop\\DB\\Carriage.xml", FileMode.Open))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Carriage>), new XmlRootAttribute("Carriages"));
                List<Carriage> resuCarr = (List<Carriage>)xmlSerializer.Deserialize(fs);

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

                        using (FileStream fs2 = new FileStream("C:\\Users\\Вова\\Desktop\\DB\\Place.xml", FileMode.Open))
                        {
                            XmlSerializer xmlSerializer2 = new XmlSerializer(typeof(List<Place>), new XmlRootAttribute("Places"));
                            List<Place> resuPlac = (List<Place>)xmlSerializer2.Deserialize(fs2);

                            foreach (var plac in resuPlac)
                            {
                                if (carriageDTO.Id == plac.CarriageId)
                                {
                                    plac.Carriage = carriageDTO;
                                    carriageDTO.Places.Add(plac);
                                }
                            }

                        }
                    }
                    train.Carriages.Add(carriageDTO);
                }
                return train;
            }

            /*var train = GetAllTrains().Single(t => t.Id == id);
            train.Carriages = new List<Carriage>();

            using (FileStream fs = new FileStream("C:\\Users\\Вова\\Desktop\\DB\\Carriage.xml", FileMode.Open))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Carriage>), new XmlRootAttribute("Carriages"));
                List<Carriage> resuCarr = (List<Carriage>)xmlSerializer.Deserialize(fs);

                List<Carriage> tempCarr = new List<Carriage>();

                foreach (var carrT in resuCarr)
                {
                    if(id == carrT.TrainId)
                    {
                        tempCarr.Add(carrT);
                    }
                }

                foreach (var carriageDTO in tempCarr) 
                {
                    if(id == carriageDTO.TrainId)
                    {             
                        carriageDTO.Places = new List<Place>();
                        carriageDTO.Train = train;

                        using (FileStream fs2 = new FileStream("C:\\Users\\Вова\\Desktop\\DB\\Place.xml", FileMode.Open))
                        {
                            XmlSerializer xmlSerializer2 = new XmlSerializer(typeof(List<Place>), new XmlRootAttribute("Places"));
                            List<Place> resuPlac = (List<Place>)xmlSerializer2.Deserialize(fs2);
                     
                            foreach (var plac in resuPlac)
                            {
                                if(carriageDTO.Id == plac.CarriageId)
                                {
                                    plac.Carriage = carriageDTO;
                                    carriageDTO.Places.Add(plac);
                                }       
                            }

                        }
                    }
                    train.Carriages.Add(carriageDTO);
                }
                return train;
            }*/
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
