using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.ValidationAttributes
{
    public class TitleMustBeDifferentFromDescriptionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var course = (CourseForManipulationDto)validationContext.ObjectInstance;
            if (course.Title == course.Description)
            {
                return new ValidationResult("The description should be different than title", new[] { "CourseForCreationDto" });
            }
            return ValidationResult.Success;
        }
    }
}
