namespace TicketsDemo.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _10 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BookingService", "BookingAgencyId", "dbo.BookingAgency");
            AddForeignKey("dbo.BookingService", "BookingAgencyId", "dbo.BookingAgency", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookingService", "BookingAgencyId", "dbo.BookingAgency");
            AddForeignKey("dbo.BookingService", "BookingAgencyId", "dbo.BookingAgency", "Id", cascadeDelete: true);
        }
    }
}
