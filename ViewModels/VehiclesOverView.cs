using GarageThree.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GarageThree.ViewModels
{

    public class VehiclesOverView
    {
        public int Id { get; set; }
        public VehicleType VehicleType { get; set; }
        public string Regnum { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd-hh-mm}", ApplyFormatInEditMode = true)]
        public DateTime Arrivaldate { get; set; }

        [DisplayFormat(DataFormatString = @"{0:hh\:mm\:ss}")]
        [DisplayName("Parked Time")]
        public TimeSpan ParkedTime { get; set; }
        public string Model { get; internal set; }

        public int OwnerID { get; set; }
        public int MembershipId { get; set; }

        public Membership Membership { get; set; }

        public ICollection<ParkingSpot> ParkingSpots { get; set; }


    }

}
