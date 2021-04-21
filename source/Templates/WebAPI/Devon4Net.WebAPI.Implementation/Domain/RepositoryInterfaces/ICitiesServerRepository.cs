using Devon4Net.Domain.UnitOfWork.Repository;
using Devon4Net.WebAPI.Implementation.Business.CitiesServerManagement.Dto;
using Devon4Net.WebAPI.Implementation.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Devon4Net.WebAPI.Implementation.Domain.RepositoryInterfaces
{
    public interface ICitiesServerRepository : IRepository<CitiesCountry>
    {
        public Task<IList<CitiesCountry>> GetAllCities();
        public Task<CitiesCountry> GetInformationFromOneCity(string city);
        public Task<CitiesCountry> Create(CitiesServerDto newItem);
        public Task<bool> DeleteCity(string city);
    }
}
