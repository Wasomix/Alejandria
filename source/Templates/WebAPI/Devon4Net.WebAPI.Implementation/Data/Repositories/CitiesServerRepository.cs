using Devon4Net.Domain.UnitOfWork.Repository;
using Devon4Net.Infrastructure.Log;
using Devon4Net.WebAPI.Implementation.Business.CitiesServerManagement.Dto;
using Devon4Net.WebAPI.Implementation.Domain.Database;
using Devon4Net.WebAPI.Implementation.Domain.Entities;
using Devon4Net.WebAPI.Implementation.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Devon4Net.WebAPI.Implementation.Data.Repositories
{
    public class CitiesServerRepository : Repository<CitiesCountry>, ICitiesServerRepository
    {
        public CitiesServerRepository(AlejandriaContext context) : base(context)
        {

        }

        public Task<IList<CitiesCountry>> GetAllCities()
        {
            Devon4NetLogger.Debug($"GetAllCities method from CitiesServerRepository");
            return Get();
        }

        public async Task<CitiesCountry> GetInformationFromOneCity(string city)
        {
            Devon4NetLogger.Debug($"GetInformationFromOneCity method from CitiesServerRepository");
            return await GetFirstOrDefault(t => t.City == city).ConfigureAwait(false);
        }

        public Task<CitiesCountry> Create(CitiesServerDto newItem)
        {
            Devon4NetLogger.Debug($"Create method from CitiesServerRepository");
            var city = new CitiesCountry
            {
                City = newItem.City,
                Country = newItem.Country
            };

            return Create(city);
        }

        public async Task<bool> DeleteCity(string city)
        {
            Devon4NetLogger.Debug($"Create method from DeleteCity");
            var deleted = await Delete(t => t.City == city).ConfigureAwait(false);

            if (deleted)
            {
                return deleted;
            }

            throw new ApplicationException($"The City entity {city} has not been deleted.");
        }
    }
}
