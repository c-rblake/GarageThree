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
                if (await db.Owners.AnyAsync()) return; //IF any.

                fake = new Faker("sv");

                //Owner => membership //Parkingspot //Vehicle types sist vehiclesparkingspot

                var owners = GetOwners();
                await db.AddRangeAsync(owners);
                await db.SaveChangesAsync();


                var members = GetMemberships(owners);
                await db.AddRangeAsync(members);

                await db.SaveChangesAsync();

                //var vehicles = GetVehicles();
                //await db.AddRangeAsync(owners);

                var vehicles = GetVehiclesTypes();
                await db.AddRangeAsync(vehicles);

            }


        }

        private static List<VehicleType> GetVehiclesTypes()
        {
            var vehicleTypes = new List<VehicleType>
            { 
                new VehicleType {Price = 50000, ReqparkingSpots = 1, Size = 1, TypeName = "Car"},
                new VehicleType {Price = 50000, ReqparkingSpots = 1, Size = 1, TypeName = "Mini van"},
                new VehicleType {Price = 50000, ReqparkingSpots = 1, Size = 1, TypeName = "Suv"},
                new VehicleType {Price = 50000, ReqparkingSpots = 1, Size = 1, TypeName = "Bike"},
                new VehicleType {Price = 50000, ReqparkingSpots = 1, Size = 1, TypeName = "Riding Cat"}
            
            };
            return vehicleTypes;

        }

        private static List<Membership> GetMemberships(List<Owner> owners)
        {
            var memberships = new List<Membership>();
            int j = 10;
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
            int j = 10;
            for (int i = 0; i < j; i++)
            {
                var owner = new Owner
                {
                    Age = fake.Random.Int(18, 60),
                    FirstName = fake.Name.FirstName(),
                    LastName = fake.Name.LastName(),
                    //MembershipId = Owner[i].id
                    Phone = fake.Phone.PhoneNumber()

                };
                owners.Add(owner);
            }
            return owners;
        }






    }
}
