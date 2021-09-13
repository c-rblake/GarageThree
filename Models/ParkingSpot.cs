using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GarageThree.Models
{
    public class ParkingSpot
        //Todo Set Limit. Sätta size eller appconfig. Börja med seed flytta - till appconfig för snabbare deploy.
    {
        public int Id { get; set; }
        public int ParkingId { get; set; }
        public ICollection<Vehicle> Vehicles = new List<Vehicle>(); // For parking Or else NUll Reference..
        public ICollection<VehicleParkingSpot> VehicleParkingSpots { get; set; }
    }
}
