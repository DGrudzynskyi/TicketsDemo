
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities.BookingAggregate;

namespace TicketsDemo.Data.Repositories
{
    public interface IRepresentativeRepository
    {
        AgencyRepresentative GetRepresentative(string code);
        void CreateRepresentative(AgencyRepresentative agencyRepresentative);
        void UpdateRepresentative(AgencyRepresentative agencyRepresentative);
        void DeleteRepresentative(AgencyRepresentative agencyRepresentative);
    }
}