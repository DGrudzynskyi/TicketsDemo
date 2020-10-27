using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using TicketsDemo.Data.Entities;

namespace TicketsDemo.XML
{
    class XMLWorker
    {
        public void Writer()
        {
            XDocument xDoc = XDocument.Load("D:\\tickets\\TicketsDemo\\XMLRepository.xml");
            // создаем новый элемент train
            XElement train2 = new XElement("train");
            // создаем элементы
            XElement NumberElem = new XElement("Number", "71");
            XElement StartLocationElem = new XElement("StartLocationElem", "Kiev");
            XElement EndLocationElem = new XElement("EndLocation", "Chop");
            XElement Carriage1 = new XElement("Carriage");
            XElement Carriage2 = new XElement("Carriage");
            XElement Carriage3 = new XElement("Carriage");
            XElement Carriage4 = new XElement("Carriage");
            //добавляем узлы
            train2.Add(NumberElem);
            train2.Add(StartLocationElem);
            train2.Add(EndLocationElem);
            // создаем корневой элемент
            XElement traines = new XElement("traines");
            // добавляем в корневой элемент
            traines.Add(train2);
       
            // добавляем корневой элемент в документ
            xDoc.Add(traines);

            xDoc.Save("D:\\tickets\\TicketsDemo\\XMLRepository.xml");
           
        }

        Train GenerateTrain(int trainID, string startLocationName, string destiny, int firstCarriageId) {
            Train train = new Train();
            Random random = new Random();

            train.Id = trainID;
            train.StartLocation = startLocationName;
            train.EndLocation = destiny;
            train.Number = random.Next(100, 200);
            train.Carriages = GenerateCarriageList(train, firstCarriageId);

            return train;
        }

        List<Carriage> GenerateCarriageList(Train train, int firstCarriageId)
        {
            Random random = new Random();
            List <Carriage> carriageList = new List<Carriage>();
            Carriage generatedCarriage;
            int carriageCount = random.Next(1, 10);

            for(int counter = 0; counter < carriageCount; counter++)
            {
                generatedCarriage = new Carriage();
                generatedCarriage.DefaultPrice = 100;
                generatedCarriage.TrainId = train.Id;
                generatedCarriage.Type = (CarriageType)random.Next(1, 3);
                generatedCarriage.Train = train;
                generatedCarriage.Number = counter;
                generatedCarriage.Id = firstCarriageId + carriageCount; 
             
            }

            return carriageList;

        }
    }
}
