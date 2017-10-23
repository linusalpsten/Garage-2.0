namespace Garage_2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_name_parkingtime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vehicles", "CheckInTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Vehicles", "ParkingTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vehicles", "ParkingTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Vehicles", "CheckInTime");
        }
    }
}
