using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GarageThree.Models;

namespace GarageThree.Data
{
    public class GarageContext : DbContext
    {
        public GarageContext (DbContextOptions<GarageContext> options)
            : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<VehicleParkingSpot> Parkings { get; set; }
        public DbSet<ParkingSpot> ParkingSpots { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>() 
                .HasMany(v => v.ParkingSpot) 
                .WithMany(ps => ps.Vehicle) 
                .UsingEntity<VehicleParkingSpot>
                (vps => vps.HasOne<ParkingSpot>().WithMany(),
                vps => vps.HasOne<Vehicle>().WithMany());
        }

    }


}
