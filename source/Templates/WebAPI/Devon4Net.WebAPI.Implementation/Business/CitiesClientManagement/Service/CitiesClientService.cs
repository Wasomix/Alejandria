using Devon4Net.Infrastructure.CircuitBreaker.Common.Enums;
using Devon4Net.Infrastructure.CircuitBreaker.Handler;
using Devon4Net.Infrastructure.Common.Options.CircuitBreaker;
using Devon4Net.WebAPI.Implementation.Business.CitiesClientManagement.Converter;
using Devon4Net.WebAPI.Implementation.Business.CitiesClientManagement.Dto;
using Devon4Net.WebAPI.Implementation.Business.CitiesServerManagement.Dto;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Devon4Net.WebAPI.Implementation.Business.CitiesClientManagement.Service
{
    public class CitiesClientService : ICitiesClientService
    {
        IHttpClientHandler _httpClientHandler;
        IOptions<CircuitBreakerOptions> _circuitBreakerOptions;
        private const string _mediaType = MediaType.ApplicationJson;
        private const string _endPoint = "CitiesClient";

        public CitiesClientService(IHttpClientHandler httpClientHandler,
                                   IOptions<CircuitBreakerOptions> circuitBreakerOptions)
        {
            _httpClientHandler     = httpClientHandler;
            _circuitBreakerOptions = circuitBreakerOptions;
        }

        public async Task<IEnumerable<CitiesClientDto>> GetAllCities()
        {
            var listOfCities = await _httpClientHandler.Send<List<CitiesServerDto>>(
                System.Net.Http.HttpMethod.Get, _endPoint, "/server_get_all_cities", null, 
                _mediaType, null, true, true).ConfigureAwait(false);

            return CitiesClientConverter.FromListOfCitiesServerToClient(listOfCities);
        }

        // TODO: Finish this one
        public Task<CitiesClientDto> GetInformationFromOneCity(string city)
        {
            CitiesClientDto mockData = new CitiesClientDto { City = city, Country = "Germany" };

            return Task.FromResult(mockData);
        }

        public async Task<CitiesClientDto> CreateNewCity(CitiesClientDto newItem)
        {
            var city = await _httpClientHandler.Send<CitiesClientDto>(
            System.Net.Http.HttpMethod.Post, _endPoint, "/server_add_city", newItem,
            _mediaType, null, true, false).ConfigureAwait(false);

            return city;
        }
    }
}
