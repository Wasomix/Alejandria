using Devon4Net.Domain.UnitOfWork.Repository;
using Devon4Net.Infrastructure.Log;
using Devon4Net.WebAPI.Implementation.Business.AuthorManagement.Dto;
using Devon4Net.WebAPI.Implementation.Domain.Database;
using Devon4Net.WebAPI.Implementation.Domain.Entities;
using Devon4Net.WebAPI.Implementation.Domain.RepositoryInterfaces;
using System.Reflection;
using System.Threading.Tasks;

namespace Devon4Net.WebAPI.Implementation.Data.Repositories
{
    public class AuthorBookRepository : Repository<AuthorBook>, IAuthorBookRepository
    {
        public AuthorBookRepository(AlejandriaContext context) : base(context)
        {

        }

        public Task<AuthorBook> Create(AuthorBook newAuthorBook)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            Devon4NetLogger.Debug($"{m.ReflectedType.Name} method from repository " +
                $"BookService with value : {newAuthorBook}");

            var authorBook = new AuthorBook
            {
                AuthorId = newAuthorBook.AuthorId,
                BookId = newAuthorBook.BookId,
                PublishDate = newAuthorBook.PublishDate,
                ValidityDate = newAuthorBook.ValidityDate
            };

            return Create(authorBook);
        }
    }
}
