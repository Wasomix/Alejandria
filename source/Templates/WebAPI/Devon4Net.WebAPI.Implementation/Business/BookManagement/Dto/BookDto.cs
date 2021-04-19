using System;

namespace Devon4Net.WebAPI.Implementation.Business.BookManagement.Dto
{
    public class BookDto
    {
        public string _title { get; set; }
        public string _summary { get; set; }
        public string _genere { get; set; }
        public DateTime _validityDate { get; set; }
    }
}
