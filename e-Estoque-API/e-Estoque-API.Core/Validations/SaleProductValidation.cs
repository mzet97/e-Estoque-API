using e_Estoque_API.Core.Entities;
using FluentValidation;

namespace e_Estoque_API.Domain.Validations;

public class SaleProductValidation : AbstractValidator<SaleProduct>
{
    public SaleProductValidation()
    {
        RuleFor(x => x.IdProduct)
           .NotEqual(Guid.Empty)
           .WithMessage("The IdProduct cannot be empty.");

        RuleFor(x => x.IdSale)
           .NotEqual(Guid.Empty)
           .WithMessage("The IdSale cannot be empty.");
    }
}
