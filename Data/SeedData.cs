using Bogus;
using Bogus.Extensions.Sweden;
using GarageThree.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GarageThree.Data
{
    public class SeedData
    {
        private static Faker fake;

        internal static async Task InitAsync(IServiceProvider services)
        {
            using (var db = services.GetRequiredService<GarageContext>())
            {
                //TODO
                fake = new Faker("sv");

                if (await db.Vehicles.AnyAsync()) return; //IF any.
                //if (await db.Memberships.AnyAsync()) return;
                
                var owners = GetOwners();
                await db.AddRangeAsync(owners);
                await db.SaveChangesAsync();

                //var members = GetMemberships(owners);
                //await db.AddRangeAsync(members);

                //var vehicles = GetVehicles();
                //await db.AddRangeAsync(owners);

                var vehicleTypes = GetVehiclesTypes(); 
                await db.VehicleTypes.AddRangeAsync(vehicleTypes); 
                await db.SaveChangesAsync();

                var parkingSpots = GetParkingSpots();
                await db.AddRangeAsync(parkingSpots);
                await db.SaveChangesAsync();

                var vehicles = GetVehicles(owners, vehicleTypes);
                await db.AddRangeAsync(vehicles);
                await db.SaveChangesAsync();

                var vehicleParkingSpot = VehicleParkingSpots(vehicles, parkingSpots); 
                await db.AddRangeAsync(vehicleParkingSpot);
                await db.SaveChangesAsync();

            }

        }

        private static List<VehicleParkingSpot> VehicleParkingSpots(List<Vehicle> vehicles, List<ParkingSpot> parkingSpots)
        {
            var vehicleParkingSpots = new List<VehicleParkingSpot>();
            int k = 5;
            for (int i = 0; i < k; i++)
            {
                var parking = new VehicleParkingSpot
                {
                    ParkingSpot = parkingSpots[i],
                    Vehicle = vehicles[i] 
                };
                vehicleParkingSpots.Add(parking);
            }

            return vehicleParkingSpots;
        }

        private static List<Vehicle> GetVehicles(List<Owner> owners, List<VehicleType> vehicleTypes)
        {
            var vehicles = new List<Vehicle>();
            int k = 8;
            for (int i = 0; i < k; i++)
            {
                var vehicle = new Vehicle
                {
                    Owner = owners[i%5],
                    VehicleType = vehicleTypes[i%5],
                    Color = fake.Commerce.Color(),
                    ArrivalTime = fake.Date.Recent(7),
                    Model = fake.Vehicle.Model(),
                    RegistrationNumber = fake.Lorem.Letter(3).ToUpper() + fake.Random.Number(100, 999).ToString(),
                    Passengers = fake.Random.Int(1,6),
                    Wheels = fake.Random.Int(4, 6)
                };

                vehicles.Add(vehicle);
            }
            return vehicles;
        }

        private static ICollection<Vehicle> MakeVehicles()
        {
            var vehicles = new List<Vehicle>();
            int k = 8;
            for (int i = 0; i < k; i++)
            {
                var vehicle = new Vehicle
                {
                    OwnerId = 1,
                    Color = fake.Commerce.Color(),
                    ArrivalTime = fake.Date.Recent(7),
                    Model = fake.Vehicle.Model(),
                    RegistrationNumber = fake.Lorem.Letter(3).ToUpper() + fake.Random.Number(100, 999).ToString(),
                    Passengers = fake.Random.Int(6),
                    Wheels = fake.Random.Int(4, 6),
                };
                vehicles.Add(vehicle);
            }
            return vehicles;
        }

        private static List<ParkingSpot> GetParkingSpots()
        {
            var parkingSpots = new List<ParkingSpot>();
            int j = 20;
            for (int i = 1; i <= j; i++)
            {
                var parkingSpot = new ParkingSpot
                {
                    ParkingId = i
                };
                parkingSpots.Add(parkingSpot);
            }
            return parkingSpots;
        }

        private static List<VehicleType> GetVehiclesTypes()
        {
            var vehicleTypes = new List<VehicleType>()
            {
                new VehicleType {Price = 50000, ReqparkingSpots = 1, Size = 1, TypeName = "Car"},
                new VehicleType {Price = 50000, ReqparkingSpots = 1, Size = 1, TypeName = "Mini van"},
                new VehicleType {Price = 50000, ReqparkingSpots = 1, Size = 1, TypeName = "Suv"},
                new VehicleType {Price = 50000, ReqparkingSpots = 1, Size = 3, TypeName = "Bus"},
                new VehicleType {Price = 50000, ReqparkingSpots = 1, Size = 4, TypeName = "Truck"}
            };
            return vehicleTypes;
        }
        private static List<Membership> GetMemberships(List<Owner> owners)
        {
            var memberships = new List<Membership>();
            int j = 5;
            for (int i = 0; i < j; i++)
            {
                var membership = new Membership
                {
                    PhoneNumber = fake.Phone.PhoneNumber(),
                    PersonalNumber = fake.Person.Personnummer(),
                    Email = fake.Internet.Email(),
                    Password = fake.Internet.Password(8),
                    RegistrationTime = fake.Date.Recent(7),
                    OwnerId = owners[i].Id
                };
                memberships.Add(membership);
            }
            return memberships;
        }

        private static List<Owner> GetOwners()
        {
            var owners = new List<Owner>();
            int j = 5;
            for (int i = 0; i < j; i++)
            {
                var owner = new Owner
                {
                    Age = fake.Random.Int(18, 60),
                    FirstName = fake.Name.FirstName(),
                    LastName = fake.Name.LastName(),
                    //MembershipId = i,
                    Phone = fake.Phone.PhoneNumber(),
                    Membership = new Membership
                    {
                        PhoneNumber = fake.Phone.PhoneNumber(),
                        PersonalNumber = fake.Person.Personnummer(),
                        Email = fake.Internet.Email(),
                        
                        Password = fake.Internet.Password(8),
                        RegistrationTime = fake.Date.Recent(7),


                    }
                };

                owners.Add(owner);
            };

            return owners;
        }






    }
}
