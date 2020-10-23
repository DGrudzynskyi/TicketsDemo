namespace TicketsDemo.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDrinkAndBedInTicket : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ticket", "Drink", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ticket", "Bed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ticket", "Bed");
            DropColumn("dbo.Ticket", "Drink");
        }
    }
}
