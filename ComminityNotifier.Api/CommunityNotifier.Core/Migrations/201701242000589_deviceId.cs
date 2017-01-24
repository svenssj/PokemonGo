namespace CommunityNotifier.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deviceId : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Device");
            AddColumn("dbo.Device", "DeviceId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Device", "RegistrationId", c => c.String());
            AddPrimaryKey("dbo.Device", "DeviceId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Device");
            AlterColumn("dbo.Device", "RegistrationId", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Device", "DeviceId");
            AddPrimaryKey("dbo.Device", "RegistrationId");
        }
    }
}
