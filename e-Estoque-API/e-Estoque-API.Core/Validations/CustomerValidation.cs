﻿using e_Estoque_API.Core.Entities;
using FluentValidation;

namespace e_Estoque_API.Core.Validations;

public class CustomerValidation : AbstractValidator<Customer>
{
    public CustomerValidation()
    {
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
    }
}