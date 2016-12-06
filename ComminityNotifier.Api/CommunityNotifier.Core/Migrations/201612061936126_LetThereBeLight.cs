namespace CommunityNotifier.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LetThereBeLight : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SightingsReport",
                c => new
                    {
                        SightingsId = c.Guid(nullable: false),
                        PokemonNumber = c.Int(nullable: false),
                        Area = c.Int(nullable: false),
                        Locaiton = c.String(),
                        ReportTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SightingsId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SightingsReport");
        }
    }
}
