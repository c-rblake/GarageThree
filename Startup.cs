
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GarageThree.Data;
using GarageThree.Services;

namespace GarageThree
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<GarageContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("GarageContext"))); //Spelet GetSection X,y
            services.AddScoped<IGetVehicleTypesService, GetVehicleTypesService>(); // kan anv�ndas
            services.AddScoped<IDevelopmentGetOwnerService, DevelopmentGetOwnerService>(); // kan anv�ndas
            //services.AddScoped<IGetVehicleTypesService, GetTypeServiceTwo>();

        }
        //Todo Movies service injection. Dropdown Index3.

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    //pattern: "{controller=Vehicles}/{action=Index}/{id?}");
                    pattern: "{controller=OwnerMembershipSignups}/{action=Signup}/{id?}");
            });
        }
    }
}
