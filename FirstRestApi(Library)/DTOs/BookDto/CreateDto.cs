using FirstRestApi_Library_.Models;
using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations;

namespace FirstRestApi_Library_.DTOs.BookDto
{
    public class CreateDto
    {
        public int Id { get; set; }
        //[Required]
        public string Name { get; set; }
        //[Required]
        public double Price { get; set; }
    }

    public class BookValidator : AbstractValidator<CreateDto>
    {
        public BookValidator()
        {
            RuleFor(x => x.Name).MinimumLength(2).WithMessage("2-den az ola bilmez")
                                .MaximumLength(50).WithMessage("50-den cox ola bilmez")
                                .NotNull().WithMessage("Name is Required");

            RuleFor(x => x.Price).GreaterThanOrEqualTo(0).WithMessage("0-dan asagi ola bilmez")
                                 .NotNull().WithMessage("Price is Required");
        }
    }
}
