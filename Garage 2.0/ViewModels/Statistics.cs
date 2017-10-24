using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Garage_2._0.Enums;

namespace Garage_2._0.ViewModels
{
    public class Statistics
    {
        public Dictionary<Model, int> VehiclesOfEachType { get; set; }
        public int NrOfWheels { get; set; }
        public double TotalParkingPrice { get; set; }
        public Dictionary<Brand, int> NrOfEachBrand { get; set; }
    }
}