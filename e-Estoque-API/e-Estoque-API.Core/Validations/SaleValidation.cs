using e_Estoque_API.Core.Entities;
using e_Estoque_API.Domain.Validations;
using FluentValidation;

namespace e_Estoque_API.Core.Validations;

public class SaleValidation : AbstractValidator<Sale>
{
    public SaleValidation()
    {
        Include(new EntityValidation());

        RuleFor(r => r.Quantity)
           .NotEmpty().WithMessage("The {PropertyName} needs to be provided")
           .GreaterThan(0).WithMessage("The {PropertyName} needs to be greater than {ComparisonValue}");

        RuleFor(r => r.SaleType)
           .NotEmpty().WithMessage("The {PropertyName} needs to be provided");

        RuleFor(r => r.PaymentType)
           .NotEmpty().WithMessage("The {PropertyName} needs to be provided");

        RuleFor(r => r.TotalPrice)
           .NotEmpty().WithMessage("The {PropertyName} needs to be provided")
           .GreaterThan(0).WithMessage("The {PropertyName} needs to be greater than {ComparisonValue}");

        RuleFor(r => r.TotalTax)
           .NotEmpty().WithMessage("The {PropertyName} needs to be provided")
           .GreaterThan(0).WithMessage("The {PropertyName} needs to be greater than {ComparisonValue}");

        RuleFor(r => r.DeliveryDate)
           .NotEmpty().WithMessage("The {PropertyName} needs to be provided");

        RuleFor(r => r.SaleDate)
           .NotEmpty().WithMessage("The {PropertyName} needs to be provided");

        RuleFor(r => r.PaymentDate)
           .NotEmpty().WithMessage("The {PropertyName} needs to be provided");

        RuleFor(r => r.IdCustomer)
           .NotEmpty().WithMessage("The {PropertyName} needs to be provided");
    }
}