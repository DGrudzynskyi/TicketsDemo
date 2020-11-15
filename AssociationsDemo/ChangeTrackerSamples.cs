using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.EF;

namespace AssociationsDemo
{
    public static class ChangeTrackerSamples
    {
        public static void CheckEntries()
        {
            using (var ctx = new TicketsContext())
            {
                var trains = ctx.Trains.AsNoTracking().ToList(); // both detached because of AsNoTracking
                ctx.Trains.Add(trains[0]);

                trains[1].EndLocation = "khmelnitskiy";
                ctx.Trains.Attach(trains[1]);

                var train1Entry = ctx.Entry(trains[0]); // added 
                var train2Entry = ctx.Entry(trains[1]); // unchanged

                train2Entry.State = System.Data.Entity.EntityState.Modified;

                ctx.SaveChanges();
            }
        }

        public static void ChangeAndRevertTrainNumberSample() {
            using (var ctx = new TicketsContext())
            {
                var trains = ctx.Trains.ToList();
                var originalTrainNumber = trains[0].Number;
                var firstTrainEntry = ctx.Entry(trains[0]);

                Debug.WriteLine("Current state of first train is {0}", firstTrainEntry.State);

                // change train number to some random value
                trains[0].Number = 200;

                Debug.WriteLine("State of first train after changing it's number {0}", firstTrainEntry.State);
                Debug.WriteLine("State of number property of first train train after changing it's number {0}", firstTrainEntry.Property(x => x.Number).IsModified);

                // by calling this line, context detects the change to trains[0] object
                // this might be replaced to any of the functions which implicitly call change detection (such as retereiving/saving anything from/to context)
                //ctx.ChangeTracker.DetectChanges(); 
                //ctx.Carriages.Find(1);

                // change train number back to some another value. 
                // Note: even if entity state was detected as 'changed' it is then never become 'unchanged'
                trains[0].Number = originalTrainNumber;

                Debug.WriteLine("State of first train after reverting number is {0}", firstTrainEntry.State);
                Debug.WriteLine("State of number property after revertings train's number {0}", firstTrainEntry.Property(x => x.Number).IsModified);

                ctx.SaveChanges();
            }
        }
    }
}
