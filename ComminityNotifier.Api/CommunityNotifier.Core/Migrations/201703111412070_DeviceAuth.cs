namespace CommunityNotifier.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeviceAuth : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SightingsReport", "DeviceId", c => c.String());
            AddColumn("dbo.Device", "Disabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.Device", "DisabledDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Device", "DisabledDate");
            DropColumn("dbo.Device", "Disabled");
            DropColumn("dbo.SightingsReport", "DeviceId");
        }
    }
}
