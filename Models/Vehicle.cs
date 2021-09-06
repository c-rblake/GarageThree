using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GarageThree.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public int VehicleTypeId { get; set; }
        public IEnumerable<Parking> Parkings { get; set; }
    }
}
