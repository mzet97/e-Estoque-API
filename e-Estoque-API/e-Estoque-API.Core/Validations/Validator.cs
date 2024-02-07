﻿using e_Estoque_API.Core.Entities;
using FluentValidation;

namespace e_Estoque_API.Core.Validations;

public static class Validator
{
    public static bool Validate<TV, TE>(TV validation, TE entity) where TV : AbstractValidator<TE> where TE : IEntityBase
    {
        var validator = validation.Validate(entity);

        if (validator.IsValid) return true;

        return false;
    }
}