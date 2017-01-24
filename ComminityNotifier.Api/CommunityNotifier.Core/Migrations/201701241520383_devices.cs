namespace CommunityNotifier.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class devices : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Device",
                c => new
                    {
                        RegistrationId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.RegistrationId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Device");
        }
    }
}
