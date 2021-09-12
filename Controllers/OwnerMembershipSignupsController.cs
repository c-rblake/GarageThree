using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GarageThree.Data;
using GarageThree.Models.ViewModels;

namespace GarageThree.Controllers
{
    public class OwnerMembershipSignupsController : Controller
    {
        private readonly GarageContext _context;

        public OwnerMembershipSignupsController(GarageContext context)
        {
            _context = context;
        }

        // GET: OwnerMembershipSignups/Create
        public IActionResult Signup()
        {
            return View();
        }

        // POST: OwnerMembershipSignups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Signup([Bind("Id,FirstName,LastName,Age,Phone,PhoneNumber,PersonalNumber,Password,Email,RegistrationTime")] OwnerMembershipSignup ownerMembershipSignup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ownerMembershipSignup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ownerMembershipSignup);
        }

    }
}
