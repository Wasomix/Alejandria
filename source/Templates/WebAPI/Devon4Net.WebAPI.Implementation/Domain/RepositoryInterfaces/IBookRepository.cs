using Devon4Net.Domain.UnitOfWork.Repository;
using Devon4Net.WebAPI.Implementation.Business.BookManagement.Dto;
using Devon4Net.WebAPI.Implementation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Devon4Net.WebAPI.Implementation.Domain.RepositoryInterfaces
{
    /// <summary>
    /// IBookRepository interface
    /// </summary>
    public interface IBookRepository
    {
        public Task<IList<Book>> GetAllBooks(Expression<Func<Book, bool>> predicate = null);
        public Task<Book> GetBookByTitleIfItExist(string bookTitle);
        public Task<IList<Book>> GetBooksByGenereIfTheyExist(string bookGenere);
        public Task<Book> Create(BookDto newBook);
    }
}
