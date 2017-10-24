using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Garage_2._0.DataAccess;
using Garage_2._0.Models;
using Garage_2._0.ViewModels;
using Garage_2._0.Enums;

namespace Garage_2._0.Controllers
{
    public class VehiclesController : Controller
    {
        private VehicleContext db = new VehicleContext();

        // GET: Vehicles
        public ActionResult Index()
        {
            List<Vehicle> vehicles = db.Vehicles.ToList();
            var vehicleItems = vehicles.Select(v => new VehicleIndex
            {
                Id = v.Id,
                RegistrationNumber = v.RegistrationNumber,
                Model = v.Model
            });
            return View(vehicleItems);
        }

        public ActionResult Statistics()
        {
            return View(GetStats());
        }

        private Statistics GetStats()
        {
            var stats = new Statistics();
            var vehicles = db.Vehicles;

            stats.VehiclesOfEachType = vehicles
                .GroupBy(v => v.Model)
                .Select(m => new { Model = m.Key, Count = m.Count() })
                .ToDictionary(v => v.Model, v => v.Count);

            var wheelQuery = vehicles.Select(v => new { v.NumberOfWheels });
            int nrOfWheels = 0;
            foreach (var item in wheelQuery)
            {
                nrOfWheels += item.NumberOfWheels;
            }
            stats.NrOfWheels = nrOfWheels;

            double totalPrice = 0;
            var timeQuery = vehicles.Select(v => new { v.CheckInTime });
            foreach (var item in timeQuery)
            {
                totalPrice += Math.Round(DateTime.Now.Subtract(item.CheckInTime).TotalHours * 75, 2);
            }
            stats.TotalParkingPrice = totalPrice;

            stats.NrOfEachBrand = vehicles
                .GroupBy(v => v.Brand)
                .Select(m => new { Brand = m.Key, Count = m.Count() })
                .ToDictionary(v => v.Brand, v => v.Count);
            return stats;
        }

        public ActionResult Find(string SearchString)
        {
            List<Vehicle> vehicles = db.Vehicles.Where(v => v.RegistrationNumber.Contains(SearchString)).ToList();
            var vehicleItems = vehicles.Select(v => new VehicleIndex
            {
                Id = v.Id,
                RegistrationNumber = v.RegistrationNumber,
                Model = v.Model
            });
            return View("Index", vehicleItems);
        }

        public ActionResult RegNr()
        {
            List<Vehicle> vehicles = db.Vehicles.OrderBy(v => v.RegistrationNumber).ToList();
            var vehicleItems = vehicles.Select(v => new VehicleIndex
            {
                Id = v.Id,
                RegistrationNumber = v.RegistrationNumber,
                Model = v.Model
            });
            return View("Index", vehicleItems);
        }

        public ActionResult Model()
        {
            List<Vehicle> vehicles = db.Vehicles.OrderBy(v => v.Model).ToList();
            var vehicleItems = vehicles.Select(v => new VehicleIndex
            {
                Id = v.Id,
                RegistrationNumber = v.RegistrationNumber,
                Model = v.Model
            });
            return View("Index", vehicleItems);
        }

        // GET: Vehicles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            var vehicleItem = new VehicleDetails {
                Id = vehicle.Id,
                Model = vehicle.Model,
                Brand = vehicle.Brand,
                Color = vehicle.Color,
                CheckInTime = vehicle.CheckInTime,
                RegistrationNumber = vehicle.RegistrationNumber
            };
            return View(vehicleItem);
        }

        // GET: Vehicles/Create
        public ActionResult CheckIn()
        {
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckIn([Bind(Include = "Id,RegistrationNumber,Color,Brand,Model,NumberOfWheels")] Vehicle vehicleData)
        {
            if (ModelState.IsValid)
            {
                var vehicle = new Vehicle
                {
                    Id = vehicleData.Id,
                    RegistrationNumber = vehicleData.RegistrationNumber,
                    Brand = vehicleData.Brand,
                    Color = vehicleData.Color,
                    Model = vehicleData.Model,
                    NumberOfWheels = vehicleData.NumberOfWheels,
                    CheckInTime = DateTime.Now
                };
                db.Vehicles.Add(vehicle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vehicleData);
        }

        // GET: Vehicles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RegistrationNumber,Color,Brand,Model,NumberOfWheels")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                var oldVehicle = db.Vehicles.AsNoTracking().Where(v => v.Id == vehicle.Id).ToList()[0];

                vehicle.CheckInTime = oldVehicle.CheckInTime;
                db.Entry(vehicle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public ActionResult CheckOut(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            var vehicleItem = new VehicleDetails
            {
                Id = vehicle.Id,
                Model = vehicle.Model,
                Brand = vehicle.Brand,
                Color = vehicle.Color,
                CheckInTime = vehicle.CheckInTime,
                RegistrationNumber = vehicle.RegistrationNumber
            };
            return View(vehicleItem);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("CheckOut")]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOutConfirmed(int id, bool receipt)
        {
            Vehicle vehicle = db.Vehicles.Find(id);
            db.Vehicles.Remove(vehicle);
            db.SaveChanges();
            if (receipt)
            {
                //  var kvittoItem = new Kvitto { checkOutTime = DateTime.Now}
                var receiptItem = new Kvitto
                {
                    RegistrationNumber = vehicle.RegistrationNumber,
                    CheckInTime = vehicle.CheckInTime,
                    CheckOutTime = DateTime.Now
                };
                return RedirectToAction("Receipt", receiptItem);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Receipt(Kvitto receipt)
        {
            return View(receipt);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
