using GarageThree.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GarageThree.Controllers
{
    public class codePalette
    {
        private readonly GarageContext _context;

        public codePalette(GarageContext context)
        {
            _context = context;
        }

        //private static void ForeignKey_AddQuoteToExistingSamuraiNotTracked(int samuraiId = 2)
        //    // GET ALL THE CARS FROM AN OWNER. Or ALL PARKINGSPOTS FROM A VEHICLE
        //{
        //    var quote = new Quote { Text = "Thanks for dinner!", SamuraiId = samuraiId };
        //    using var newContext = new SamuraiContext(); 
        //    newContext.Quotes.Add(quote);
        //    newContext.SaveChanges();
        //}


        public void IncludeAndFilter() // Example with owners and vehicles
        {
            int membershipID = 1; // should come as an Input in a ViewModel.
            // <input type="hidden" asp-for="Id" /> Create or Edit has this Logic.
            var OwnersWithVehicles = _context.Owners.Include(o => o.Vehicles).ToList(); // all owners and vehicles.
            var FilteredVehicles = _context.Owners.Include(o => o.Vehicles.Where(v=> v.OwnerId == membershipID)); 
            // Could break the code up in two to check for Nulls first on Vehicles then on OwnerID.
        }
    }

}
