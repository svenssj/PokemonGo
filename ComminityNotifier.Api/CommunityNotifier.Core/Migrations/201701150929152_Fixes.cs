namespace CommunityNotifier.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fixes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SightingsReport", "Area_AreaId", "dbo.Areas");
            DropForeignKey("dbo.SightingsReport", "Pokemon_PokemonNumber", "dbo.Pokemon");
            DropIndex("dbo.SightingsReport", new[] { "Area_AreaId" });
            DropIndex("dbo.SightingsReport", new[] { "Pokemon_PokemonNumber" });
            RenameColumn(table: "dbo.SightingsReport", name: "Area_AreaId", newName: "AreaId");
            RenameColumn(table: "dbo.SightingsReport", name: "Pokemon_PokemonNumber", newName: "PokemonNumber");
            AlterColumn("dbo.SightingsReport", "AreaId", c => c.Int(nullable: false));
            AlterColumn("dbo.SightingsReport", "PokemonNumber", c => c.Int(nullable: false));
            CreateIndex("dbo.SightingsReport", "PokemonNumber");
            CreateIndex("dbo.SightingsReport", "AreaId");
            AddForeignKey("dbo.SightingsReport", "AreaId", "dbo.Areas", "AreaId", cascadeDelete: true);
            AddForeignKey("dbo.SightingsReport", "PokemonNumber", "dbo.Pokemon", "PokemonNumber", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SightingsReport", "PokemonNumber", "dbo.Pokemon");
            DropForeignKey("dbo.SightingsReport", "AreaId", "dbo.Areas");
            DropIndex("dbo.SightingsReport", new[] { "AreaId" });
            DropIndex("dbo.SightingsReport", new[] { "PokemonNumber" });
            AlterColumn("dbo.SightingsReport", "PokemonNumber", c => c.Int());
            AlterColumn("dbo.SightingsReport", "AreaId", c => c.Int());
            RenameColumn(table: "dbo.SightingsReport", name: "PokemonNumber", newName: "Pokemon_PokemonNumber");
            RenameColumn(table: "dbo.SightingsReport", name: "AreaId", newName: "Area_AreaId");
            CreateIndex("dbo.SightingsReport", "Pokemon_PokemonNumber");
            CreateIndex("dbo.SightingsReport", "Area_AreaId");
            AddForeignKey("dbo.SightingsReport", "Pokemon_PokemonNumber", "dbo.Pokemon", "PokemonNumber");
            AddForeignKey("dbo.SightingsReport", "Area_AreaId", "dbo.Areas", "AreaId");
        }
    }
}
