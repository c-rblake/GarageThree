using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GarageThree.Data;
using GarageThree.Models;

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
            var garageContext = _context.Vehicles.Include(v => v.VehicleType);
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
                _context.Add(vehicle);
                await _context.SaveChangesAsync(); // readonly though??
                return RedirectToAction(nameof(Index));
            }
            ViewData["VehicleTypeId"] = new SelectList(_context.VehicleTypes, "Id", "Id", vehicle.VehicleTypeId);
            ViewData["OwnerId"] = new SelectList(_context.Owners, "Id", "Id", vehicle.OwnerId); // Id Value, Text
            return View(vehicle);
        }

        // GET: Park/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
            ViewData["VehicleTypeId"] = new SelectList(_context.VehicleTypes, "Id", "Id", vehicle.VehicleTypeId);
            return View(vehicle);
        }

        // POST: Park/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OwnerId,RegistrationNumber,Passengers,Color,Wheels,VehicleTypeId")] Vehicle vehicle)
        {
            if (id != vehicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicle);
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
            ViewData["VehicleTypeId"] = new SelectList(_context.VehicleTypes, "Id", "Id", vehicle.VehicleTypeId);
            return View(vehicle);
        }

        // GET: Park/UnPark/5
        public async Task<IActionResult> UnPark(int? id)
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
