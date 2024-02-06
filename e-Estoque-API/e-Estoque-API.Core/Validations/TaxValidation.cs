using e_Estoque_API.Core.Entities;
using FluentValidation;

namespace e_Estoque_API.Core.Validations
{
    public class TaxValidation : AbstractValidator<Tax>
    {
        public TaxValidation()
        {
            RuleFor(t => t.Name)
                .NotEmpty().WithMessage("The {PropertyName} needs to be provided")
                .Length(3, 80).WithMessage("The {PropertyName} need to have between {MinLength} and {MaxLength} characters");

            RuleFor(t => t.Description)
                .NotEmpty().WithMessage("The {PropertyName} needs to be provided")
                .Length(3, 250).WithMessage("The {PropertyName} need to have between {MinLength} and {MaxLength} characters");

            RuleFor(t => t.Percentage)
                .NotEmpty().WithMessage("The {PropertyName} needs to be provided")
                .InclusiveBetween(0, 100).WithMessage("The {PropertyName} need to be between {From} and {To}");
        }
    }
}
