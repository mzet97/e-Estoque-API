﻿using e_Estoque_API.Domain.Entities;
using FluentValidation;

namespace e_Estoque_API.Domain.Validations;

public class EntityValidation : AbstractValidator<Entity>
{
    public EntityValidation()
    {
        RuleFor(x => x.Id)
            .NotEqual(Guid.Empty)
            .WithMessage("The Id cannot be empty.");

        RuleFor(x => x.CreatedAt)
            .NotEqual(default(DateTime))
            .WithMessage("The creation date must be provided.")
            .InclusiveBetween(new DateTime(1900, 1, 1), new DateTime(3000, 12, 31))
            .WithMessage("The creation date must be between 1900 and 3000.");

        RuleFor(x => x.UpdatedAt)
            .Must(BeAValidDateOrNull)
            .WithMessage("UpdatedAt must be a valid date between 1900 and 3000 or null.");

        RuleFor(x => x.DeletedAt)
            .Must(BeAValidDateOrNull)
            .WithMessage("DeletedAt must be a valid date between 1900 and 3000 or null.");
    }

    private bool BeAValidDateOrNull(DateTime? date)
    {
        if (!date.HasValue)
            return true;

        return date.Value >= new DateTime(1900, 1, 1) && date.Value <= new DateTime(3000, 12, 31);
    }
}

