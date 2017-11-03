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
using Garage_2._0.Classes;

namespace Garage_2._0.Controllers
{
    public class VehiclesController : Controller
    {
        private static int garageCapacity = 20;
        private Garage garage;

        public VehiclesController() : base()
        {

        }

        private VehicleContext db = new VehicleContext();

        // GET: Vehicles
        public ActionResult Index()
        {
            List<Vehicle> vehicles = db.Vehicles.ToList();
            ViewBag.SpaceLeft = garageCapacity - vehicles.Count;
            var vehicleItems = vehicles.Select(v => new VehicleIndex
            {
                Id = v.Id,
                Owner = v.Member.FullName,
                CheckInTime = v.CheckInTime,
                RegistrationNumber = v.RegistrationNumber,
                Type = v.Type
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
                .GroupBy(v => v.Type)
                .Select(m => new { Type = m.Key, Count = m.Count() })
                .ToDictionary(v => v.Type, v => v.Count);

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
            ViewBag.SpaceLeft = garageCapacity - db.Vehicles.Count();
            List<Vehicle> vehicles = db.Vehicles.Where(v => v.RegistrationNumber.Contains(SearchString)).ToList();
            var vehicleItems = vehicles.Select(v => new VehicleIndex
            {
                Id = v.Id,
                Owner = v.Member.FullName,
                CheckInTime = v.CheckInTime,
                RegistrationNumber = v.RegistrationNumber,
                Type = v.Type
            });
            return View("Index", vehicleItems);
        }

        public ActionResult Filter(int id)
        {
            List<Vehicle> vehicles = db.Vehicles.Where(v => v.MemberId == id).ToList();
            ViewBag.SpaceLeft = garageCapacity - db.Vehicles.Count();
            var vehicleItems = vehicles.Select(v => new VehicleIndex
            {
                Id = v.Id,
                Owner = v.Member.FullName,
                CheckInTime = v.CheckInTime,
                RegistrationNumber = v.RegistrationNumber,
                Type = v.Type
            });
            return View(vehicleItems);
        }

        public ActionResult RegNr()
        {
            ViewBag.SpaceLeft = garageCapacity - db.Vehicles.Count();
            List<Vehicle> vehicles = db.Vehicles.OrderBy(v => v.RegistrationNumber).ToList();
            var vehicleItems = vehicles.Select(v => new VehicleIndex
            {
                Id = v.Id,
                RegistrationNumber = v.RegistrationNumber,
                Type = v.Type
            });
            return View("Index", vehicleItems);
        }

        public ActionResult Model()
        {
            ViewBag.SpaceLeft = garageCapacity - db.Vehicles.Count();
            List<Vehicle> vehicles = db.Vehicles.OrderBy(v => v.Type).ToList();
            var vehicleItems = vehicles.Select(v => new VehicleIndex
            {
                Id = v.Id,
                RegistrationNumber = v.RegistrationNumber,
                Type = v.Type
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
            var vehicleItem = new VehicleDetails
            {
                Id = vehicle.Id,
                Type = vehicle.Type,
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
            var viewModel = new CheckInVM();
            viewModel.Types = db.Types.ToList();
            viewModel.Members = db.Members.ToList();
            return View(viewModel);
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckIn([Bind(Include = "Id,RegistrationNumber,Color,Brand,TypeId,MemberId,NumberOfWheels")] Vehicle vehicleData)
        {
            if (db.Vehicles.ToList().Count < garageCapacity)
            {
                
                if (ModelState.IsValid)
                {
                    var vehicle = new Vehicle
                    {
                        Id = vehicleData.Id,
                        RegistrationNumber = vehicleData.RegistrationNumber,
                        Brand = vehicleData.Brand,
                        Color = vehicleData.Color,
                        TypeId = vehicleData.TypeId,
                        MemberId = vehicleData.MemberId,
                        NumberOfWheels = vehicleData.NumberOfWheels,
                        CheckInTime = DateTime.Now
                    };
                    db.Vehicles.Add(vehicle);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ViewBag.IsFull = "true";
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
            var viewModel = new CheckInVM()
            {
                Id = vehicle.Id,
                RegistrationNumber = vehicle.RegistrationNumber,
                Color = vehicle.Color,
                Brand = vehicle.Brand,
                NumberOfWheels = vehicle.NumberOfWheels,
                Types = db.Types.ToList(),
                Members = db.Members.ToList()
            };
            return View(viewModel);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RegistrationNumber,Color,Brand,TypeId,MemberId,NumberOfWheels")] Vehicle vehicleData)
        {
            if (ModelState.IsValid)
            {
                var oldVehicle = db.Vehicles.AsNoTracking().Where(v => v.Id == vehicleData.Id).ToList()[0];

                var vehicle = new Vehicle
                {
                    Id = vehicleData.Id,
                    RegistrationNumber = vehicleData.RegistrationNumber,
                    Brand = vehicleData.Brand,
                    Color = vehicleData.Color,
                    TypeId = vehicleData.TypeId,
                    MemberId = vehicleData.MemberId,
                    NumberOfWheels = vehicleData.NumberOfWheels,
                    CheckInTime = oldVehicle.CheckInTime
                };
                db.Entry(vehicle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vehicleData);
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
                Type = vehicle.Type,
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
