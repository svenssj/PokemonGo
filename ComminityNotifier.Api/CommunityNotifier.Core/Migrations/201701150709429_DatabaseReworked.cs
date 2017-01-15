namespace CommunityNotifier.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DatabaseReworked : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Areas",
                c => new
                    {
                        AreaId = c.Int(nullable: false, identity: true),
                        AreaName = c.String(),
                    })
                .PrimaryKey(t => t.AreaId);
            
            CreateTable(
                "dbo.Pokemon",
                c => new
                    {
                        PokemonNumber = c.Int(nullable: false, identity: true),
                        PokemonName = c.String(),
                    })
                .PrimaryKey(t => t.PokemonNumber);
            
            AddColumn("dbo.SightingsReport", "Iv", c => c.Int(nullable: false));
            AddColumn("dbo.SightingsReport", "FastMove", c => c.String());
            AddColumn("dbo.SightingsReport", "ChargeMove", c => c.String());
            AddColumn("dbo.SightingsReport", "Area_AreaId", c => c.Int());
            AddColumn("dbo.SightingsReport", "Pokemon_PokemonNumber", c => c.Int());
            CreateIndex("dbo.SightingsReport", "Area_AreaId");
            CreateIndex("dbo.SightingsReport", "Pokemon_PokemonNumber");
            AddForeignKey("dbo.SightingsReport", "Area_AreaId", "dbo.Areas", "AreaId");
            AddForeignKey("dbo.SightingsReport", "Pokemon_PokemonNumber", "dbo.Pokemon", "PokemonNumber");
            DropColumn("dbo.SightingsReport", "Pokemon");
            DropColumn("dbo.SightingsReport", "Area");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SightingsReport", "Area", c => c.Int(nullable: false));
            AddColumn("dbo.SightingsReport", "Pokemon", c => c.Int(nullable: false));
            DropForeignKey("dbo.SightingsReport", "Pokemon_PokemonNumber", "dbo.Pokemon");
            DropForeignKey("dbo.SightingsReport", "Area_AreaId", "dbo.Areas");
            DropIndex("dbo.SightingsReport", new[] { "Pokemon_PokemonNumber" });
            DropIndex("dbo.SightingsReport", new[] { "Area_AreaId" });
            DropColumn("dbo.SightingsReport", "Pokemon_PokemonNumber");
            DropColumn("dbo.SightingsReport", "Area_AreaId");
            DropColumn("dbo.SightingsReport", "ChargeMove");
            DropColumn("dbo.SightingsReport", "FastMove");
            DropColumn("dbo.SightingsReport", "Iv");
            DropTable("dbo.Pokemon");
            DropTable("dbo.Areas");
        }
    }
}
