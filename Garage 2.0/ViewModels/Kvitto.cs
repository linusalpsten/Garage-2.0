﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garage_2._0.ViewModels
{
    public class Kvitto
    {
        public string RegistrationNumber { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime CheckOutTime { get; set; }
        public TimeSpan ParkeringsPeriod { get { return CheckOutTime.Subtract(CheckInTime); } }
        public double TotalPrice { get { return Math.Round(ParkeringsPeriod.TotalHours * 75,2); } }
    }
}