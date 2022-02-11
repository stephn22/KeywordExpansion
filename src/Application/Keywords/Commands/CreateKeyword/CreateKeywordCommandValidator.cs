using Domain.Constants;
using FluentValidation;

namespace Application.Keywords.Commands.CreateKeyword;

public class CreateKeywordCommandValidator : AbstractValidator<CreateKeywordCommand>
{
    public CreateKeywordCommandValidator()
    {
        RuleFor(k => k.Value)
            .MaximumLength(KeywordConstants.MaxLength);
        //.NotEmpty();

        RuleFor(k => k.StartingSeed)
            .MaximumLength(KeywordConstants.MaxLength);

        RuleFor(k => k.Culture)
            .NotEmpty();

        RuleFor(k => k.Ranking)
            .GreaterThanOrEqualTo(0);

        RuleFor(k => k.SuggestService)
            .NotEmpty();
    }
}