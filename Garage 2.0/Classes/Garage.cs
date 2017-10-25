using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Garage_2._0.Models;
using Garage_2._0.Enums;

namespace Garage_2._0.Classes
{
    public class Garage
    {
        private List<ParkingSpace> parkingSpaces;
        public List<ParkingSpace> ParkingSpaces { get { return parkingSpaces; } }
        private int capacity;
        private Dictionary<Model, int> SizeDictionary = new Dictionary<Model, int>();
        public Garage(int cap)
        {
            capacity = cap;
            parkingSpaces = new List<ParkingSpace>();
            for (int i = 0; i < cap; i++)
            {
                parkingSpaces.Add(new ParkingSpace());
            }
            SizeDictionary[Model.Car] = 3;
            SizeDictionary[Model.Buss] = 7;
            SizeDictionary[Model.Motorcycle] = 1;
        }

        public bool ParkVehicle(Vehicle vehicle)
        {
            int size = SizeDictionary[vehicle.Model];
            int parkingSpaceNeeded = getSpaceNeeded(size);

            int index;
            if (size <= 3)
            {
                index = parkingSpaces.FindIndex(x => x.SpaceLeft >= size);
            }
            else
            {
                index = findspace(parkingSpaceNeeded);
            }
            if (index == -1)
            {
                return false;
            }
            for (int i = 0; i < parkingSpaceNeeded; i++)
            {
                if (size > 3)
                {
                    size -= 3;
                    parkingSpaces[index + i].ParkVehicle(vehicle, 3);
                }
                else
                {
                    parkingSpaces[index + i].ParkVehicle(vehicle, size);
                }

            }
            return true;
        }

        public bool ParkVehicleAt(Vehicle vehicle, int position)
        {
            int size = SizeDictionary[vehicle.Model];
            int parkingSpacesNeeded = getSpaceNeeded(size);
            if (position + parkingSpacesNeeded > capacity)
            {
                return false;
            }
            for (int i = 0; i < parkingSpacesNeeded; i++)
            {
                if (parkingSpaces[position + i].SpaceLeft == 0)
                {
                    return false;
                }
            }
            for (int i = 0; i < parkingSpacesNeeded; i++)
            {
                if (size > 3)
                {
                    size -= 3;
                    parkingSpaces[position + i].ParkVehicle(vehicle, 3);
                }
                else
                {
                    parkingSpaces[position + i].ParkVehicle(vehicle, size);
                }
            }
            return true;
        }

        private int findspace(int size)
        {
            Predicate<ParkingSpace> test = x => x.SpaceLeft == 3;
            int index = parkingSpaces.FindIndex(test);
            bool foundSpaces;
            while (index != -1)
            {
                if (parkingSpaces.Count >= index + 3)
                {
                    foundSpaces = true;
                    for (int i = 0; i < size; i++)
                    {
                        if (!test(parkingSpaces[index + i]))
                        {
                            foundSpaces = false;
                        }
                    }
                    if (foundSpaces)
                    {
                        return index;
                    }
                    else
                    {
                        index = parkingSpaces.FindIndex(index + 1, test);
                    }
                }
                else
                {
                    return -1;
                }
            }
            return -1;
        }

        private int getSpaceNeeded(int size)
        {
            var a = size / 3;
            if (size % 3 != 0)
            {
                a += 1;
            }
            return a;
        }
    }
}