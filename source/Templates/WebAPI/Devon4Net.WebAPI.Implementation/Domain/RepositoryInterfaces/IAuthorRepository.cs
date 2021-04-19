using Devon4Net.Domain.UnitOfWork.Repository;
using Devon4Net.WebAPI.Implementation.Business.AuthorManagement.Dto;
using Devon4Net.WebAPI.Implementation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Devon4Net.WebAPI.Implementation.Domain.RepositoryInterfaces
{
    public interface IAuthorRepository
    {
        public Task<IList<Author>> GetAuthor(Expression<Func<Author, bool>> predicate = null);
        public Task<bool> DeleteAuthorByName(string authorName);
        public Task<Author> GetAuthorIfItExist(string authorName);
        public Task<Author> Create(AuthorDto newAuthor);
    }
}
