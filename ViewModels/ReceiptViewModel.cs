using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GarageThree.ViewModels
{
    public class ReceiptViewModel
    {
        public int Id { get; set; }
        public string Regnum { get; set; }
        public string Model { get; set; }
        public DateTime Arrivaldate { get; set; }
        public DateTime Leavingdate { get; set; }
        public double Price { get; set; }

        public TimeSpan ParkingTime;
    }
}
