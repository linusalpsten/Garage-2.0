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
            var types = new[] 
            {
                new VehicleType { Type = "Car" },
                new VehicleType { Type = "Buss" },
                new VehicleType { Type = "Motorcycle" }
            };

            var members = new[]
            {
                new Member { FirstName = "Linus", LastName = "Alpsten" },
                new Member { FirstName = "Rolf", LastName = "Lundqvist" }
            };

            context.Types.AddOrUpdate(
                t => t.Type,
                types
                );

            context.Members.AddOrUpdate(
                m => new { m.FirstName, m.LastName },
                members
                );

            context.SaveChanges();

            context.Vehicles.AddOrUpdate(
                p => p.RegistrationNumber,
                new Vehicle { RegistrationNumber = "abc123", Brand = Enums.Brand.Ford, Color = Enums.Color.Black, TypeId = types[0], NumberOfWheels = 4, CheckInTime = DateTime.Now, MemberId = members[0]},
                new Vehicle { RegistrationNumber = "bcd234", Brand = Enums.Brand.Opel, Color = Enums.Color.Blue, TypeId = types[0], NumberOfWheels = 4, CheckInTime = DateTime.Now, MemberId = members[1] },
                new Vehicle { RegistrationNumber = "cde345", Brand = Enums.Brand.Renault, Color = Enums.Color.Green, TypeId = types[0], NumberOfWheels = 4, CheckInTime = DateTime.Now, MemberId = members[1] },
                new Vehicle { RegistrationNumber = "def456", Brand = Enums.Brand.Tesla, Color = Enums.Color.Red, TypeId = types[0], NumberOfWheels = 1, CheckInTime = DateTime.Now, MemberId = members[0] }
                );
        }
    }
}
