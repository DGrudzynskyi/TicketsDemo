using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TicketsDemo.Data.Entities;
using TicketsDemo.Domain.Interfaces;
using TicketsDemo.EF;
using TicketsDemo.Xml.Interfaces;

namespace TicketsDemo.Xml
{
    public class TrainsImporter : ITrainsImporter
    {
        private ISettingsService _settingsService;

        public TrainsImporter(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        public void EFIntoXMLTrainsImporter()
        {
            using (var ctx = new TicketsContext())
            {
                List<Train> trains = ctx.Trains
                    .Select(c => new
                    {
                        id = c.Id,
                        number = c.Number,
                        slocation = c.StartLocation,
                        elocation = c.EndLocation,
                        carriages = c.Carriages.Select(o => new
                        {
                            id = o.Id,
                            type = o.Type,
                            dprice = o.DefaultPrice,
                            tid = o.TrainId,
                            number = o.Number,
                            places = o.Places.Select(x => new
                            {
                                id = x.Id,
                                number = x.Number,
                                pmultiplier = x.PriceMultiplier,
                                cid = x.CarriageId
                            })
                        })
                    })
                    .AsEnumerable()
                    .Select(an => new Train
                    {
                        // Инициализируем экземпляр Train из анонимного объекта
                        Id = an.id,
                        Number = an.number,
                        StartLocation = an.slocation,
                        EndLocation = an.elocation,
                        Carriages = an.carriages.Select(on => new Carriage
                        {
                            Id = on.id,
                            Type = on.type,
                            DefaultPrice = on.dprice,
                            TrainId = on.tid,
                            Number = on.number,
                            Places = on.places.Select(xn => new Place
                            {
                                Id = xn.id,
                                Number = xn.number,
                                PriceMultiplier = xn.pmultiplier,
                                CarriageId = xn.cid
                            })
                            .ToList()
                        })
                        .ToList()
                    })
                    .ToList();

                XmlSerializer formatter = new XmlSerializer(typeof(List<Train>));

                using (FileStream fs = new FileStream(_settingsService.TrainXMLPath, FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, trains);
                }
            }
        }      
    }
}
