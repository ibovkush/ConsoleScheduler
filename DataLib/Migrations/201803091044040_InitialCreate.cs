namespace DataLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FuelPrices",
                c => new
                    {
                        FuelPriceId = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 10, scale: 3),
                        Date = c.String(),
                    })
                .PrimaryKey(t => t.FuelPriceId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FuelPrices");
        }
    }
}
