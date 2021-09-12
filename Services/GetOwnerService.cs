using GarageThree.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GarageThree.Services
{
    public class GetOwnerService : IGetOwnerService
    {
        //QUESTIONABLE IF THIS SHOULD BE USED...
        private readonly GarageContext _context;

        public GetOwnerService(GarageContext _context)
        {
            this._context = _context;
        }

        public async Task<IEnumerable<SelectListItem>> GetOwnersAsync()
        {
            return await _context.Owners.Select(o => new SelectListItem { 
            Value = o.Id.ToString(),
            Text = o.FullName
            }).ToListAsync();

        }
    }
}
