using AutoMapper;
using FluentValidation;
using LibraryAPI.DTOs;

namespace LibraryAPI.Validations
{
    public class BookUpdateValidation : AbstractValidator<UpdateBookDTO>
    {
        public BookUpdateValidation()
        {
            RuleFor(model => model.Title).NotEmpty();
            RuleFor(model => model.Description).NotEmpty();
            RuleFor(model => model.Author).NotEmpty();
            RuleFor(model => model.Genre).NotEmpty();
            RuleFor(model => model.NumberInStock).NotEmpty();
        }
    }
}
