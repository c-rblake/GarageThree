using GarageThree.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GarageThree.Services
{
    public class DevelopmentGetOwnerService : IDevelopmentGetOwnerService
    {
        //private readonly GarageContext _context;

        //public DevelopmentGetOwnerService(GarageContext _context)
        //{
        //    this._context = _context;
        //}

        public async Task<IEnumerable<SelectListItem>> DevelopmentGetOwners()
        {
            // Part 1: create List.
            // ... Print the first element of the List.
            List<int> list = new List<int>(new int[] {1, 2, 3, 7 }); // MÅSTE FINNAS I DATABASEN
            //Console.WriteLine($"FIRST ELEMENT: {list[0]}");

            return list.Select(o => new SelectListItem
            {
                Value = o.ToString(),
                Text = o.ToString()
            });
        }
    }
}
