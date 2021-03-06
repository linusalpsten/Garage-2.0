﻿using Garage_2._0.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Garage_2._0.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"[a-zA-Z]{3}\d{3}|[a-zA-Z]{1,8}", ErrorMessage = "Registration number should have 3 letters followed by 3 numbers or 1 to 8 letters")]
        [DisplayName("Registration Number")]
        public string RegistrationNumber { get; set; }
        public Color Color { get; set; }
        public Brand Brand { get; set; }
        //public Model Model { get; set; }
        [Range(1,20,ErrorMessage = "The number of wheels should be between 1 and 20")]
        [DisplayName("Number of Wheels")]
        public int NumberOfWheels { get; set; }
        public DateTime CheckInTime { get; set; }
        public int ParkingSpot { get; set; }
        public int MemberId { get; set; }
        public int TypeId { get; set; }

        public virtual VehicleType Type { get; set; }
        public virtual Member Member { get; set; }
    }
}