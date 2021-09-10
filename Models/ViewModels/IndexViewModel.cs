using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GarageThree.Models.ViewModels
{
    public class IndexViewModel
    {
        //id, regno, owner, fordonstyp, parkeradtid
                public int Id { get; set; }
                public string RegistrationNumber { get; set; }
                public int ParkId { get; set; }
                public string OwnerFullName { get; set; }
               
                public string VehicleTypeTypeName { get; set; }
                public int MemberId { get; set; }
    }
}
