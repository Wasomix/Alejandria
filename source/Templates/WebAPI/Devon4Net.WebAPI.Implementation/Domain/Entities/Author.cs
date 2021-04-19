using System;
using System.Collections.Generic;

namespace Devon4Net.WebAPI.Implementation.Domain.Entities
{
    public partial class Author
    {
        public Author()
        {
            AuthorBook = new HashSet<AuthorBook>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }

        public virtual ICollection<AuthorBook> AuthorBook { get; set; }
    }
}
