﻿using Garage_2._0.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Garage_2._0.ViewModels
{
    public class VehicleIndex
    {
        public int Id { get; set; }
        [DisplayName("Registration Number")]
        public string RegistrationNumber { get; set; }
        [DisplayName("Type of Vehicle")]
        public Model Model { get; set; }
    }
}