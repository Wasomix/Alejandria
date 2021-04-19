using Devon4Net.Infrastructure.FluentValidation;
using Devon4Net.WebAPI.Implementation.Domain.Entities;
using FluentValidation;


namespace Devon4Net.WebAPI.Implementation.Business.BookManagement.Validator
{
    public class BooksFluentValidator : CustomFluentValidator<Book>
    {
        public BooksFluentValidator(bool launchExceptionWhenError) : base(launchExceptionWhenError)
        {
        }

        public override void CustomValidate()
        {
            RuleFor(book => book.Title).NotNull();
            RuleFor(book => book.Title).NotEmpty();
            RuleFor(book => book.Summary).NotNull();
            RuleFor(book => book.Summary).NotEmpty();
            RuleFor(book => book.Genere).NotNull();
            RuleFor(book => book.Genere).NotEmpty();
        }
    }
}
