using Devon4Net.Domain.UnitOfWork.Service;
using Devon4Net.Domain.UnitOfWork.UnitOfWork;
using Devon4Net.Infrastructure.Log;
using Devon4Net.Infrastructure.Utils;
using Devon4Net.WebAPI.Implementation.Business.BookManagement.Converters;
using Devon4Net.WebAPI.Implementation.Business.BookManagement.Dto;
using Devon4Net.WebAPI.Implementation.Domain.Database;
using Devon4Net.WebAPI.Implementation.Domain.Entities;
using Devon4Net.WebAPI.Implementation.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Devon4Net.WebAPI.Implementation.Business.BookManagement.Service
{
    /// <summary>
    /// Book service implementation
    /// </summary>
    public class BookService : Service<AlejandriaContext>, IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IUnitOfWork<AlejandriaContext> uoW) : base(uoW)
        {
            _bookRepository = uoW.Repository<IBookRepository>();
        }

        public async Task<IEnumerable<BookDto>> GetAllBooks(/*Expression<Func<Book, bool>> predicate = null*/)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            Devon4NetLogger.Debug($"{m.ReflectedType.Name} method from service {m.Name}");
            var result = await _bookRepository.GetAllBooks(/*predicate*/).ConfigureAwait(false);
            return result.Select(BookConverter.ModelToDto);         
        }

        private bool IsNotBookInTheDdbb<T>(T book)
        {
            return MemoryChecker.IsMemoryNotAllocatedToVariable<T>(book);
        }

        public async Task<BookDto> GetBookByTitle(string bookTitle)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            Devon4NetLogger.Debug($"{m.ReflectedType.Name} method from service {m.Name}");
            var book = await _bookRepository.GetBookByTitleIfItExist(bookTitle).ConfigureAwait(false);

            if (IsNotBookInTheDdbb(book))
            {
                throw new ArgumentException($"The provided book title {bookTitle} does not exists");
            } else
            {
                return BookConverter.ModelToDto(book);
            }            
        }

        public async Task<IEnumerable<BookDto>> GetBooksByGenere(string bookGenere)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            Devon4NetLogger.Debug($"{m.ReflectedType.Name} method from service {m.Name}");
            var books = await _bookRepository.GetBooksByGenereIfTheyExist(bookGenere).ConfigureAwait(false);

            if (IsNotBookInTheDdbb<IList<Book>>(books))
            {
                throw new ArgumentException($"The provided book genere {bookGenere} does not exists");
            }
            else
            {
                return books.Select(BookConverter.ModelToDto); 
            }
        }

        public async Task<BookDto> CreateBook(BookDto newBook)
        {
            Devon4NetLogger.Debug($"CreateBook method from service BookService with value : {newBook}");

            CheckIfAnyOfTheStringsAreNotCorrect(newBook);

            return BookConverter.ModelToDto(await _bookRepository.Create(newBook).ConfigureAwait(false));
        }

        public async Task<Book> CreateBookAndGetModel(BookDto newBook)
        {
            Devon4NetLogger.Debug($"CreateBook method from service BookService with value : {newBook}");

            CheckIfAnyOfTheStringsAreNotCorrect(newBook);

            return await _bookRepository.Create(newBook).ConfigureAwait(false);
        }

        private void CheckIfAnyOfTheStringsAreNotCorrect(BookDto newBook)
        {
            if (IsStringNotCorrect(newBook._title) || IsStringNotCorrect(newBook._summary) || IsStringNotCorrect(newBook._genere))
            {
                throw new ArgumentException("One of the new book field can not be null.");
            }
        }

        private bool IsStringNotCorrect(string stringToTest)
        {
            return !IsStringCorrect(stringToTest);
        }

        private bool IsStringCorrect(string stringToTest)
        {
            bool stringIsCorrect = true;
            if (string.IsNullOrEmpty(stringToTest) || string.IsNullOrWhiteSpace(stringToTest))
            {
                stringIsCorrect = false;
            }

            return stringIsCorrect;
        }
    }
}
