using Application.Common.Services.BingAds;
using Application.Keywords.Commands.UpdateKeyword;
using Application.Keywords.Queries.GetKeywords;
using MediatR;

namespace Application.Keywords.Queries.RankKeywords;

public class RankKeywordsQuery : IRequest
{ }

public class RankKeywordsQueryHandler : IRequestHandler<RankKeywordsQuery>
{
    private readonly IMediator _mediator;

    public RankKeywordsQueryHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<Unit> Handle(RankKeywordsQuery request, CancellationToken cancellationToken)
    {
        var keywords = await _mediator.Send(new GetKeywordsQuery(), cancellationToken);

        foreach (var keyword in keywords)
        {
            var rank = await BingAdsUtil.GetBingAdsAsync(keyword.Value, keyword.Culture, cancellationToken);

            await _mediator.Send(new UpdateKeywordCommand
            {
                Id = keyword.Id,
                Ranking = rank.Ads.Count,
                Culture = keyword.Culture,
                StartingSeed = keyword.StartingSeed,
                SuggestService = keyword.SuggestService,
                Timestamp = keyword.Timestamp,
                Value = keyword.Value
            }, cancellationToken);
        }

        return Unit.Value;
    }
}