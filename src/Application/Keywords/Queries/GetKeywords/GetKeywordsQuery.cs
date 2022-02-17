using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Keywords.Queries.GetKeywords;

public class GetKeywordsQuery : IRequest<IQueryable<Keyword>>
{
}

public class GetKeywordsQueryHandler : IRequestHandler<GetKeywordsQuery, IQueryable<Keyword>>
{
    private readonly IApplicationDbContext _context;

    public GetKeywordsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public Task<IQueryable<Keyword>> Handle(GetKeywordsQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_context.Keywords.Select(k => k));
    }
}