﻿using Devon4Net.Infrastructure.Log;
using Devon4Net.WebAPI.Implementation.Business.CitiesClientManagement.Dto;
using Devon4Net.WebAPI.Implementation.Business.CitiesClientManagement.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Devon4Net.WebAPI.Implementation.Business.CitiesClientManagement.Controller
{
    public class CitiesClientController : ControllerBase
    {
        private ICitiesClientService _citiesClientServiceHandler;

        public CitiesClientController(ICitiesClientService citiesClientServiceHandler)
        {
            _citiesClientServiceHandler = citiesClientServiceHandler;
        }

        /// <summary>
        /// Gets all cities
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/client_get_all_cities")]
        //[Authorize(AuthenticationSchemes = AuthConst.AuthenticationScheme, Roles = AuthConst.Librarian)]
        [ProducesResponseType(typeof(List<CitiesClientDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAllCities()
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            Devon4NetLogger.Debug($"Executing {m.ReflectedType.Name}" +
                " from controller {m.Name}");
            return Ok(await _citiesClientServiceHandler.GetAllCities().ConfigureAwait(false));
        }

        /// <summary>
        /// Get all information related to one city
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/client_get_city_by_id")]
        //[Authorize(AuthenticationSchemes = AuthConst.AuthenticationScheme, Roles = AuthConst.Librarian)]
        [ProducesResponseType(typeof(List<CitiesClientDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetInformationFromOneCity(string city)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            Devon4NetLogger.Debug($"Executing {m.ReflectedType.Name}" +
                " from controller {m.Name}");
            return Ok(await _citiesClientServiceHandler.GetInformationFromOneCity(city).ConfigureAwait(false));
        }

        /// <summary>
        /// Create new city
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("/client__add_city")]
        //[Authorize(AuthenticationSchemes = AuthConst.AuthenticationScheme, Roles = AuthConst.Librarian)]
        [ProducesResponseType(typeof(CitiesClientDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateNewCity(CitiesClientDto newCity)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            Devon4NetLogger.Debug($"Executing {m.ReflectedType.Name}" +
                " from controller {m.Name}");
            return Ok(await _citiesClientServiceHandler.CreateNewCity(newCity).ConfigureAwait(false));
        }
    }
}
