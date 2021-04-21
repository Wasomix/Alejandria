using Devon4Net.WebAPI.Implementation.Business.CitiesClientManagement.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Devon4Net.WebAPI.Implementation.Business.CitiesClientManagement.Service
{
    public interface ICitiesClientService
    {
        public Task<IEnumerable<CitiesClientDto>> GetAllCities();
        public Task<CitiesClientDto> GetInformationFromOneCity(string city);
        public Task<CitiesClientDto> CreateNewCity(CitiesClientDto newCity);
        public Task<CitiesClientDto> DeleteCity(string cityToDelete);
    }
}
