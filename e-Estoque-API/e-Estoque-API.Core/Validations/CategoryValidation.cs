using e_Estoque_API.Core.Entities;
using e_Estoque_API.Domain.Validations;
using FluentValidation;

namespace e_Estoque_API.Core.Validations;

public class CategoryValidation : AbstractValidator<Category>
{
    public CategoryValidation()
    {
        Include(new EntityValidation());

        RuleFor(c => c.Name)
        .NotEmpty().WithMessage("The {PropertyName} needs to be provided")
        .Length(3, 80).WithMessage("The {PropertyName} need to have between {MinLength} and {MaxLength} characters");

        RuleFor(c => c.ShortDescription)
            .NotEmpty().WithMessage("The {PropertyName} needs to be provided")
            .Length(3, 500).WithMessage("The {PropertyName} need to have between {MinLength} and {MaxLength} characters");

        RuleFor(c => c.Description)
            .NotEmpty().WithMessage("The {PropertyName} needs to be provided")
            .Length(3, 5000).WithMessage("The {PropertyName} need to have between {MinLength} and {MaxLength} characters");
    }
}