namespace TicketsDemo.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBookingAgencies : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AgencyRepresentatives",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BookingAgenciesId = c.Int(nullable: false),
                        BookingAgenciesCode = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BookingAgencies", t => t.BookingAgenciesId, cascadeDelete: true)
                .Index(t => t.BookingAgenciesId);
            
            CreateTable(
                "dbo.BookingAgencies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Markup = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AgencyRepresentatives", "BookingAgenciesId", "dbo.BookingAgencies");
            DropIndex("dbo.AgencyRepresentatives", new[] { "BookingAgenciesId" });
            DropTable("dbo.BookingAgencies");
            DropTable("dbo.AgencyRepresentatives");
        }
    }
}
