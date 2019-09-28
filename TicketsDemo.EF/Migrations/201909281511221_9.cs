namespace TicketsDemo.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _9 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Train", "trainAgency_Id", c => c.Int());
            CreateIndex("dbo.Train", "trainAgency_Id");
            AddForeignKey("dbo.Train", "trainAgency_Id", "dbo.Agency", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Train", "trainAgency_Id", "dbo.Agency");
            DropIndex("dbo.Train", new[] { "trainAgency_Id" });
            DropColumn("dbo.Train", "trainAgency_Id");
        }
    }
}
