﻿using e_Estoque_API.Core.Entities;
using FluentValidation;

namespace e_Estoque_API.Core.Validations;

public class InventoryValidation : AbstractValidator<Inventory>
{
    public InventoryValidation()
    {
        RuleFor(r => r.Quantity)
           .NotEmpty().WithMessage("The {PropertyName} needs to be provided")
           .GreaterThan(0).WithMessage("The {PropertyName} needs to be greater than {ComparisonValue}");

        RuleFor(r => r.DateOrder)
           .NotEmpty().WithMessage("The {PropertyName} needs to be provided");

        RuleFor(r => r.IdProduct)
           .NotEmpty().WithMessage("The {PropertyName} needs to be provided");
    }
}