using GarageThree.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GarageThree.Services
{
    public class GetVehicleTypesService : IGetVehicleTypesService
    {
        private readonly GarageContext _context;
        public GetVehicleTypesService(GarageContext _context)
        {
            this._context = _context;
        }
        public async Task<IEnumerable<SelectListItem>> GetVehicleTypesAsync()
        {
            return await _context.VehicleTypes.Select(vt => new SelectListItem
            {
                //Disabled
                Value = vt.Id.ToString(),
                Text = vt.TypeName
            }).ToListAsync();

        }
    }
}
