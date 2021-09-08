using GarageThree.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GarageThree
{
    public class Program
    {
        private static GarageContext _context = new GarageContext();
        
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();


            //TODO SEED Garage Spots for Example.
            //using (var scope = host.Services.CreateScope())
            //{

            //    var services = scope.ServiceProvider;

            //    try
            //    {
                    
            //    }
            //    catch (Exception)
            //    {

            //        throw;
            //    }
              
            //}


            host.Run();
            //SomeSeed();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        //For seed purpose
        //private static void SomeSeed()
        //{
        //    _context.Vehicles.AddRange(
        //        new Models.Vehicle { Color = "Red", OwnerId = 1, Passengers = 1, VehicleTypeId = 1 }
        //        );
        //    _context.SaveChanges();
        //}
    }
}
