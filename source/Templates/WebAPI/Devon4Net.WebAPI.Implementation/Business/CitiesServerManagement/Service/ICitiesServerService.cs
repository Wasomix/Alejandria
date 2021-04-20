using Devon4Net.WebAPI.Implementation.Business.CitiesServerManagement.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Devon4Net.WebAPI.Implementation.Business.CitiesServerManagement.Service
{
    public interface ICitiesServerService
    {
        public Task<IEnumerable<CitiesServerDto>> GetAllCities();
        public Task<CitiesServerDto> GetInformationFromOneCity(string city);
        public Task<CitiesServerDto> CreateNewCity(CitiesServerDto newCity);
    }
}
