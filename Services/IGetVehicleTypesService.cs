﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GarageThree.Services
{
    public interface IGetVehicleTypesService
    {
        Task<IEnumerable<SelectListItem>> GetVehicleTypesAsync();
    }
}
