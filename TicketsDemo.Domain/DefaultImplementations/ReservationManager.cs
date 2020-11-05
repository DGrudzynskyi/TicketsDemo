using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Domain.Interfaces;

namespace TicketsDemo.Domain.DefaultImplementations
{
    public class ReservationManager : IReservationSettings
    {
        public string LogFilePath
        {
            get
            {
                return $@"{AppDomain.CurrentDomain.BaseDirectory}{ConfigurationManager.AppSettings["LogFile"]}";
            }
        }
    }
}