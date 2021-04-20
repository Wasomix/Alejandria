using Devon4Net.WebAPI.Implementation.Business.CitiesClientManagement.Dto;
using Devon4Net.WebAPI.Implementation.Business.CitiesServerManagement.Dto;
using System.Collections.Generic;

namespace Devon4Net.WebAPI.Implementation.Business.CitiesClientManagement.Converter
{
    public class CitiesClientConverter
    {
        public static List<CitiesClientDto> FromListOfCitiesServerToClient(List<CitiesServerDto> citiesServer)
        {
            List<CitiesClientDto> citiesClient = new List<CitiesClientDto>();
            foreach(var city in citiesServer)
            {
                citiesClient.Add(FromCityServerToClient(city));
            }

            return citiesClient;
        }

        public static CitiesClientDto FromCityServerToClient(CitiesServerDto cityServer)
        {
            return new CitiesClientDto {City = cityServer.City, Country = cityServer.Country };
        }
    }
}
