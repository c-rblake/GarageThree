using GarageThree.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GarageThree.ViewModels
{
    public class MembersOverview
    {
        public int Id { get; set; }

        
        public string Email { get; set; }

        //FK
        public int OwnerId { get; set; }

        //Nav prop
        public List<Vehicle> Vehicles { get; set; }
        public int VehicleCount { get; internal set; }
    }
}
