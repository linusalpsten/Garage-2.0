﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Garage_2._0.Enums;
using System.ComponentModel;
using Garage_2._0.Models;

namespace Garage_2._0.ViewModels
{
    public class Statistics
    {
        public Dictionary<VehicleType, int> VehiclesOfEachType { get; set; }
        [DisplayName("Total Number of Wheels")]
        public int NrOfWheels { get; set; }
        [DisplayName("Total Parking Price")]
        public double TotalParkingPrice { get; set; }
        public Dictionary<Brand, int> NrOfEachBrand { get; set; }
    }
}