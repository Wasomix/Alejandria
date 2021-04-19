using System;
using System.Collections.Generic;

namespace Devon4Net.WebAPI.Implementation.Domain.Entities
{
    public partial class Book
    {
        public Book()
        {
            AuthorBook = new HashSet<AuthorBook>();
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Genere { get; set; }
        public DateTime ValidityDate { get; set; }

        public virtual ICollection<AuthorBook> AuthorBook { get; set; }
    }
}
