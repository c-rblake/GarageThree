using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GarageThree.Models
{
    //TBD
    public class Receipt
    {
        public int id { get; set; }
        public int VehicleID { get; set; }
        public DateTime ArrivalTime { get; set; }
        public DateTime CollecTime { get; set; }
        public double Price { get; set; }

        //Vehicle.ArrivalTime
    }
}
