using FluentValidation;
using LibraryAPI.DTOs;

namespace LibraryAPI.Validations
{
    public class BookCreateValidation : AbstractValidator<CreateBookDTO>
    {
        public BookCreateValidation()
        {
            RuleFor(model => model.Title).NotEmpty();
            RuleFor(model => model.Author).NotEmpty();
            RuleFor(model => model.Genre).NotEmpty();
            RuleFor(model => model.Description).NotEmpty();
            RuleFor(model => model.PublishingYear).NotEmpty()/*.LessThanOrEqualTo(DateTime.Now.Year)*/;
            RuleFor(model => model.NumberInStock).NotEmpty();
        }
    }
}
