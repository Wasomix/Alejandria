using System;
using System.Collections.Generic;

namespace Devon4Net.WebAPI.Implementation.Domain.Entities
{
    public partial class AuthorBook
    {
        public Guid Id { get; set; }
        public Guid? AuthorId { get; set; }
        public Guid? BookId { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime ValidityDate { get; set; }

        public virtual Author Author { get; set; }
        public virtual Book Book { get; set; }
    }
}
