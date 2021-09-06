using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GarageThree.Models
{
    public class VehicleType
    {
        public int Id { get; set; }

        public int Size { get; set; }
        public string TypeName { get; set; }
        public int ReqparkingSpots { get; set; }
        public int Price { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
