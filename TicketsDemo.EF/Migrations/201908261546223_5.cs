namespace TicketsDemo.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PlaceInRun", "PlaceId", "dbo.Place");
            DropForeignKey("dbo.Reservation", "PlaceInRunId", "dbo.PlaceInRun");
            DropForeignKey("dbo.Ticket", "Id", "dbo.Reservation");
            DropForeignKey("dbo.Run", "TrainId", "dbo.Train");
            DropForeignKey("dbo.PriceComponent", "TicketId", "dbo.Ticket");
            DropIndex("dbo.PlaceInRun", new[] { "PlaceId" });
            DropIndex("dbo.Reservation", new[] { "PlaceInRunId" });
            DropIndex("dbo.Ticket", new[] { "Id" });
            DropIndex("dbo.Run", new[] { "TrainId" });
            DropPrimaryKey("dbo.Ticket");
            AddColumn("dbo.PlaceInRun", "Number", c => c.Int(nullable: false));
            AddColumn("dbo.Reservation", "PlaceNumber", c => c.Int(nullable: false));
            AddColumn("dbo.Reservation", "RunId", c => c.Int(nullable: false));
            AlterColumn("dbo.Ticket", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Ticket", "Id");
            AddForeignKey("dbo.PriceComponent", "TicketId", "dbo.Ticket", "Id", cascadeDelete: true);
            DropColumn("dbo.PlaceInRun", "PlaceId");
            DropColumn("dbo.Reservation", "PlaceInRunId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reservation", "PlaceInRunId", c => c.Int(nullable: false));
            AddColumn("dbo.PlaceInRun", "PlaceId", c => c.Int(nullable: false));
            DropForeignKey("dbo.PriceComponent", "TicketId", "dbo.Ticket");
            DropPrimaryKey("dbo.Ticket");
            AlterColumn("dbo.Ticket", "Id", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Reservation", "RunId");
            DropColumn("dbo.Reservation", "PlaceNumber");
            DropColumn("dbo.PlaceInRun", "Number");
            AddPrimaryKey("dbo.Ticket", "Id");
            CreateIndex("dbo.Run", "TrainId");
            CreateIndex("dbo.Ticket", "Id");
            CreateIndex("dbo.Reservation", "PlaceInRunId");
            CreateIndex("dbo.PlaceInRun", "PlaceId");
            AddForeignKey("dbo.PriceComponent", "TicketId", "dbo.Ticket", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Run", "TrainId", "dbo.Train", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Ticket", "Id", "dbo.Reservation", "Id");
            AddForeignKey("dbo.Reservation", "PlaceInRunId", "dbo.PlaceInRun", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PlaceInRun", "PlaceId", "dbo.Place", "Id", cascadeDelete: true);
        }
    }
}
