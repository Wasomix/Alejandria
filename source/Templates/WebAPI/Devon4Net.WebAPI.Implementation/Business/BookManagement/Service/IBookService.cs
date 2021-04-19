using Devon4Net.WebAPI.Implementation.Business.BookManagement.Dto;
using Devon4Net.WebAPI.Implementation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Devon4Net.WebAPI.Implementation.Business.BookManagement.Service
{
    public interface IBookService
    {
        /// <summary>
        /// GetBook
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IEnumerable<BookDto>> GetAllBooks(/*Expression<Func<Book, bool>> predicate = null*/);

        /// <summary>
        /// GetBookByTitle
        /// </summary>
        /// <param name="bookTitle"></param>
        /// <returns></returns>
        Task<BookDto> GetBookByTitle(string bookTitle);

        /// <summary>
        /// GetBookByGenere
        /// </summary>
        /// <param name="bookGenere"></param>
        /// <returns></returns>
        Task<IEnumerable<BookDto>> GetBooksByGenere(string bookGenere);

        Task<BookDto> CreateBook(BookDto newBook);
        Task<Book> CreateBookAndGetModel(BookDto newBook);
    }
}
