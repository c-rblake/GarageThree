using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GarageThree.Validation
{
    public class ValidatePersonalNumber : ValidationAttribute
    {

        public override bool IsValid(object value)
        {
            if (value is string personalNumber)
            {
                var personNumberArray = personalNumber.ToString().ToCharArray().Select(Convert.ToInt32).ToArray();

                if (personNumberArray.Length != 12)
                    return false;
                if (Convert.ToInt32(personalNumber.ToString().Substring(4, 2)) <= 0 || Convert.ToInt32(personalNumber.ToString().Substring(4, 2)) > 12)
                    return false;
                var dateMonth = Convert.ToInt32(personalNumber.ToString().Substring(6, 2));
                if (dateMonth <= 0 && dateMonth > 31)
                    return false;
                try
                {
                    var completeDate = Convert.ToDateTime(
                    personalNumber.ToString().Substring(0, 4) + "-" +
                    personalNumber.ToString().Substring(4, 2) + "-" +
                    personalNumber.ToString().Substring(6, 2)
                    );
                    if (completeDate.Year > DateTime.Now.Year - 18 || completeDate.Year < DateTime.Now.Year - 100)
                        return false;
                }
                catch { return false; }
            }

            //Cast to int
            //Validation logic
            //4 first is det ett valid year årtal, är user över 18år, är user max 100år
            //is number 5 och 6 en valid  month?
            //is number 7 och 8 ett valid day
            //is det right number of digits 4
            //Return result
            //return base.IsValid(value);
            return true;
        }
    }
}
