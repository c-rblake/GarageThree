using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GarageThree.Models
{
    public class ParkingSpot
        //Todo Set Limit.
    {
        public int Id { get; set; }
        public int ParkingId { get; set; }
        public IEnumerable<Vehicle> Vehicle { get; set; }
    }
}
