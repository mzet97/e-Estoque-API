using e_Estoque_API.Core.Entities;
using e_Estoque_API.Domain.Validations;
using FluentValidation;

namespace e_Estoque_API.Core.Validations;

public class CustomerValidation : AbstractValidator<Customer>
{
    public CustomerValidation()
    {
        Include(new EntityValidation());

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .Length(3, 80)
            .WithMessage("The {PropertyName} need to have between {MinLength} and {MaxLength} characters");

        RuleFor(x => x.DocId)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .Length(3, 80)
            .WithMessage("The {PropertyName} need to have between {MinLength} and {MaxLength} characters");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .Length(3, 250)
            .WithMessage("The {PropertyName} need to have between {MinLength} and {MaxLength} characters");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .Length(3, 250)
            .WithMessage("The {PropertyName} need to have between {MinLength} and {MaxLength} characters");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .Length(3, 80)
            .WithMessage("The {PropertyName} need to have between {MinLength} and {MaxLength} characters");

        RuleFor(x => x.CustomerAddress)
            .NotNull()
            .WithMessage("{PropertyName} is required");

        RuleFor(x => x.CustomerAddress.Street)
           .NotEmpty()
           .WithMessage("{PropertyName} is required")
           .Length(3, 80)
           .WithMessage("The {PropertyName} need to have between {MinLength} and {MaxLength} characters");

        RuleFor(x => x.CustomerAddress.Number)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .Length(3, 80)
            .WithMessage("The {PropertyName} need to have between {MinLength} and {MaxLength} characters");

        RuleFor(x => x.CustomerAddress.Complement)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .Length(3, 80)
            .WithMessage("The {PropertyName} need to have between {MinLength} and {MaxLength} characters");

        RuleFor(x => x.CustomerAddress.Neighborhood)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .Length(3, 80)
            .WithMessage("The {PropertyName} need to have between {MinLength} and {MaxLength} characters");

        RuleFor(x => x.CustomerAddress.District)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .Length(3, 80)
            .WithMessage("The {PropertyName} need to have between {MinLength} and {MaxLength} characters");

        RuleFor(x => x.CustomerAddress.City)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .Length(3, 80)
            .WithMessage("The {PropertyName} need to have between {MinLength} and {MaxLength} characters");

        RuleFor(x => x.CustomerAddress.Country)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .Length(3, 80)
            .WithMessage("The {PropertyName} need to have between {MinLength} and {MaxLength} characters");

        RuleFor(x => x.CustomerAddress.ZipCode)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .Length(3, 80)
            .WithMessage("The {PropertyName} need to have between {MinLength} and {MaxLength} characters");

        RuleFor(x => x.CustomerAddress.Latitude)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .Length(3, 80)
            .WithMessage("The {PropertyName} need to have between {MinLength} and {MaxLength}");
    }
}