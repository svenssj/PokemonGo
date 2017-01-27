namespace CommunityNotifier.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class merge : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Device",
                c => new
                    {
                        DeviceId = c.String(nullable: false, maxLength: 128),
                        RegistrationId = c.String(),
                    })
                .PrimaryKey(t => t.DeviceId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Device");
        }
    }
}
