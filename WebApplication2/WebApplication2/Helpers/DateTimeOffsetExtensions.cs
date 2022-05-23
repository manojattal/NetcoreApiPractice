using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Helpers
{
    public static class DateTimeOffsetExtensions
    {
        public static int GetCurrentAge(this DateTimeOffset date)
        {
            var currentDate = DateTime.UtcNow;
            var age = currentDate.Year - date.Year;

            if(date.AddYears(age) > DateTime.Now)
            {
                age--;
            }
            return age;
        }
    }
}
