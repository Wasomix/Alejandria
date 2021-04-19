// This class is responsible for converting model to dto and AuthorBookDto
// to AuthorDto and AuthorBookDto to BookDto. 
// TODO: I have to separate this class into 

using Devon4Net.WebAPI.Implementation.Business.AuthorManagement.Dto;
using Devon4Net.WebAPI.Implementation.Business.BookManagement.Converters;
using Devon4Net.WebAPI.Implementation.Business.BookManagement.Dto;
using Devon4Net.WebAPI.Implementation.Domain.Entities;


namespace Devon4Net.WebAPI.Implementation.Business.AuthorManagement.Converters
{
    public static class AuthorBookConverter
    {
        public static AuthorDto AuthorBookDtoToAuthorDto(AuthorBookDto item)
        {
            if (item == null) return new AuthorDto();

            return new AuthorDto
            {
                _name = item._author._name,
                _surname = item._author._surname,
                _email = item._author._email,
                _phone = item._author._phone
            };               
        }

        public static BookDto AuthorBookDtoToBookDto(AuthorBookDto item)
        {
            if (item == null) return new BookDto();

            return new BookDto
            {
                _title = item._book._title,
                _summary = item._book._summary,
                _genere = item._book._genere,
                _validityDate = item._book._validityDate
            };
        }
    }
}
