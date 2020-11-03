﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Repositories;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;

namespace TicketsDemo.XML
{
    public class XmlTrainRepository : ITrainRepository
    {
        XMLSettingsService xml_set = new XMLSettingsService();
        public List<Train> GetAllTrains()
        {
            
      
            
            var serializer = new XmlSerializer(typeof(List<Train>));
            List<Train> trains;
            using (FileStream fs = new FileStream(xml_set.PlacesXMLPath, FileMode.Open))
            {
                trains = (List<Train>)serializer.Deserialize(fs);
            }
            foreach (Train train in trains)
            {
                foreach (Carriage carriage in train.Carriages)
                {
                    carriage.Train = train;
                    foreach(Place place in carriage.Places)
                    {
                        place.Carriage = carriage;
                    }
                }
            }
            return trains;
        }
        public Train GetTrainDetails(int trainId)
        {
            List<Train> trains = GetAllTrains();
            return trains.First(x => x.Id == trainId);
        }
        public void CreateTrain(Train train)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Train));
            using (FileStream fs = new FileStream(xml_set.PlacesXMLPath, FileMode.Append))
            {
                serializer.Serialize(fs, train);
            }
            
        }
        public void UpdateTrain(Train train)
        {
            List<Train> trains = GetAllTrains();
            Train trainToRemove = trains.Single(x => x.Id == train.Id);
            trains.Remove(trainToRemove);
            trains.Add(train);
            SerializeListOfTrain(trains);
        }
        public void DeleteTrain(Train train)
        {
            XDocument xDoc = XDocument.Load(xml_set.PlacesXMLPath);
            foreach (XElement xelem in xDoc.Root.Elements("Train"))
            {
                if (xelem.Element("Id").Value == train.Id.ToString())
                {
                    xelem.Remove();
                }
            }
            xDoc.Save(xml_set.PlacesXMLPath);
        }

        public void SerializeListOfTrain(List<Train> trains)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Train>));
            using (FileStream fs = new FileStream(xml_set.PlacesXMLPath, FileMode.Create))
            {
                serializer.Serialize(fs, trains);
            }
        }

       
    }
}
