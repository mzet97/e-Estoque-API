using e_Estoque_API.Core.Entities;
using FluentValidation;

namespace e_Estoque_API.Core.Validations;

public class AddressValidation : AbstractValidator<Address>
{
    public AddressValidation()
    {
        RuleFor(x => x.Street)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .Length(3, 80)
            .WithMessage("The {PropertyName} need to have between {MinLength} and {MaxLength} characters");

        RuleFor(x => x.Number)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .Length(3, 80)
            .WithMessage("The {PropertyName} need to have between {MinLength} and {MaxLength} characters");

        RuleFor(x => x.Complement)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .Length(3, 80)
            .WithMessage("The {PropertyName} need to have between {MinLength} and {MaxLength} characters");

        RuleFor(x => x.Neighborhood)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .Length(3, 80)
            .WithMessage("The {PropertyName} need to have between {MinLength} and {MaxLength} characters");

        RuleFor(x => x.District)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .Length(3, 80)
            .WithMessage("The {PropertyName} need to have between {MinLength} and {MaxLength} characters");

        RuleFor(x => x.City)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .Length(3, 80)
            .WithMessage("The {PropertyName} need to have between {MinLength} and {MaxLength} characters");

        RuleFor(x => x.County)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .Length(3, 80)
            .WithMessage("The {PropertyName} need to have between {MinLength} and {MaxLength} characters");

        RuleFor(x => x.ZipCode)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .Length(3, 80)
            .WithMessage("The {PropertyName} need to have between {MinLength} and {MaxLength} characters");

        RuleFor(x => x.Latitude)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .Length(3, 80)
            .WithMessage("The {PropertyName} need to have between {MinLength} and {MaxLength}");
    }
}