using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.EF;

namespace AssociationsDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //OneToManyAddReference.UsingAssotiation();
            //OneToManyAddReference.UsingNavigationKey();
            //OneToManyAddReference.ByAddingToCollectionLazyLoading();
            //OneToManyAddReference.ByAddingToCollectionEagerLoading();

            //AccessNavigationPropertiesInLoop.ListPlacesForAllTrainsLazyLoading();
            // AccessNavigationPropertiesInLoop.ListPlacesForAllTrainsEagerLoading();

            //AccessNavigationPropertiesInLoop.ListPlacesForAllTrainsLazyLostContext();
            ChangeTrackerSamples.CheckEntries();

            //SelectSpecializedDataSet.GetAllReservationsWithTicketsForTrain(1, DateTime.Now.AddDays(-7), DateTime.Now);
        }
    }
}
