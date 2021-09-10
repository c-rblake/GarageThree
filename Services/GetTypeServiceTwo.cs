using GarageThree.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GarageThree.Services
{
    public class GetTypeServiceTwo : IGetVehicleTypesService
    {
        private readonly GarageContext _context;
        public GetTypeServiceTwo(GarageContext _context)
        {
            this._context = _context;
        }
        public async Task<IEnumerable<SelectListItem>> GetVehicleTypesAsync()
        {
            return await _context.VehicleTypes.Select(vt => new SelectListItem { 
            Value = vt.Id.ToString(),
            Text = vt.TypeName
            }).ToListAsync();
        }

    }
}
