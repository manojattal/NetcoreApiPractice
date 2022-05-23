using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class CourseForUpdateDto : CourseForManipulationDto
    {
        [Required]
        public override string Description { get => base.Description; set => base.Description = value; }
    }
}
