namespace NetDuino.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sparklingtwinklepowder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Components", "PositionX", c => c.Int(nullable: false));
            AddColumn("dbo.Components", "PositionY", c => c.Int(nullable: false));
            AddColumn("dbo.Components", "Width", c => c.Int(nullable: false));
            AddColumn("dbo.Components", "Height", c => c.Int(nullable: false));
            AddColumn("dbo.Components", "IsToggle", c => c.Boolean());
            AddColumn("dbo.Components", "ToggleValue", c => c.Int());
            AddColumn("dbo.Components", "LabelValue", c => c.String());
            AddColumn("dbo.Components", "SliderValue", c => c.Int());
            DropColumn("dbo.Components", "Value");
            DropColumn("dbo.Components", "IsToggleable");
            DropColumn("dbo.Components", "Toggle");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Components", "Toggle", c => c.Boolean());
            AddColumn("dbo.Components", "IsToggleable", c => c.Boolean());
            AddColumn("dbo.Components", "Value", c => c.String());
            DropColumn("dbo.Components", "SliderValue");
            DropColumn("dbo.Components", "LabelValue");
            DropColumn("dbo.Components", "ToggleValue");
            DropColumn("dbo.Components", "IsToggle");
            DropColumn("dbo.Components", "Height");
            DropColumn("dbo.Components", "Width");
            DropColumn("dbo.Components", "PositionY");
            DropColumn("dbo.Components", "PositionX");
        }
    }
}
