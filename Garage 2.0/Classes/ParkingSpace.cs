using Garage_2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garage_2._0.Classes
{
    public class ParkingSpace
    {
        private int spaceLeft;
        public int SpaceLeft { get { return spaceLeft; } }
        private List<Vehicle> parkedVehicles = new List<Vehicle>();
        public List<Vehicle> ParkedVehicles { get { return parkedVehicles; } }
        public ParkingSpace()
        {
            spaceLeft = 3;
        }
        public void ParkVehicle(Vehicle vehicle, int space)
        {
            spaceLeft -= space;
            parkedVehicles.Add(vehicle);
        }
        public void UnParkVehicle(Vehicle vehicle, int space)
        {
            spaceLeft += space;
            parkedVehicles.Remove(vehicle);
        }
    }
}