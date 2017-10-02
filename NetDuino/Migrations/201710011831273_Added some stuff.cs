namespace NetDuino.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedsomestuff : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArduinoModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AuthKey = c.String(),
                        UserID = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.ComponentModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Port = c.Int(nullable: false),
                        ComponentName = c.String(),
                        Value = c.String(),
                        LastUpdated = c.DateTime(nullable: false),
                        ArduinoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ArduinoModels", t => t.ArduinoID, cascadeDelete: true)
                .Index(t => t.ArduinoID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ArduinoModels", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ComponentModels", "ArduinoID", "dbo.ArduinoModels");
            DropIndex("dbo.ComponentModels", new[] { "ArduinoID" });
            DropIndex("dbo.ArduinoModels", new[] { "User_Id" });
            DropTable("dbo.ComponentModels");
            DropTable("dbo.ArduinoModels");
        }
    }
}
