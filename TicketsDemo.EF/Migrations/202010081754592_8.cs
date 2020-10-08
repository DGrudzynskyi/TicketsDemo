namespace TicketsDemo.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _8 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookingAgency",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        FareCoef = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BookingService",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BookingAgencyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BookingAgency", t => t.BookingAgencyId, cascadeDelete: true)
                .Index(t => t.BookingAgencyId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookingService", "BookingAgencyId", "dbo.BookingAgency");
            DropIndex("dbo.BookingService", new[] { "BookingAgencyId" });
            DropTable("dbo.BookingService");
            DropTable("dbo.BookingAgency");
        }
    }
}
