namespace NetDuino.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intisnowstring : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ArduinoModels", new[] { "User_Id" });
            DropColumn("dbo.ArduinoModels", "UserID");
            RenameColumn(table: "dbo.ArduinoModels", name: "User_Id", newName: "UserId");
            AlterColumn("dbo.ArduinoModels", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.ArduinoModels", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ArduinoModels", new[] { "UserId" });
            AlterColumn("dbo.ArduinoModels", "UserId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.ArduinoModels", name: "UserId", newName: "User_Id");
            AddColumn("dbo.ArduinoModels", "UserID", c => c.Int(nullable: false));
            CreateIndex("dbo.ArduinoModels", "User_Id");
        }
    }
}
