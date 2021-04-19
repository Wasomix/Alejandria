using Devon4Net.WebAPI.Implementation.Business.BookManagement.Dto;
using System;

namespace Devon4Net.WebAPI.Implementation.Business.AuthorManagement.Dto
{
    public class AuthorBookDto
    {
        public AuthorDto _author { get; set; }
        public BookDto _book { get; set; }
        public DateTime PublishDate { get; set; }

        public AuthorBookDto()
        {
            _author = new AuthorDto();
            _book = new BookDto();
        }
    }
}
