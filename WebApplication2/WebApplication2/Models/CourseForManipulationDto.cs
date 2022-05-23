using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.ValidationAttributes;

namespace WebApplication2.Models
{
    [TitleMustBeDifferentFromDescriptionAttribute]
    public abstract class CourseForManipulationDto
    {
        [Required(ErrorMessage = "Title is required, custom message")]
        [MaxLength(100)]
        public string Title { get; set; }
        
        [MaxLength(1500)]
        public virtual string Description { get; set; }
    }
}
