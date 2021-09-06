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
        public IEnumerable<Parking> ParkingId { get; set; }

        public string RegistrationNumber { get; set; }
        public int Passengers { get; set; }
        public string Color { get; set; }
        public int Wheels { get; set; }
    }
}
