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
    public class HolidayRepository : IHolidayRepository
    {
        XMLSettingsService xml_set = new XMLSettingsService();
        public List<Holiday> GetHolidaysList()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Holiday>));
            List<Holiday> holidays;

            using (FileStream fs = new FileStream(xml_set.HolidaysXMLPath, FileMode.Open))
            {
                holidays = (List<Holiday>)serializer.Deserialize(fs);
            }
            return holidays;
        }

        public void CreateHoliday(Holiday holiday)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Holiday));
            using (FileStream fs = new FileStream(xml_set.HolidaysXMLPath, FileMode.Append))
            {
                serializer.Serialize(fs, holiday);
            }
        }

       

       
    }
}
