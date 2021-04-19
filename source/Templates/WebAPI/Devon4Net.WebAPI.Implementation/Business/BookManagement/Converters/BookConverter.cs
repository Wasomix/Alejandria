﻿using Devon4Net.WebAPI.Implementation.Business.BookManagement.Dto;
using Devon4Net.WebAPI.Implementation.Domain.Entities;

namespace Devon4Net.WebAPI.Implementation.Business.BookManagement.Converters
{
    /// <summary>
    /// BookConverter
    /// </summary>
    public static class BookConverter
    {
        /// <summary>
        /// ModelToDto Book transformation
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static BookDto ModelToDto(Book item)
        {
            if (item == null) return new BookDto();

            return new BookDto
            {
                _title = item.Title,
                _summary = item.Summary,
                _genere = item.Genere,
                _validityDate  = item.ValidityDate
            };
        }
    }
}
