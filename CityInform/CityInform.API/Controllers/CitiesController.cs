using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInform.API.Controllers 
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        [HttpGet]
        public ActionResult GetCities()
        {
            return Ok(CitiesDataStore.Current.Cities);
        }

        [HttpGet("{cityId}")]
        public ActionResult GetCity(int cityId)
        {
            var city = CitiesDataStore.Current.Cities.Where(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            return Ok(city);
        }
    }
}
