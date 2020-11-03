using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsDemo.Domain.Interfaces
{
   public interface ICSVFieldSettingsService
    {
        string CSVRecordIdFieldId { get; }
        string CSVRecordIdFieldNumber { get; }
        string CSVRecordIdFieldStartLocation { get; }
        string CSVRecordIdFieldEndLocation { get; }
        string CSVRecordIdFieldTrainId { get; }
        string CSVRecordIdFieldType { get; }
        string CSVRecordIdFieldDefaultPrice { get; }
        string CSVRecordIdFieldPriceMultiplier { get; }
        string CSVRecordIdFieldCarriageId { get; }
    }
}
