namespace CommunityNotifier.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PokemonEnum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SightingsReport", "Pokemon", c => c.Int(nullable: false));
            DropColumn("dbo.SightingsReport", "PokemonNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SightingsReport", "PokemonNumber", c => c.Int(nullable: false));
            DropColumn("dbo.SightingsReport", "Pokemon");
        }
    }
}
