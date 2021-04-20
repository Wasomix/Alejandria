using Devon4Net.Infrastructure.Log;
using Devon4Net.WebAPI.Implementation.Business.CitiesServerManagement.Dto;
using Devon4Net.WebAPI.Implementation.Business.CitiesServerManagement.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace Devon4Net.WebAPI.Implementation.Business.CitiesServerManagement.Controller
{
    /// <summary>
    /// CitiesServer controller
    /// </summary>
    [EnableCors("CorsPolicy")]
    [ApiController]
    [Route("/v1/citiesservermanagement")]
    public class CitiesServerController : ControllerBase
    {
        private ICitiesServerService _citiesServerServiceHandler;

        public CitiesServerController(ICitiesServerService citiesServerServiceHandler)
        {
            _citiesServerServiceHandler = citiesServerServiceHandler;
        }

        /// <summary>
        /// Gets all cities
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/server_get_all_cities")]
        //[Authorize(AuthenticationSchemes = AuthConst.AuthenticationScheme, Roles = AuthConst.Librarian)]
        [ProducesResponseType(typeof(List<CitiesServerDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAllCities()
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            Devon4NetLogger.Debug($"Executing {m.ReflectedType.Name}" +
                " from controller {m.Name}");
            return Ok(await _citiesServerServiceHandler.GetAllCities().ConfigureAwait(false));
        }

        /// <summary>
        /// Get all information related to one city
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/server_get_city_by_id")]
        //[Authorize(AuthenticationSchemes = AuthConst.AuthenticationScheme, Roles = AuthConst.Librarian)]
        [ProducesResponseType(typeof(List<CitiesServerDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetInformationFromOneCity(string city)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            Devon4NetLogger.Debug($"Executing {m.ReflectedType.Name}" +
                " from controller {m.Name}");
            return Ok(await _citiesServerServiceHandler.GetInformationFromOneCity(city).ConfigureAwait(false));
        }

        /// <summary>
        /// Create new city
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("/server_add_city")]
        //[Authorize(AuthenticationSchemes = AuthConst.AuthenticationScheme, Roles = AuthConst.Librarian)]
        [ProducesResponseType(typeof(CitiesServerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateNewCity(CitiesServerDto newCity)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            Devon4NetLogger.Debug($"Executing {m.ReflectedType.Name}" +
                " from controller {m.Name}");
            return Ok(await _citiesServerServiceHandler.CreateNewCity(newCity).ConfigureAwait(false));
        }
    }
}
