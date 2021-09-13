using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GarageThree.Data;
using GarageThree.Models;
using GarageThree.ViewModels;

namespace GarageThree.Controllers
{
    public class ParkController : Controller
    {
        private readonly GarageContext _context;

        public ParkController(GarageContext context)
        {
            _context = context;
        }

        // GET: Park
        public async Task<IActionResult> Index()
        {
            var garageContext = _context.Vehicles.Include(v => v.VehicleType);
            return View(await garageContext.ToListAsync());
        }

        public async Task<IActionResult> ParkinSpots()
        {
            var garageContext = _context.ParkingSpots.Include(p => p.Vehicles);
            return View(await garageContext.ToListAsync());
        }

        public async Task<IActionResult> Collect()
        {
            var garageContext = _context.Vehicles.Include(v => v.VehicleType).Include(ps => ps.ParkingSpots);
            return View(nameof(Collect), await garageContext.ToListAsync());
        }

        public async Task<IActionResult> FilterCollect(string RegistrationNumber = null)
        {
            //Todo if RegistrationNumber = null do nothing
            if (!String.IsNullOrWhiteSpace(RegistrationNumber))
            { 
                var model = _context.Vehicles.Include(v => v.VehicleType)
                .Include(ps => ps.ParkingSpots)
                .Where(v=>v.RegistrationNumber.Contains(RegistrationNumber));
        
                return View(nameof(Collect), await model.ToListAsync());
            }

            TempData["Empty"] = "Registration number could not be found"; //BUGGY
            //if (model.Count() == 0)
            //{
            //    TempData["Empty"] = "Registration number could not be found";
            //    Console.WriteLine("funkar");
            //}
            //model = RegistrationNumber == null ? model : model.Where(v=>v.RegistrationNumber=="helo"); Not allowed

            //Todo Dropdown logic.

            return RedirectToAction(nameof(Collect));
        }

        public async Task<IActionResult> SignUp()
        {
            return View();
        }



        // GET: Park/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles
                .Include(v => v.VehicleType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // GET: Park/Create
        public IActionResult Park()
        {
            ViewData["VehicleTypeId"] = new SelectList(_context.VehicleTypes, "Id", "Id");
            ViewData["OwnerId"] = new SelectList(_context.Owners, "Id", "Id"); // Add with ViewData to Allow for Create Vehicle.
            return View();
        }

        // POST: Park/Park
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Park([Bind("Id,OwnerId,RegistrationNumber,Passengers,Color,Wheels,VehicleTypeId")] Vehicle vehicle)
        {

            

            if (ModelState.IsValid)
            {
                
                var ownerId = _context.Owners.Where(o => o.Id == vehicle.OwnerId);
                if (!ownerId.Any()) // Select(v=>v).ToList(). TO DO VALIDATE FURTHER
                {
                    // TODO SWAP TO DEPENDECY INJECTION-
                    ModelState.AddModelError("OwnerId", "New to the Garage? Sign up here");
                    return View(vehicle);
                }
                if (_context.Vehicles.Where(v=>v.RegistrationNumber.Contains(vehicle.RegistrationNumber)).Count()>0) // Select(v=>v).ToList().
                {
                    // TODO SWAP TO DEPENDECY INJECTION-
                    ModelState.AddModelError("RegistrationNumber", "Registration number must be unique");
                    return View(vehicle);
                }
                var ageVehicleAbove18 = _context.Owners
                    .Include(o => o.Vehicles.Where(v => v.Id == vehicle.Id)) // Query Vehicle
                    .FirstOrDefault().Age >= 18; //Query Owner


                if (!ageVehicleAbove18) 
                {
                    ModelState.AddModelError("Age", "You are too young to park that Vehicle here, Find an older member to help you");
                    return View(vehicle);
                }


                //WORK IN PROGRESS
                var availibleParks = _context.ParkingSpots.FirstOrDefault(); // ToDo validate and Limit
                vehicle.ParkingSpots.Add(availibleParks);
                vehicle.ArrivalTime = DateTime.Now;

                _context.Vehicles.Add(vehicle);
                TempData["Success"] = $"Vehicle {vehicle.RegistrationNumber} succesfully parked at Parking: {vehicle.ParkingSpots.FirstOrDefault().ParkingId}";
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // TODO SWAP TO DEPENDECY INJECTION-
            ViewData["VehicleTypeId"] = new SelectList(_context.VehicleTypes, "Id", "Id", vehicle.VehicleTypeId);
            ViewData["OwnerId"] = new SelectList(_context.Owners, "Id", "Id", vehicle.OwnerId); // Id Value, Text
            return View(vehicle);
        }

        // GET: Park/Edit/5
        public async Task<IActionResult> Edit(int? id) // DbContext Method?
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles.FindAsync(id);
            
            if (vehicle == null)
            {
                return NotFound();
            }
            //ViewData["VehicleTypeId"] = new SelectList(_context.VehicleTypes, "Id", "Id", vehicle.VehicleTypeId);
            

            return View(vehicle);
        }

        public async Task<IActionResult> OverView() //2.0 => 3.0
        {
            var model = _context.Vehicles.Select(v => new VehiclesOverView 
            {
                Id = v.Id,
                Regnum = v.RegistrationNumber,
                Model = v.Model,
                Arrivaldate = v.ArrivalTime,
                ParkedTime = (DateTime.Now - v.ArrivalTime),
                //3.0
                OwnerID = v.OwnerId, // Todo Ej i ASP Vy
                MembershipId = _context.Memberships.FirstOrDefault(m => m.Id == v.OwnerId).Id, // Bara Membership ID
                Membership = _context.Memberships.FirstOrDefault(m => m.Id == v.OwnerId), // Hela membership
                VehicleType = v.VehicleType,
                ParkingSpots = v.ParkingSpots
            });

            //var membershipID = _context.Owners.Where(Membership)
            return View("OverView", await model.ToListAsync());
        }


        public async Task<IActionResult> MembersOverview() // Automapper.... OM JAG FÅR ETT ID så...
        {
            var model = _context.Memberships.Select(m => new MembersOverview
            {
                Id = m.Id,
                OwnerId = m.OwnerId, //  Todo Unique property
                Email = m.Email,
                Vehicles = _context.Vehicles.Where(v=> v.OwnerId == m.OwnerId).ToList(), // Med Hidden ID i nästa steg. DETAILS
                VehicleCount = _context.Vehicles.Where(v => v.OwnerId == m.OwnerId).Count()
            });
           
            return View(await model.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> FilterMembers(string email = null) // HTML input name="regnum" // [Bind("Id,OwnerId,VehicleTypeId")], Vehicle vehicle
        {
            var model = _context.Memberships.Select(m => new MembersOverview
            {
                Id = m.Id,
                OwnerId = m.OwnerId, //  Todo Unique property
                Email = m.Email,
                Vehicles = _context.Vehicles.Where(v => v.OwnerId == m.OwnerId).ToList(), // Med Hidden ID i nästa steg. DETAILS
                VehicleCount = _context.Vehicles.Where(v => v.OwnerId == m.OwnerId).Count()
            });

            model = email == null ?
                model :
                //model.Where(v => v.Regnum == regnum); EXACT MATCH
                model.Where(v => v.Email.Contains(email));

            if (model.Count() == 0)
            {
                TempData["Empty"] = "Registration number could not be found";
                Console.WriteLine("funkar");
            }

            return View(nameof(MembersOverview), await model.ToListAsync());
        }

        public async Task<IActionResult> MemberDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var member = _context.Memberships.Find(id);
            //var member = _context.Owners.FirstOrDefaultAsync(o => o.Id == member.Id);
            if (member == null)
            {
                return NotFound();
            }
            var owner = _context.Owners.FirstOrDefaultAsync(o => o.Id == member.Id);
            //var vehicles = _context.Vehicles.Where(v => v.OwnerId == owner.Id).ToList();
            var vehicles2 = _context.Owners.Include(o => o.Vehicles).Where(o => o.Id == member.Id);

            //.Include(v => v.VehicleType)
            //.FirstOrDefaultAsync(m => m.Id == id);

            var model = vehicles2.Select(ov => new OwnerVehicleView {

                Id= ov.Id,
                FirstName = ov.FirstName,
                LastName = ov.LastName,
                Vehicles = ov.Vehicles
            });

            return View(await model.ToListAsync()); // await
        }


        public async Task<IActionResult> ReceiptsOverView()
        {

                var receiptsContext = _context.Receipts;
                return View("ReceiptsOverView", await receiptsContext.ToListAsync());


        }

        // POST: Park/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OwnerId,RegistrationNumber,Passengers,Color,Wheels,VehicleTypeId")] Vehicle vehicle)
        {
            var arrivalTime = vehicle.ArrivalTime;
            if (id != vehicle.Id) // HMTL CHECK
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicle);
                    // Not set from outside or Bind. Todo View model and New class Preferred.
                    _context.Entry(vehicle).Property(v => v.ArrivalTime).IsModified = false; // Lämnas orörd vid SaveChanges.

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            //ViewData["VehicleTypeId"] = new SelectList(_context.VehicleTypes, "Id", "Id", vehicle.VehicleTypeId);
            return View(vehicle);
        }

        // GET: Park/UnPark/5
        public async Task<IActionResult> UnPark(int? id) // Replaced by Receipt
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles
                .Include(v => v.VehicleType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        public async Task<IActionResult> Receipt(int? id) // DO ONE THING. Unless you do Two of course.
        {
            if (id == null) { return NotFound(); }
            var vehicle = await _context.Vehicles.FindAsync(id);
            //var vehicle = await _context.Vehicles.FirstOrDefaultAsync(v => v.Id == id);
            if (vehicle == null) { return NotFound(); }

            var model = new ReceiptViewModel
            {
                Id = vehicle.Id,
                Regnum = vehicle.RegistrationNumber,
                Model = vehicle.Model,
                Arrivaldate = vehicle.ArrivalTime,
                Leavingdate = DateTime.Now,
                ParkingTime = (DateTime.Now - vehicle.ArrivalTime),
                Price = ((short)(DateTime.Now - vehicle.ArrivalTime).TotalMinutes) * 1
            };
            //Todo Persistent Receipt
            var receipt = new Receipt
            {
                VehicleID = model.Id,
                ArrivalTime = vehicle.ArrivalTime,
                CollecTime = DateTime.Now,
                Price = ((short)(DateTime.Now - vehicle.ArrivalTime).TotalMinutes) * 1
            };
            _context.Receipts.Add(receipt); //There is still only ONE database Call.
            _context.SaveChanges(); // Save changes

            return View("Receipt", model);
        }
        

        // POST: Park/UnPark/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleExists(int id)
        {
            return _context.Vehicles.Any(e => e.Id == id);
        }
    }
}
