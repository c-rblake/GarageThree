using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GarageThree.Models
{
    public class VehicleParkingSpot // Join Table
    {
        //public int Id { get; set; }
        public int VehicleId { get; set; }
        public int ParkingSpotId { get; set; }

        public Vehicle Vehicle { get; set; }
        public ParkingSpot ParkingSpot { get; set; }

        //public DateTime ArrivalTime { get; set; }
        //public DateTime CollectTime { get; set; }
    }
}
