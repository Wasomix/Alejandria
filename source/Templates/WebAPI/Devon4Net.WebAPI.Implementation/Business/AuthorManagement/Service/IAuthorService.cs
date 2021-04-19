using Devon4Net.WebAPI.Implementation.Business.AuthorManagement.Dto;
using Devon4Net.WebAPI.Implementation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Devon4Net.WebAPI.Implementation.Business.AuthorManagement.Service
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorDto>> GetAuthor(Expression<Func<Author, bool>> predicate = null);

        Task<bool> DeleteAuthorByName(string authorName);

        Task<AuthorDto> CreateAuthor(AuthorDto newAuthor);

        Task<AuthorBookDto> Publish(AuthorBookDto newBookToPublish);
    }
}
