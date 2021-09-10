﻿using GarageThree.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GarageThree.ViewModels
{
    public class OwnerVehicleView
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Vehicle> Vehicles { get; set; }
    }
}