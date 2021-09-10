using GarageThree.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// Summary description for Class1
/// </summary>
/// 
namespace GarageThree
{
    public class CheckFirstAndLastNamesAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            const string errorMessage = "First name cannot be equal to last name";

            if (value is string input)
            {
                var model = (Owner)validationContext.ObjectInstance;
               
                if (model.FirstName == model.LastName)
                    return new ValidationResult(errorMessage);
                else
                    return ValidationResult.Success;
            }
            return new ValidationResult(errorMessage);
        }
    }
}


