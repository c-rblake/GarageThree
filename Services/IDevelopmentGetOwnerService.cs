using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GarageThree.Services
{
    public interface IDevelopmentGetOwnerService
    {
        Task<IEnumerable<SelectListItem>> DevelopmentGetOwners();
    }
}