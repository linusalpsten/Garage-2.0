using Garage_2._0.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garage_2._0.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string RegistrationNumber { get; set; }
        public Color Color { get; set; }
        public Brand Brand { get; set; }
        public Model Model { get; set; }
        public int NumberOfWheels { get; set; }
    }
}