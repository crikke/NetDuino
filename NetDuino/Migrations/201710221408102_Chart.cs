namespace NetDuino.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Chart : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChartTicks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Time = c.DateTime(nullable: false),
                        Value = c.Double(nullable: false),
                        ChartId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Components", t => t.ChartId, cascadeDelete: true)
                .Index(t => t.ChartId);
            
            AddColumn("dbo.Components", "YLabel", c => c.String());
            AddColumn("dbo.Components", "XLabel", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChartTicks", "ChartId", "dbo.Components");
            DropIndex("dbo.ChartTicks", new[] { "ChartId" });
            DropColumn("dbo.Components", "XLabel");
            DropColumn("dbo.Components", "YLabel");
            DropTable("dbo.ChartTicks");
        }
    }
}
