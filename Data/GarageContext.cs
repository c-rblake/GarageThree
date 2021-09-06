using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GarageThree.Models;

namespace GarageThree.Data
{
    public class GarageContext : DbContext
    {
        public GarageContext (DbContextOptions<GarageContext> options)
            : base(options)
        {
        }

        public DbSet<GarageThree.Models.Vehicle> Vehicle { get; set; }
        public DbSet<GarageThree.Models.Membership> Membership { get; set; }
        public DbSet<GarageThree.Models.Owner> Owner { get; set; }
        public DbSet<GarageThree.Models.Parking> Parking { get; set; }
        public DbSet<GarageThree.Models.ParkingSpot> ParkingSpot { get; set; }
        public DbSet<GarageThree.Models.VehicleType> VehicleType { get; set; }
    }
}
