using GarageThree.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GarageThree.Models
{
    public class Membership
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string PhoneNumber { get; set; }

        [Required]
        //[RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$"), StringLength(12)]
        [ValidatePersonalNumber]
        public string PersonalNumber { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        public DateTime RegistrationTime { get; set; }
    }
}
