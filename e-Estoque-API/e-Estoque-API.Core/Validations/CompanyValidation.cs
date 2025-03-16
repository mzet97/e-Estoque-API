using e_Estoque_API.Core.Entities;
using e_Estoque_API.Domain.Validations;
using FluentValidation;

namespace e_Estoque_API.Core.Validations;

public class CompanyValidation : AbstractValidator<Company>
{
    public CompanyValidation()
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

        RuleFor(x => x.CompanyAddress)
            .NotNull()
            .WithMessage("{PropertyName} is required");

        RuleFor(x => x.CompanyAddress.Street)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .Length(3, 80)
            .WithMessage("The {PropertyName} need to have between {MinLength} and {MaxLength} characters");

        RuleFor(x => x.CompanyAddress.Number)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .Length(3, 80)
            .WithMessage("The {PropertyName} need to have between {MinLength} and {MaxLength} characters");

        RuleFor(x => x.CompanyAddress.Complement)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .Length(3, 80)
            .WithMessage("The {PropertyName} need to have between {MinLength} and {MaxLength} characters");

        RuleFor(x => x.CompanyAddress.Neighborhood)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .Length(3, 80)
            .WithMessage("The {PropertyName} need to have between {MinLength} and {MaxLength} characters");

        RuleFor(x => x.CompanyAddress.District)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .Length(3, 80)
            .WithMessage("The {PropertyName} need to have between {MinLength} and {MaxLength} characters");

        RuleFor(x => x.CompanyAddress.City)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .Length(3, 80)
            .WithMessage("The {PropertyName} need to have between {MinLength} and {MaxLength} characters");

        RuleFor(x => x.CompanyAddress.County)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .Length(3, 80)
            .WithMessage("The {PropertyName} need to have between {MinLength} and {MaxLength} characters");

        RuleFor(x => x.CompanyAddress.ZipCode)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .Length(3, 80)
            .WithMessage("The {PropertyName} need to have between {MinLength} and {MaxLength} characters");

        RuleFor(x => x.CompanyAddress.Latitude)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .Length(3, 80)
            .WithMessage("The {PropertyName} need to have between {MinLength} and {MaxLength}");
    }
}