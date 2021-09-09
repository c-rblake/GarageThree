﻿using System;
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

        public async Task<IActionResult> Index2()
        {
            var garageContext = _context.Vehicles.Include(v => v.VehicleType).Include(ps => ps.ParkingSpot);
            return View(nameof(Index2), await garageContext.ToListAsync());
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
                if (_context.Vehicles.Where(v=>v.RegistrationNumber.Contains(vehicle.RegistrationNumber)).Count()>0) // Select(v=>v).ToList().
                {
                    // TODO SWAP TO DEPENDECY INJECTION-
                    ModelState.AddModelError("RegistrationNumber", "Registration number must be unique");
                    return View(vehicle);
                }

                _context.Add(vehicle);
                vehicle.ArrivalTime = DateTime.Now; // Set Independently
                await _context.SaveChangesAsync(); // readonly though??
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
                VehicleType = v.VehicleType
            });

            //var membershipID = _context.Owners.Where(Membership)
            return View("OverView", await model.ToListAsync());
        }

        public async Task<IActionResult> MembersOverview() // Automapper.... OM JAG FÅR ETT ID så...
        {
            var model = _context.Memberships.Select(m => new MembersOverview
            {
                Id = m.Id,
                OwnerId = m.OwnerId,
                Email = m.Email,
                Vehicles = _context.Vehicles.Where(v=> v.OwnerId == m.OwnerId).ToList(), // Med Hidden ID i nästa steg. DETAILS
                VehicleCount = _context.Vehicles.Where(v => v.OwnerId == m.OwnerId).Count()
            }
            );
            return View("MembersOverview", await model.ToListAsync());
        }
        public async Task<IActionResult> MemberDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = _context.Memberships.Find(id);
            var owner = _context.Owners.FirstOrDefaultAsync(o => o.Id == member.Id);
            var vehicles = _context.Vehicles.Where(v => v.OwnerId == owner.Id).ToList();
                //.Include(v => v.VehicleType)
                //.FirstOrDefaultAsync(m => m.Id == id);

            if (member == null)
            {
                return NotFound();
            }

            return View(vehicles);
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
