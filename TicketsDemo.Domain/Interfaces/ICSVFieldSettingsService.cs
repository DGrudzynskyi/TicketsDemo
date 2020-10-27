using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsDemo.Domain.Interfaces
{
   public interface ICSVFieldSettingsService
    {
        string Id { get; }
        string Number { get; }
        string StartLocation { get; }
        string EndLocation { get; }
        string TrainId { get; }
        string Type { get; }
        string DefaultPrice { get; }
        string PriceMultiplier { get; }
        string CarriageId { get; }
    }
}
