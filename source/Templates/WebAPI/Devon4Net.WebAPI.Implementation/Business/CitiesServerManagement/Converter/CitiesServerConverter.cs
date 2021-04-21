using Devon4Net.WebAPI.Implementation.Business.CitiesServerManagement.Dto;
using Devon4Net.WebAPI.Implementation.Domain.Entities;
using System.Collections.Generic;


namespace Devon4Net.WebAPI.Implementation.Business.CitiesServerManagement.Converter
{
    public class CitiesServerConverter
    {
        public static List<CitiesServerDto> FromListOfModelToDto(IList<CitiesCountry> listOfCitiesCountry)
        {
            List<CitiesServerDto> listOfCities = new List<CitiesServerDto>();
            
            foreach(var city in listOfCitiesCountry)
            {
                listOfCities.Add(FromModelToDto(city));
            }

            return listOfCities;
        }
        public static CitiesServerDto FromModelToDto(CitiesCountry city)
        {
            return new CitiesServerDto { City = city.City, Country = city.Country };
        }
    }
}
