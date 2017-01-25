namespace CommunityNotifier.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Nests : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NestReport",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Pokemon_PokemonNumber = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pokemon", t => t.Pokemon_PokemonNumber)
                .Index(t => t.Pokemon_PokemonNumber);
            
            CreateTable(
                "dbo.Location",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Spot = c.String(),
                        LocationTimeStamp = c.DateTime(nullable: false),
                        Area_AreaId = c.Int(),
                        NestReport_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Areas", t => t.Area_AreaId)
                .ForeignKey("dbo.NestReport", t => t.NestReport_Id)
                .Index(t => t.Area_AreaId)
                .Index(t => t.NestReport_Id);
            
            DropTable("dbo.Device");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Device",
                c => new
                    {
                        DeviceId = c.String(nullable: false, maxLength: 128),
                        RegistrationId = c.String(),
                    })
                .PrimaryKey(t => t.DeviceId);
            
            DropForeignKey("dbo.NestReport", "Pokemon_PokemonNumber", "dbo.Pokemon");
            DropForeignKey("dbo.Location", "NestReport_Id", "dbo.NestReport");
            DropForeignKey("dbo.Location", "Area_AreaId", "dbo.Areas");
            DropIndex("dbo.Location", new[] { "NestReport_Id" });
            DropIndex("dbo.Location", new[] { "Area_AreaId" });
            DropIndex("dbo.NestReport", new[] { "Pokemon_PokemonNumber" });
            DropTable("dbo.Location");
            DropTable("dbo.NestReport");
        }
    }
}
