using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Keywords.Commands.CreateKeyword;

public class CreateKeywordCommand : IRequest<Keyword>
{
    public string Value { get; set; }
    public string Culture { get; set; }
    public int Ranking { get; set; }
    public DateTime Timestamp { get; set; }
}

public class CreateKeywordCommandHandler : IRequestHandler<CreateKeywordCommand, Keyword>
{
    private readonly IApplicationDbContext _context;

    public CreateKeywordCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Keyword> Handle(CreateKeywordCommand request, CancellationToken cancellationToken)
    {
        var entity = new Keyword
        {
            Value = request.Value,
            Culture = request.Culture,
            Ranking = request.Ranking,
            Timestamp = request.Timestamp
        };

        _context.Keywords.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}