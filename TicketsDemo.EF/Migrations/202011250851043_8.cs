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
                        BookingAgenciesId = c.Int(nullable: false),
                        BookingAgenciesCode = c.String(),
                        BookingAgencie_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BookingAgencie", t => t.BookingAgencie_Id, cascadeDelete: true)
                .Index(t => t.BookingAgencie_Id);
            
            CreateTable(
                "dbo.BookingAgencie",
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
            DropForeignKey("dbo.AgencyRepresentative", "BookingAgencie_Id", "dbo.BookingAgencie");
            DropIndex("dbo.AgencyRepresentative", new[] { "BookingAgencie_Id" });
            DropTable("dbo.BookingAgencie");
            DropTable("dbo.AgencyRepresentative");
        }
    }
}
