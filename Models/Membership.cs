using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GarageThree.Models
{
    public class Membership
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public int PersonalNumber { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationTime { get; set; }
    }
}
