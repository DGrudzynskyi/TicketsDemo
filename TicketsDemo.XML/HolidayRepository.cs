using System;
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
using TicketsDemo.XML.Interfaces;

namespace TicketsDemo.XML
{
    public class HolidayRepository : IHolidayRepository
    {
        private ISettingsService SettingsService;
        public HolidayRepository(ISettingsService holidayRep)
        {

            SettingsService = holidayRep;
        }
        public List<Holiday> GetAllHolidays()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Holiday>));
            List<Holiday> holidays;

            using (FileStream fs = new FileStream(SettingsService.HolidaysXMLPath, FileMode.Open))
            {
                holidays = (List<Holiday>)serializer.Deserialize(fs);
            }
            return holidays;
        }

        public void CreateHoliday(Holiday holiday)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Holiday));
            using (FileStream fs = new FileStream(SettingsService.HolidaysXMLPath, FileMode.Append))
            {
                serializer.Serialize(fs, holiday);
            }
        }      
       
    }
}
