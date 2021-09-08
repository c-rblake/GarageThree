using GarageThree.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GarageThree.ViewModels
{

        public class VehiclesOverView
        {
            public int Id { get; set; }
            public VehicleType Type { get; set; }
            public string Regnum { get; set; }
            public DateTime Arrivaldate { get; set; }

            public TimeSpan ParkedTime { get; set; }
        public string Model { get; internal set; }
    }

}
