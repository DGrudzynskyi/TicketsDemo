namespace TicketsDemo.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _8 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AgencyRepresentative",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BookingAgencyId = c.Int(nullable: false),
                        BookingAgencyCode = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BookingAgency", t => t.BookingAgencyId, cascadeDelete: false)
                .Index(t => t.BookingAgencyId);
            
            CreateTable(
                "dbo.BookingAgency",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Markup = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AgencyRepresentative", "BookingAgencyId", "dbo.BookingAgency");
            DropIndex("dbo.AgencyRepresentative", new[] { "BookingAgencyId" });
            DropTable("dbo.BookingAgency");
            DropTable("dbo.AgencyRepresentative");
        }
    }
}
