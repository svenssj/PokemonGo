namespace CommunityNotifier.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class notificationfilters : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DeviceAreaFilter",
                c => new
                    {
                        Key = c.Int(nullable: false, identity: true),
                        Area_AreaId = c.Int(),
                        Device_DeviceId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Key)
                .ForeignKey("dbo.Areas", t => t.Area_AreaId)
                .ForeignKey("dbo.Device", t => t.Device_DeviceId)
                .Index(t => t.Area_AreaId)
                .Index(t => t.Device_DeviceId);
            
            CreateTable(
                "dbo.DevicePokemonFilter",
                c => new
                    {
                        Key = c.Int(nullable: false, identity: true),
                        Device_DeviceId = c.String(maxLength: 128),
                        Pokemon_PokemonNumber = c.Int(),
                    })
                .PrimaryKey(t => t.Key)
                .ForeignKey("dbo.Device", t => t.Device_DeviceId)
                .ForeignKey("dbo.Pokemon", t => t.Pokemon_PokemonNumber)
                .Index(t => t.Device_DeviceId)
                .Index(t => t.Pokemon_PokemonNumber);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DevicePokemonFilter", "Pokemon_PokemonNumber", "dbo.Pokemon");
            DropForeignKey("dbo.DevicePokemonFilter", "Device_DeviceId", "dbo.Device");
            DropForeignKey("dbo.DeviceAreaFilter", "Device_DeviceId", "dbo.Device");
            DropForeignKey("dbo.DeviceAreaFilter", "Area_AreaId", "dbo.Areas");
            DropIndex("dbo.DevicePokemonFilter", new[] { "Pokemon_PokemonNumber" });
            DropIndex("dbo.DevicePokemonFilter", new[] { "Device_DeviceId" });
            DropIndex("dbo.DeviceAreaFilter", new[] { "Device_DeviceId" });
            DropIndex("dbo.DeviceAreaFilter", new[] { "Area_AreaId" });
            DropTable("dbo.DevicePokemonFilter");
            DropTable("dbo.DeviceAreaFilter");
        }
    }
}
