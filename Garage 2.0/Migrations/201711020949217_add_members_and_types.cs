namespace Garage_2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_members_and_types : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VehicleTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Vehicles", "MemberId", c => c.Int(nullable: false));
            AddColumn("dbo.Vehicles", "TypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Vehicles", "MemberId");
            CreateIndex("dbo.Vehicles", "TypeId");
            AddForeignKey("dbo.Vehicles", "MemberId", "dbo.Members", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Vehicles", "TypeId", "dbo.VehicleTypes", "Id", cascadeDelete: true);
            DropColumn("dbo.Vehicles", "Model");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vehicles", "Model", c => c.Int(nullable: false));
            DropForeignKey("dbo.Vehicles", "TypeId", "dbo.VehicleTypes");
            DropForeignKey("dbo.Vehicles", "MemberId", "dbo.Members");
            DropIndex("dbo.Vehicles", new[] { "TypeId" });
            DropIndex("dbo.Vehicles", new[] { "MemberId" });
            DropColumn("dbo.Vehicles", "TypeId");
            DropColumn("dbo.Vehicles", "MemberId");
            DropTable("dbo.VehicleTypes");
            DropTable("dbo.Members");
        }
    }
}
