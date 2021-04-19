using Devon4Net.Domain.UnitOfWork.Repository;
using Devon4Net.Infrastructure.Log;
using Devon4Net.Infrastructure.Utils;
using Devon4Net.WebAPI.Implementation.Business.AuthorManagement.Dto;
using Devon4Net.WebAPI.Implementation.Domain.Database;
using Devon4Net.WebAPI.Implementation.Domain.Entities;
using Devon4Net.WebAPI.Implementation.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Devon4Net.WebAPI.Implementation.Data.Repositories
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(AlejandriaContext context) : base(context)
        {

        }

        /// <summary>
        /// Get Author method
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Task<IList<Author>> GetAuthor(Expression<Func<Author, bool>> predicate = null)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            Devon4NetLogger.Debug($"{m.ReflectedType.Name} method from {m.Name}" +
                $" AuthorRespository");
            return Get(predicate);
        }

        public async Task<Author> GetAuthorIfItExist(string authorName)
        {
            return await GetFirstOrDefault(t => t.Name == authorName).ConfigureAwait(false);
        }

        public async Task<bool> DeleteAuthorByName(string authorName)
        {
            Devon4NetLogger.Debug($"DeleteAuthorByName method from repository AuthorRepository with value : {authorName}");
            var deleted = await Delete(t => t.Name == authorName).ConfigureAwait(false);

            if (deleted)
            {
                return deleted;
            }

            throw new ApplicationException($"The Author entity {authorName} has not been deleted.");
        }

        public Task<Author> Create(AuthorDto newAuthor)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            Devon4NetLogger.Debug($"{m.ReflectedType.Name} method from repository " +
                $"BookService with value : {newAuthor}");

            var author = new Author
            {
                Name = newAuthor._name,
                Surname = newAuthor._surname,
                Email = newAuthor._email,
                Phone = newAuthor._phone
            };

#if ENABLE // TODO: Add it correctly
            var result = AuthorValidator.Validate(newAuthor);

            if (!result.IsValid)
            {
                throw new ArgumentException($"The 'Description' field can not be null.{result.Errors}");
            }
#endif

            return Create(author);
        }
    }
}
