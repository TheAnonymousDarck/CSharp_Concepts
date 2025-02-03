using Backend.DTOs.Beer;
using FluentValidation;

namespace Backend.Validators;

public class BeerUpdateValidator: AbstractValidator<BeerUpdateDto>
{
    public BeerUpdateValidator()
    {
        RuleFor(x => x.Id).NotNull().WithMessage("Id is required.");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Alcohol).NotEmpty().WithMessage("Alcohol is required.");
        RuleFor(x => x.BrandId).NotEmpty().WithMessage("Brand is required.");
    }
}