using App.Common.Exceptions;
using App.Common.Interfaces;
using App.Entities;
using MediatR;

namespace App.Keywords.Commands.UpdateKeyword;

public class UpdateKeywordCommand : IRequest
{
    public int Id { get; set; }
    public string Value { get; set; }
    public string Culture { get; set; }
    public int Ranking { get; set; }
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

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}