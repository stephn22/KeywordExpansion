﻿using FluentValidation;

namespace App.ApplicationCore.Keywords.Commands.CreateKeyword;

public class CreateKeywordCommandValidator : AbstractValidator<CreateKeywordCommand>
{
    public CreateKeywordCommandValidator()
    {
        RuleFor(k => k.Value)
            .NotEmpty();

        RuleFor(k => k.Culture)
            .NotEmpty();

        RuleFor(k => k.Ranking)
            .GreaterThanOrEqualTo(0);
    }
}