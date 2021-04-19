using Devon4Net.WebAPI.Implementation.Business.AuthorManagement.Dto;
using Devon4Net.WebAPI.Implementation.Domain.Entities;


namespace Devon4Net.WebAPI.Implementation.Business.AuthorManagement.Converters
{
    public static class AuthorConverter
    {
        /// <summary>
        /// ModelToDto Author transformation
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static AuthorDto ModelToDto(Author item)
        {
            if (item == null) return new AuthorDto();

            return new AuthorDto
            {
                _name = item.Name,
                _surname = item.Surname,
                _email = item.Email,
                _phone = item.Phone
            };
        }
    }
}
