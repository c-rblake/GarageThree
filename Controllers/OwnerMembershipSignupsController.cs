using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GarageThree.Data;
using GarageThree.Models.ViewModels;
using GarageThree.Models;

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
        public async Task<IActionResult> Signup([Bind("Id,FirstName,LastName,Age,PhoneNumber,PersonalNumber,Password,Email")] OwnerMembershipSignup ownerMembershipSignup)
        {

            if (ModelState.IsValid)
            {
                var owner = new Owner
                {
                    Age = ownerMembershipSignup.Age,
                    FirstName = ownerMembershipSignup.FirstName,
                    LastName = ownerMembershipSignup.LastName,
                    //FullName = $"{ownerMembershipSignup.FirstName + " " + ownerMembershipSignup.LastName}"
                    Phone = ownerMembershipSignup.PhoneNumber,
                    Membership = new Membership
                    {
                        PhoneNumber = ownerMembershipSignup.PhoneNumber,
                        Password = ownerMembershipSignup.Password,
                        PersonalNumber = ownerMembershipSignup.PersonalNumber,
                        Email = ownerMembershipSignup.Email,
                        RegistrationTime = DateTime.Now
                    }
                };

                //Todo If ownerMembershipSignup.Age redirect to index .... Else redirect to create vehicle

                _context.Add(owner);
                await _context.SaveChangesAsync();
                if (ownerMembershipSignup.Age >= 18)
                    return RedirectToAction("Create", "Vehicles");
                //RedirectToAction("actionName", "controllerName"
                return RedirectToAction("Index","Park");

            }
            return View(ownerMembershipSignup);
        }

    }
}
