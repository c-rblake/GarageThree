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
        public string RegistrationNumber { get; set; }
        public int? Passengers { get; set; }
        public string Color { get; set; }
        public int? Wheels { get; set; }

        //Foreign Key
        public int VehicleTypeId { get; set; }

        //Navigation Properties
        public VehicleType VehicleType { get; set; }

        public IEnumerable<ParkingSpot> ParkingSpot { get; set; }
        //public object ParkingSpots { get; internal set; }
    }
}
