namespace TicketsDemo.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _8 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Agent",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CompanyId = c.String(),
                        Percent = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Agent");
        }
    }
}
