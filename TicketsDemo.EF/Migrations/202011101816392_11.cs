namespace TicketsDemo.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Holiday");
            AlterColumn("dbo.Holiday", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Holiday", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Holiday");
            AlterColumn("dbo.Holiday", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Holiday", "Id");
        }
    }
}
