namespace Garage_2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_regnr_requirements : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Vehicles", "RegistrationNumber", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Vehicles", "RegistrationNumber", c => c.String());
        }
    }
}
