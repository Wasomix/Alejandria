using Devon4Net.WebAPI.Implementation.Business.CitiesServerManagement.Dto;
using Devon4Net.WebAPI.Implementation.Domain.RepositoryInterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Devon4Net.WebAPI.Implementation.Business.CitiesServerManagement.Service
{
    public class CitiesServerService : /*Service<>,*/ ICitiesServerService
    {
        //private ICitiesServerRepository _citiesServerRepository;

        public CitiesServerService(/*ICitiesServerRepository citiesServerRepository*/)
        {
            //_citiesServerRepository = citiesServerRepository;
        }

        public Task<IEnumerable<CitiesServerDto>> GetAllCities()
        {
            IEnumerable<CitiesServerDto> result = new List<CitiesServerDto>()
            {
                new CitiesServerDto {City = "Madrid", Country = "Spain" },
                new CitiesServerDto {City = "Castellon", Country = "Spain" },
                new CitiesServerDto {City = "Berlin", Country = "Germany" }
            };

            return Task.FromResult(result);
        }

        public Task<CitiesServerDto> GetInformationFromOneCity(string city)
        {
            CitiesServerDto mockData = new CitiesServerDto { City = city, Country = "Germany" };
            
            return Task.FromResult(mockData);
        }

        public Task<CitiesServerDto> CreateNewCity(CitiesServerDto newItem)
        {
            CitiesServerDto mockData = new CitiesServerDto
            {
                City = newItem.City,
                Country = newItem.Country
            };

            return Task.FromResult(mockData);
        }
    }
}
