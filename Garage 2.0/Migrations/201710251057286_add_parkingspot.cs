namespace Garage_2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_parkingspot : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vehicles", "ParkingSpot", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Vehicles", "ParkingSpot");
        }
    }
}
