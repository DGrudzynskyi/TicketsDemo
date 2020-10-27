namespace TicketsDemo.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Carriage", "Number", c => c.Int(nullable: false));
            AddColumn("dbo.PlaceInRun", "CarriageNumber", c => c.Int(nullable: false));
            DropColumn("dbo.PlaceInRun", "CarriageId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PlaceInRun", "CarriageId", c => c.Int(nullable: false));
            DropColumn("dbo.PlaceInRun", "CarriageNumber");
            DropColumn("dbo.Carriage", "Number");
        }
    }
}
