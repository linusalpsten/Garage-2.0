namespace Garage_2._0.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Garage_2._0.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Garage_2._0.DataAccess.VehicleContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Garage_2._0.DataAccess.VehicleContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.Vehicles.AddOrUpdate(
                p => p.RegistrationNumber,
                new Vehicle { RegistrationNumber = "abc123", Brand = Enums.Brand.Ford, Color = Enums.Color.Black, Model = Enums.Model.Car, NumberOfWheels = 4, ParkingTime = DateTime.Now },
                new Vehicle { RegistrationNumber = "bcd234", Brand = Enums.Brand.Opel, Color = Enums.Color.Blue, Model = Enums.Model.Car, NumberOfWheels = 4, ParkingTime = DateTime.Now },
                new Vehicle { RegistrationNumber = "cde345", Brand = Enums.Brand.Renault, Color = Enums.Color.Green, Model = Enums.Model.Car, NumberOfWheels = 4, ParkingTime = DateTime.Now }
                );
        }
    }
}
