using Devon4Net.Domain.UnitOfWork.Service;
using Devon4Net.Domain.UnitOfWork.UnitOfWork;
using Devon4Net.Infrastructure.Log;
using Devon4Net.WebAPI.Implementation.Business.AuthorManagement.Dto;
using Devon4Net.WebAPI.Implementation.Domain.Database;
using Devon4Net.WebAPI.Implementation.Domain.Entities;
using Devon4Net.WebAPI.Implementation.Domain.RepositoryInterfaces;
using Devon4Net.WebAPI.Implementation.Business.AuthorManagement.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Devon4Net.Infrastructure.Utils;
using Devon4Net.WebAPI.Implementation.Business.BookManagement.Converters;
using Devon4Net.WebAPI.Implementation.Options;
using Microsoft.Extensions.Options;
using Devon4Net.Infrastructure.CircuitBreaker.Handler;
using Devon4Net.Infrastructure.CircuitBreaker.Common;
using Devon4Net.Infrastructure.CircuitBreaker.Common.Enums;
using Devon4Net.Infrastructure.Common.Options.CircuitBreaker;
using Devon4Net.WebAPI.Implementation.Business.BookManagement.Dto;
using System.IO;


using System.Net.Http;

namespace Devon4Net.WebAPI.Implementation.Business.AuthorManagement.Service
{
    public class AuthorService : Service<AlejandriaContext>, IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookRepository _bookRepository;
        private AlejandriaOptions _alejandriaOptions;
        private CircuitBreakerOptions _circuitBreakerOptions;
        private IHttpClientHandler _httpClientHandler;

        public AuthorService(IUnitOfWork<AlejandriaContext> uoW, 
                             IOptions<AlejandriaOptions> alejandriaOptions,
                             IOptions<CircuitBreakerOptions> circuitBreakerOptions,
                             IHttpClientHandler httpClientHandler) : base (uoW)
        {
            _authorRepository = uoW.Repository<IAuthorRepository>();
            _bookRepository = uoW.Repository<IBookRepository>();
            InitializeAlejandriaOptions(alejandriaOptions);
            InitializeCircuitBreaker(httpClientHandler, circuitBreakerOptions);
        }

        private void InitializeAlejandriaOptions(IOptions<AlejandriaOptions> alejandriaOptions)
        {
            _alejandriaOptions = new AlejandriaOptions();
            _alejandriaOptions.Validity = alejandriaOptions.Value.Validity;
        }

        private void InitializeCircuitBreaker(IHttpClientHandler httpClientHandler,
                                              IOptions<CircuitBreakerOptions> circuitBreakerOptions)
        {
            _httpClientHandler = httpClientHandler;
            _circuitBreakerOptions = circuitBreakerOptions.Value;
        }
        
        /// <summary>
        /// Gets the Author
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<IEnumerable<AuthorDto>> GetAuthor(Expression<Func<Author, bool>> predicate = null)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            Devon4NetLogger.Debug($"{m.ReflectedType.Name} method from service {m.Name}");
            var result = await _authorRepository.GetAuthor(predicate).ConfigureAwait(false);
            return result.Select(AuthorConverter.ModelToDto);
        }

        private bool IsNotAuthorInTheDdbb(Author author)
        {
            return MemoryChecker.IsMemoryNotAllocatedToVariable<Author>(author);
        }

        public async Task<bool> DeleteAuthorByName(string authorName)
        {
            Devon4NetLogger.Debug($"DeleteAuthorByName method from service AuthorService with value : {authorName}");
            var author = await _authorRepository.GetAuthorIfItExist(authorName).ConfigureAwait(false);

            if (IsNotAuthorInTheDdbb(author))
            {
                throw new ArgumentException($"The provided author {authorName} does not exists");
            }
            else
            {
                return await _authorRepository.DeleteAuthorByName(authorName).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// CreateAuthor
        /// </summary>
        /// <param name="newAuthor"></param>
        /// <returns></returns>
        public async Task<AuthorDto> CreateAuthor(AuthorDto newAuthor)
        {
            Devon4NetLogger.Debug($"CreateAuthor method from service BookService with value : {newAuthor}");
            return AuthorConverter.ModelToDto(await _authorRepository.Create(newAuthor).ConfigureAwait(false));
        }

        public async Task<AuthorBookDto> Publish(AuthorBookDto newBookToPublish)
        {
            //Author author  = await _authorRepository.Create(AuthorBookConverter.AuthorBookDtoToAuthorDto(newBookToPublish)).ConfigureAwait(false);
            BookDto book = await CreateBook(AuthorBookConverter.AuthorBookDtoToBookDto(newBookToPublish)).ConfigureAwait(false); // Use HttpClient for that
            //PrepareAuthorBookModelFromAuthorModelAndBookModel(author, book);
            //CreateAuthorBook();

            /*var author = AuthorConverter.ModelToDto(await _authorRepository.Create(newBookToPublish._authorDto).ConfigureAwait(false));
            var book = BookConverter.ModelToDto(await _bookRepository.Create(newBookToPublish._bookDto).ConfigureAwait(false));          

            AuthorBookDto publishedBook = new AuthorBookDto(author, book);*/

            //return publishedBook;
            return new AuthorBookDto();
        }

        private async Task<Author> CreateAndGetAuthor(AuthorDto newAuthor)
        {
            return await _authorRepository.Create(newAuthor).ConfigureAwait(false);
        }

        private async Task<BookDto> CreateBook(BookDto newBook)
        {
            // TODO: Put the correct call
            string mediaType = MediaType.ApplicationJson;
            string endPoint = "Books"; 
            var httpResponse = await _httpClientHandler.Send(System.Net.Http.HttpMethod.Get, endPoint,
                "/books", null, mediaType, true, false, null);
            var listOfBooks = GetObjectFromHttpResponse<List<BookDto>>(in httpResponse);

            string title = "";
            string genere = "";
            string summary = "";
            foreach (BookDto book in listOfBooks)
            {
                title = book._title;
                genere = book._genere;
                summary = book._summary;
            }

            return BookConverter.ModelToDto(await _bookRepository.Create(newBook).ConfigureAwait(false));
        }

        private T GetObjectFromHttpResponse<T>(in HttpResponseMessage httpResponse)
        {
            var responseBody = httpResponse.Content.ReadAsStringAsync().Result;

            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseBody); ;
        }

        private AuthorBook PrepareAuthorBookStructureFromAuthorDtoAndBookDto(AuthorDto newAuthor,
                                                                             BookDto newBook)
        {
            Author a1 = new Author();
            Book b1 = new Book();
            DateTime pdate = new DateTime();

            return new AuthorBook
            {
                AuthorId = a1.Id,
                BookId = b1.Id,
                PublishDate = pdate,
                ValidityDate = b1.ValidityDate
            };
        }
    }
}
