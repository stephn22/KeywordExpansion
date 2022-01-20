using FluentValidation;

namespace Application.Keywords.Commands.CreateKeyword;

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

        RuleFor(k => k.Timestamp)
            .NotEmpty();
    }
}