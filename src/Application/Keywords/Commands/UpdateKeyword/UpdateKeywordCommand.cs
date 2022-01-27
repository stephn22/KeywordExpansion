using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Keywords.Commands.UpdateKeyword;

public class UpdateKeywordCommand : IRequest
{
    public int Id { get; set; }
    public string Value { get; set; }
    public string Culture { get; set; }
    public int Ranking { get; set; }
    public DateTime Timestamp { get; set; }
    public string SuggestService { get; set; }
}

public class UpdateKeywordCommandHandler : IRequestHandler<UpdateKeywordCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateKeywordCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateKeywordCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Keywords.FindAsync(request.Id);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Keyword), request.Id);
        }

        entity.Value = request.Value;
        entity.Culture = request.Culture;
        entity.Ranking = request.Ranking;
        entity.Timestamp = request.Timestamp;
        entity.SuggestService = request.SuggestService;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}