using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GarageThree.Models
{
    public class Owner
    {
        public int Id { get; set; }
        
        [CheckFirstAndLastNames]
        public string FirstName { get; set; }
        [CheckFirstAndLastNames]
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public int Age { get; set; }
        public string Phone { get; set; }

        
        public int MembershipId { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; }
        
        
    }
}
