using Devon4Net.Infrastructure.JWT.Common.Const;
using Devon4Net.Infrastructure.JWT.Handlers;
using Devon4Net.Infrastructure.Log;
using Devon4Net.WebAPI.Implementation.Business.AuthorManagement.Dto;
using Devon4Net.WebAPI.Implementation.Business.AuthorManagement.Service;
using Devon4Net.WebAPI.Implementation.Business.UserManagement.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Devon4Net.WebAPI.Implementation.Business.AuthorManagement.Controller
{
    /// <summary>
    /// Author controller
    /// </summary>
    [EnableCors("CorsPolicy")]
    [ApiController]
    [Route("/v1/authormanagement")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService AuthorService;
        private IJwtHandler JwtHandler { get; set; }

        public AuthorController(IAuthorService authorService, 
                                IJwtHandler jwtHandler)
        {
            AuthorService = authorService;
            JwtHandler    = jwtHandler;
        }

        /// <summary>
        /// Performs the login of the author proces via the user/password flow
        /// This is only a sample. Please avoid any logic on the controller.
        /// </summary>
        /// <returns>LoginResponse class will provide the JWT token to securize the server calls</returns>
        [HttpPost]
        [HttpOptions]
        [AllowAnonymous]
        [Route("/loginAuthor")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Login(string user, string password, string role)
        {
            Devon4NetLogger.Debug("Executing Login from controller AuthController");

            var token = JwtHandler.CreateClientToken(new List<Claim>
            {
                new Claim(ClaimTypes.Role, role), //AuthConst.Author), //
                new Claim(ClaimTypes.NameIdentifier,Guid.NewGuid().ToString()),
            });

            return Ok(new LoginResponse { Token = token });
        }

        /// <summary>
        /// Gets all authors
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/authors")]
        [Authorize(AuthenticationSchemes = AuthConst.AuthenticationScheme, Roles = AuthConst.Librarian)]
        [ProducesResponseType(typeof(List<AuthorDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAuthors()
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            Devon4NetLogger.Debug($"Executing {m.ReflectedType.Name}" +
                " from controller {m.Name}");
            return Ok(await AuthorService.GetAuthor().ConfigureAwait(false));
        }

        /// <summary>
        /// Creates the Author
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("/Add_Author")]
        [Authorize(AuthenticationSchemes = AuthConst.AuthenticationScheme, Roles = AuthConst.Librarian)]
        [ProducesResponseType(typeof(AuthorDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Create(AuthorDto newAuthor)
        {
            Devon4NetLogger.Debug("Executing GetTodo from controller TodoController");
            return Ok(await AuthorService.CreateAuthor(newAuthor).ConfigureAwait(false));
        }

        /// <summary>
        /// Creates Book and author
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("/publish")]
        //[Authorize(AuthenticationSchemes = AuthConst.AuthenticationScheme, Roles = $"{AuthConst.Author}, {AuthConst.Librarian}")]
        [Authorize(AuthenticationSchemes = AuthConst.AuthenticationScheme, Roles = "Author, Librarian")]
        [ProducesResponseType(typeof(AuthorBookDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Publish(AuthorBookDto newBookToPublish)
        {
            Devon4NetLogger.Debug("Executing Publish from controller AuthorController");
            return Ok(await AuthorService.Publish(newBookToPublish).ConfigureAwait(false));
        }

        /// <summary>
        /// Deletes the Author provided the author's name
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("/delete")]
        [Authorize(AuthenticationSchemes = AuthConst.AuthenticationScheme, Roles = AuthConst.Librarian)]
        [ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteAuthorByName(string authorName)
        {
            Devon4NetLogger.Debug("Executing GetTodo from controller AuthorController");
            return Ok(await AuthorService.DeleteAuthorByName(authorName).ConfigureAwait(false));
        }
    }
}
