using CityInform.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInform.API
{
    public class CitiesDataStore
    {
        public static CitiesDataStore Current { get; } = new CitiesDataStore();

        public List<CityDto> Cities { get; set; }

        public CitiesDataStore()
        {
            Cities = new List<CityDto>()
            {
                new CityDto() {Id = 1 , Name= "NYC", Description = "NYC Desc",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto() { Id=1, Name="Central Park", Description="Cental Park Desc"},
                        new PointOfInterestDto() { Id=2, Name="Central Hall", Description="Cental Hall Desc Desc"}
                    }},
                new CityDto() {Id = 2 , Name= "Pune", Description = "Pune Desc",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto() { Id=1, Name="Central Park", Description="Cental Park Desc"},
                        new PointOfInterestDto() { Id=2, Name="Central Hall", Description="Cental Hall Desc Desc"}
                    }},
                new CityDto() {Id = 3 , Name= "Mumbai", Description = "Mumbai Desc",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto() { Id=1, Name="Central Park", Description="Cental Park Desc"},
                        new PointOfInterestDto() { Id=2, Name="Central Hall", Description="Cental Hall Desc Desc"}
                    }},
            };
        }
    }
}
