using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GarageThree.Models
{
    public class Owner
    {
        public int Id { get; set; }
        

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int Age { get; set; }
        public int PhonNumber { get; set; }
        public int MembershipId { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
