namespace NetDuino.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asd123 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ComponentModels", newName: "Components");
            AddColumn("dbo.Components", "IsToggleable", c => c.Boolean());
            AddColumn("dbo.Components", "Toggle", c => c.Boolean());
            AddColumn("dbo.Components", "MaxValue", c => c.Int());
            AddColumn("dbo.Components", "MinValue", c => c.Int());
            AddColumn("dbo.Components", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Components", "Discriminator");
            DropColumn("dbo.Components", "MinValue");
            DropColumn("dbo.Components", "MaxValue");
            DropColumn("dbo.Components", "Toggle");
            DropColumn("dbo.Components", "IsToggleable");
            RenameTable(name: "dbo.Components", newName: "ComponentModels");
        }
    }
}
