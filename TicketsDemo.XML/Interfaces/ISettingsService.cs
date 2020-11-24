using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsDemo.XML.Interfaces
{
   public interface ISettingsService
    {
        string HolidaysXMLPath { get; }
        string PlacesXMLPath { get; }
        string RepXMLPath { get; }
    }
}
