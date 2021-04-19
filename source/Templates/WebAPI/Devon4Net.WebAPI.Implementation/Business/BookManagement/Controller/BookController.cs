using Devon4Net.Infrastructure.JWT.Handlers;
using Devon4Net.Infrastructure.JWT.Common.Const;
using Devon4Net.Infrastructure.Log;
using Devon4Net.WebAPI.Implementation.Business.BookManagement.Dto;
using Devon4Net.WebAPI.Implementation.Business.BookManagement.Service;
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

namespace Devon4Net.WebAPI.Implementation.Business.BookManagement.Controller
{
    /// <summary>
    /// Book controller
    /// </summary>
    [EnableCors("CorsPolicy")]
    [ApiController]
    [Route("/v1/bookmanagement")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private IJwtHandler JwtHandler { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bookService"></param>
        public BookController(IBookService bookService,
                                IJwtHandler jwtHandler)
        {
            _bookService = bookService;
            JwtHandler = jwtHandler;
        }


        /// <summary>
        /// Performs the login of the author proces via the user/password flow
        /// This is only a sample. Please avoid any logic on the controller.
        /// </summary>
        /// <returns>LoginResponse class will provide the JWT token to securize the server calls</returns>
        [HttpPost]
        [HttpOptions]
        [AllowAnonymous]
        [Route("/loginBook")]
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
        /// Gets the entire list of Books
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/books")]
        [ProducesResponseType(typeof(List<BookDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAllBooks()
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            Devon4NetLogger.Debug($"Executing {m.ReflectedType.Name}" +
                " from controller {m.Name}");
            return Ok(await _bookService.GetAllBooks().ConfigureAwait(false));
        }

        /// <summary>
        /// Get book by title
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("/booksbytitle")]
        [Authorize(AuthenticationSchemes = AuthConst.AuthenticationScheme, Roles = AuthConst.Usuario)]
        [ProducesResponseType(typeof(BookDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetBookByTitle(string bookTitle)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            Devon4NetLogger.Debug($"Executing {m.ReflectedType.Name}" +
                " from controller {m.Name}");
            return Ok(await _bookService.GetBookByTitle(bookTitle).ConfigureAwait(false));
        }

        /// <summary>
        /// Get book by genere
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("/booksbygenere")]
        [Authorize(AuthenticationSchemes = AuthConst.AuthenticationScheme, Roles = AuthConst.Usuario)]
        [ProducesResponseType(typeof(BookDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetBooksByGenere(string bookGenere)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            Devon4NetLogger.Debug($"Executing {m.ReflectedType.Name}" +
                " from controller {m.Name}");
            return Ok(await _bookService.GetBooksByGenere(bookGenere).ConfigureAwait(false));
        }

        /// <summary>
        /// Creates the Book
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("/Add_Book")]
        [ProducesResponseType(typeof(BookDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Create(BookDto newBook)
        {
            Devon4NetLogger.Debug("Executing GetTodo from controller TodoController");
            var result = await _bookService.CreateBook(newBook).ConfigureAwait(false);
            return StatusCode(StatusCodes.Status201Created, result);
        }


        /// <summary>
        /// Creates the Book and gets the model 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("/Add_Book_and_get_model")]
        [ProducesResponseType(typeof(BookDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateBookAndGetModel(BookDto newBook)
        {
            Devon4NetLogger.Debug("Executing GetTodo from controller TodoController");
            var result = await _bookService.CreateBookAndGetModel(newBook).ConfigureAwait(false);
            return StatusCode(StatusCodes.Status201Created, result);
        }
    }
}
