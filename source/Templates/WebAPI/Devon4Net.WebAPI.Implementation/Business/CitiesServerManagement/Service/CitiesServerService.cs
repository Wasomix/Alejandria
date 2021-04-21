using Devon4Net.Domain.UnitOfWork.Service;
using Devon4Net.Domain.UnitOfWork.UnitOfWork;
using Devon4Net.Infrastructure.Log;
using Devon4Net.WebAPI.Implementation.Business.CitiesServerManagement.Converter;
using Devon4Net.WebAPI.Implementation.Business.CitiesServerManagement.Dto;
using Devon4Net.WebAPI.Implementation.Domain.Database;
using Devon4Net.WebAPI.Implementation.Domain.RepositoryInterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Devon4Net.WebAPI.Implementation.Business.CitiesServerManagement.Service
{
    public class CitiesServerService : Service<AlejandriaContext>, ICitiesServerService
    {
        private ICitiesServerRepository _citiesServerRepository;

        public CitiesServerService(IUnitOfWork<AlejandriaContext> uow) : base (uow)
        {
            _citiesServerRepository = uow.Repository<ICitiesServerRepository>();
        }

        public async Task<IEnumerable<CitiesServerDto>> GetAllCities()
        {
            Devon4NetLogger.Debug($"GetAllCities method from service CitiesServerService");
            var result = await _citiesServerRepository.GetAllCities().ConfigureAwait(false);

            return CitiesServerConverter.FromListOfModelToDto(result);
        }

        public async Task<CitiesServerDto> GetInformationFromOneCity(string city)
        {
            var cityInformation = await _citiesServerRepository.GetInformationFromOneCity(city).ConfigureAwait(false);

            return CitiesServerConverter.FromModelToDto(cityInformation);
        }

        public async Task<CitiesServerDto> CreateNewCity(CitiesServerDto newItem)
        {
            Devon4NetLogger.Debug($"GetAllCities method from service CitiesServerService");
            var result = await _citiesServerRepository.Create(newItem).ConfigureAwait(false);

            return CitiesServerConverter.FromModelToDto(result);
        }

        public async Task<bool> DeleteCity(string city)
        {
            Devon4NetLogger.Debug($"DeleteCity method from service CitiesServerService");
            var result = await _citiesServerRepository.DeleteCity(city).ConfigureAwait(false);

            return result;
        }
    }
}
