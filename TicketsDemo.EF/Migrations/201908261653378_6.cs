namespace TicketsDemo.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlaceInRun", "CarriageId", c => c.Int(nullable: false));
            AddColumn("dbo.Reservation", "PlaceInRunId", c => c.Int(nullable: false));
            DropColumn("dbo.Reservation", "PlaceNumber");
            DropColumn("dbo.Reservation", "RunId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reservation", "RunId", c => c.Int(nullable: false));
            AddColumn("dbo.Reservation", "PlaceNumber", c => c.Int(nullable: false));
            DropColumn("dbo.Reservation", "PlaceInRunId");
            DropColumn("dbo.PlaceInRun", "CarriageId");
        }
    }
}
