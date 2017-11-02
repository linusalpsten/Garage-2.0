using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Garage_2._0.Models;

namespace Garage_2._0.DataAccess
{
    public class VehicleContext : DbContext
    {
        public VehicleContext() : base("VehicleConnection")
        {

        }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleType> Types { get; set; }
        public DbSet<Member> Members { get; set; }
    }
}