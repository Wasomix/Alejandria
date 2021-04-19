using Devon4Net.Domain.UnitOfWork.Repository;
using Devon4Net.Infrastructure.Log;
using Devon4Net.Infrastructure.Utils;
using Devon4Net.WebAPI.Implementation.Business.BookManagement.Dto;
using Devon4Net.WebAPI.Implementation.Business.BookManagement.Validator;
using Devon4Net.WebAPI.Implementation.Domain.Database;
using Devon4Net.WebAPI.Implementation.Domain.Entities;
using Devon4Net.WebAPI.Implementation.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Devon4Net.WebAPI.Implementation.Data.Repositories
{
    /// <summary>
    /// Repository implementation for the Book
    /// </summary>
    public class BookRepository : Repository<Book>, IBookRepository
    {
        //private BooksFluentValidator BooksValidator { get; }
        /*
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public BookRepository(AlejandriaContext context, BooksFluentValidator booksValidator) : base(context)
        {
            BooksValidator = booksValidator;
        }*/

        public BookRepository(AlejandriaContext context) : base(context)
        {
            
        }

        /// <summary>
        /// Get Book method
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Task<IList<Book>> GetAllBooks(Expression<Func<Book, bool>> predicate = null)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            Devon4NetLogger.Debug($"{m.ReflectedType.Name} method from {m.Name}" +
                $" BookService");
            return Get(/*predicate*/);
        }

        public async Task<Book> GetBookByTitleIfItExist(string bookTitle)
        {
            return await GetFirstOrDefault(t => t.Title == bookTitle).ConfigureAwait(false);
        }

        public async Task<IList<Book>> GetBooksByGenereIfTheyExist(string bookGenere)
        {
            return await Get(t => t.Genere == bookGenere).ConfigureAwait(false);
        }

        /// <summary>
        /// Creates the Book
        /// </summary>
        /// <param name="newBook"></param>
        /// <returns></returns>
        public Task<Book> Create(BookDto newBook)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            Devon4NetLogger.Debug($"{m.ReflectedType.Name} method from repository " +
                $"BookService with value : {newBook}");

            var book = new Book {
                Title = newBook._title, 
                Genere = newBook._genere ,
                Summary = newBook._summary,
                ValidityDate = newBook._validityDate
            };

            /*var result = BooksValidator.Validate(book);

            if (!result.IsValid)
            {
                throw new ArgumentException($"The 'Description' field can not be null.{result.Errors}");
            }*/

            return Create(book);
        }
    }
}
