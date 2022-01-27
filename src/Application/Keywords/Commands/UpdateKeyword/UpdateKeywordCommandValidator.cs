using FluentValidation;

namespace Application.Keywords.Commands.UpdateKeyword;

public class UpdateKeywordCommandValidator : AbstractValidator<UpdateKeywordCommand>
{
    public UpdateKeywordCommandValidator()
    {
        RuleFor(k => k.Value)
            .NotEmpty();

        RuleFor(k => k.Ranking)
            .GreaterThanOrEqualTo(0);

        RuleFor(k => k.Culture)
            .NotEmpty();

        RuleFor(k => k.Timestamp)
            .NotEmpty();

        RuleFor(k => k.SuggestService)
            .NotEmpty();
    }
}