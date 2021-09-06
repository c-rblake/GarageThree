using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GarageThree.Models
{
    public class Parking
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int ParkingSpotId { get; set; }
    }
}
